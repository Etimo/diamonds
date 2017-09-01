import random
import sys
import argparse
from time import sleep
from game.board import Board
from game.bot import Bot
from game.api import Api
from game.util import *
from colorama import init, Fore, Back, Style

init()
BASE_URL = "http://localhost/api"


# Parse command line arguments
parser = argparse.ArgumentParser(description="Diamonds example bot")
group = parser.add_mutually_exclusive_group()
group.add_argument("--token",
    help="A bot token to use when running using an existing bot",
    action="store")
group.add_argument("--name",
    help="The name of the bot to register",
    action="store")
parser.add_argument("--email",
    help="The email of the bot to register",
    action="store")

group = parser.add_argument_group('API connection')
group.add_argument('--host', action="store", default=BASE_URL, help="Default: {}".format(BASE_URL))

args = parser.parse_args()

api = Api(args.host)

if not args.token:
    # (Try and) Register a new bot
    bot = Bot(args.email, args.name, api)
    result = bot.register()
    if result.status_code == 200:
        print("")
        print(Style.BRIGHT + "Bot registered. Token: {}".format(bot.bot_token) + Style.RESET_ALL)
        print("Re-run bot using --token parameter.")
    exit(0)

# Setup bot using token and play game
bot = Bot("", "", api)
bot.bot_token = args.token
bot.get_my_info()
print("Welcome back", bot.name)

# Setup variables
current_board_id = None
directions = [(1,0), (0,1), (-1,0), (0,-1)]
goal_position = None
current_position = None
current_direction = 0
sweeping_forward = True

# List active boards to find one we can join
boards = bot.list_boards()
for board in boards:
    # Try to join board
    current_board_id = board.id
    result = bot.join(current_board_id)
    if result.status_code == 200:
        break

# Did we find a board?
if not current_board_id:
    print("Unable to find any boards to join")
    exit(1)

# Get state of current board
board = bot.get_board(current_board_id)

# Game play loop
while True:
    # Find our info among the bots on the board
    board_bot = board.get_bot(bot)
    current_position = board_bot["position"]

    if goal_position:
        # We are aiming for a specific position, calculate delta
        delta_x = clamp(goal_position["x"] - current_position["x"], -1, 1)
        delta_y = clamp(goal_position["y"] - current_position["y"], -1, 1)
        if delta_x != 0:
            delta_y = 0
    else:
        delta = directions[current_direction]
        delta_x = delta[0]
        delta_y = delta[1]

    # Try to perform move
    result = bot.move(1, delta_x, delta_y)
    if result.status_code == 409 or random.random() > 0.6:
        # Change direction if we were unable to move or just randomly
        current_direction = (current_direction + 1) % len(directions)

        # Read new board state
        board = bot.get_board(current_board_id)
    elif result.status_code == 403:
        # Game over, we are not allowed to move anymore
        print("Game over!")
        print("Restart bot to run again")
        exit(0)

    # Get new state
    board = Board(result.json())
    board_bot = board.get_bot(bot)

    # Analyze new state
    if board_bot["diamonds"] == 5:
        # Move to base
        base = board_bot["base"]
        goal_position = base
    else:
        # Just roam around
        goal_position = None

    sleep(0.5)
