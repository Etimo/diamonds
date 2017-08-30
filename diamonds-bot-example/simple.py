import requests
import json
import random
from time import sleep
from colorama import init, Fore, Back, Style


init()
BASE_URL = "http://localhost/api"


class Board(object):

    def __init__(self, data):
        self.id = data["id"]
        self.width = data["width"]
        self.height = data["height"]
        self.bots = data["bots"]
        self.diamonds = data["diamonds"]

    def get_bot(self, bot):
        for item in self.bots:
            if item["name"] == bot.name:
                return item

class Bot(object):

    def __init__(self, email, name, url):
        self.email = email
        self.name = name
        self.url = url

    def _get_url(self, endpoint):
        return "{}{}".format(self.url, endpoint)

    def _get_direction(self, dx, dy):
        if dx == -1 and dy == 0:
            return "West"
        elif dx == 1 and dy == 0:
            return "East"
        elif dx == 0 and dy == -1:
            return "North"
        elif dx == 0 and dy == 1:
            return "South"
        else:
            raise Exception("Invalid move")

    def _req(self, endpoint, method, body):
        print(">>> {} {} {}".format(
            Style.BRIGHT + method.upper() + Style.RESET_ALL,
            Fore.GREEN + endpoint + Style.RESET_ALL,
            body))
        func = getattr(requests, method)
        headers = {
            "Content-Type": "application/json"
        }
        req = func(self._get_url(endpoint), headers=headers, data=json.dumps(body))
        print("<<< {} {}".format(
            req.status_code,
            req.text)
        )
        return req

    def get_my_info(self):
        req = self._req("/bots/{}".format(self.bot_token), "get", {})
        if req.status_code == 200:
            result = req.json()
            self.name = result["name"]
            self.email = result["email"]

    def register(self):
        req = self._req("/bots", "post", {
            "email": self.email,
            "name": self.name
        })

        if req.status_code == 200:
            result = req.json()
            self.bot_token = result["token"]
            print("Received token", self.bot_token)

    def list_boards(self):
        req = self._req("/boards", "get", {})

        if req.status_code == 200:
            return map(lambda x: Board(x), req.json())

    def join(self, board_id):
        req = self._req("/boards/{}/join".format(board_id), "post", {
            "botToken": self.bot_token
        })

    def get_board(self, board_id):
        req = self._req("/boards/{}".format(board_id), "get", {})
        if req.status_code == 200:
            return Board(req.json())

    def move(self, board_id, dx, dy):
        return self._req("/boards/{}/move".format(board_id), "post", {
            "direction": self._get_direction(dx, dy),
            "botToken": self.bot_token
        })

try:
    with open(".bot_token", "r") as f:
        bot_token = f.read()
    bot = Bot("", "", BASE_URL)
    bot.bot_token = bot_token
    bot.get_my_info()
    print("Welcome back", bot.name)
except:
    print("No existing bot, register new one")
    bot = Bot("bot-1@etimo.se", "Bot 1", BASE_URL)
    bot.register()
    bot.list_boards()
    bot.join(1)
    with open(".bot_token", "w") as f:
        f.write(bot.bot_token)

print("LETS DO THIS!")
board = bot.get_board(1)
directions = [(1,0), (0,1), (-1,0), (0,-1)]
goal_position = None
current_position = None
current_direction = 0
while True:
    delta = directions[current_direction]
    if goal_position:
        delta_x = goal_position["x"] - current_position["x"]
        delta_y = goal_position["y"] - current_position["y"]
        if delta_x < 0:
            delta = [-1, 0]
        elif delta_y < 0:
            delta = [0, -1]
        elif delta_x > 0:
            delta = [1, 0]
        elif delta_y > 0:
            delta = [0, 1]
    result = bot.move(1, delta[0], delta[1])
    if result.status_code == 409 or random.random() > 0.6:
        current_direction = (current_direction + 1) % len(directions)
    else:
        board = Board(result.json())
        board_bot = board.get_bot(bot)
        if board_bot["diamonds"] == 5:
            # Move to base
            base = board_bot["base"]
            goal_position = base
            current_position = board_bot["position"]
        else:
            goal_position = None

    sleep(1)
