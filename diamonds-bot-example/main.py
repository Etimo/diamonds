import sys
import argparse
from time import sleep
from game.board import Board
from game.bot import Bot
from game.api import Api
from game.util import *
from game.logic.random import RandomLogic
from game.logic.first_diamond import FirstDiamondLogic
from game.logic.crash_in_middle import CrashInMiddle
from game.logic.random_diamond import RandomDiamondLogic
from game.logic.teleporter_shaker import TeleporterShake
from game.logic.button_madness import ButtonMadness
from colorama import init, Fore, Back, Style

init()
BASE_URL = "http://diamonds.etimo.se/api"
CONTROLLERS = {
    "Random": RandomLogic,
    "CrashInMiddle": CrashInMiddle,
    "FirstDiamond": FirstDiamondLogic,
    "RandomDiamond": RandomDiamondLogic,
    "Teleport": TeleporterShake,
    "RedButton": ButtonMadness
}

###############################################################################
#
# Parse command line arguments
#
###############################################################################
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
parser.add_argument("--board",
    help="Id of the board to join",
    action="store")
parser.add_argument("--time-factor",
    help="A factor to multiply each move command with. If you want to run the bot in a slower mode e.g. use --time-factor=5 to multiply each delay with 5.",
    default=1,
    action="store")
parser.add_argument("--logic",
    help="The logic controller to use. Valid options are: {}".format(", ".join(list(CONTROLLERS.keys()))),
    action="store")
parser.add_argument("--max-bots",
    help="Limit for how many bots there should be after joining, otherwise the bot will simply wait",
    default=None,
    action="store")
group = parser.add_argument_group('API connection')
group.add_argument('--host', action="store", default=BASE_URL, help="Default: {}".format(BASE_URL))
args = parser.parse_args()

time_factor = int(args.time_factor)
max_bots = int(args.max_bots)
api = Api(args.host)
logic_controller = args.logic
if logic_controller not in CONTROLLERS:
    print("Invalid logic controller.")
    exit(1)

###############################################################################
#
# (Try and) Register a new bot if we have not supplied a token
#
###############################################################################
if not args.token:
    bot = Bot(args.email, args.name, api)
    result = bot.register()
    if result.status_code == 200:
        print("")
        print(Style.BRIGHT + "Bot registered. Token: {}".format(bot.bot_token) + Style.RESET_ALL)
        args.token = bot.bot_token
    else:
        print("Unable to register bot")
        exit(1)

###############################################################################
#
# Setup bot using token and play game
#
###############################################################################
bot = Bot("", "", api)
bot.bot_token = args.token
bot.get_my_info()
print("Welcome back", bot.name)

# Setup variables
logic_class = CONTROLLERS[logic_controller]
bot_logic = logic_class()

###############################################################################
#
# Find a board to join
#
###############################################################################
current_board_id = args.board
if not current_board_id:
    # List active boards to find one we can join if we haven't specified one
    boards = bot.list_boards()
    for board in boards:
        # Try to join board
        current_board_id = board.id
        if max_bots is None or len(board.bots) < max_bots:
            result = bot.join(current_board_id)
            if result.status_code == 200:
                break
else:
    # Try to join the one we specified
    result = bot.join(current_board_id)
    if result.status_code != 200:
        current_board_id = None

# Did we manage to join a board?
if not current_board_id:
    print("Unable to find any boards to join")
    exit(1)

###############################################################################
#
# Prepare state from current board
#
###############################################################################
board = bot.get_board(current_board_id)
move_delay = board.data["minimumDelayBetweenMoves"] / 1000

###############################################################################
#
# Game play loop
#
###############################################################################
while True:
    # Find our info among the bots on the board
    board_bot = board.get_bot(bot)

    # Calculate next move
    delta_x, delta_y = bot_logic.next_move(board_bot, board)

    # Try to perform move
    result = bot.move(current_board_id, delta_x, delta_y)
    if result.status_code == 409:
        # Read new board state
        board = bot.get_board(current_board_id)
    elif result.status_code == 403:
        # Game over, we are not allowed to move anymore
        break
    else:
        board = Board(result.json())

    # Get new state
    board_bot = board.get_bot(bot)
    if not board_bot:
        # Managed to get game over after move
        break

    # Don't spam the board more than it allows!
    sleep(move_delay * time_factor)

###############################################################################
#
# Game over!
#
###############################################################################
print("Game over!")
print("You played using the following token:")
print(Style.BRIGHT + bot.bot_token + Style.RESET_ALL)
print("Restart bot to run again. Use the following command:")
print("{} --token={}".format(sys.argv[0], bot.bot_token))
