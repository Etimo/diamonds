import random
from ..util import get_direction


class BaseSitter(object):
    def __init__(self):
        self.target_bot_name = None

    def next_move(self, board_bot, board):
        # Find target bot
        target_bot = next((bot for bot in board.bots if bot["name"] == self.target_bot_name), None)
        if target_bot is None:
            target_bot = next((bot for bot in board.bots if bot["name"] != board_bot["name"]), None)
        if target_bot is None:
            return 0, 0

        goal_position = target_bot["base"]
        # Calculate move according to goal position
        current_position = board_bot["position"]
        return get_direction(current_position["x"], current_position["y"], goal_position["x"], goal_position["y"])
