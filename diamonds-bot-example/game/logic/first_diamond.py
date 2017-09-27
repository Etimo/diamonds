import random
from ..util import get_direction


class FirstDiamondLogic(object):
    def __init__(self):
        self.goal_position = None

    def next_move(self, board_bot, board):
        # Analyze new state
        if board_bot["diamonds"] == 5:
            # Move to base
            base = board_bot["base"]
            self.goal_position = base
        else:
            # Just roam around
            self.goal_position = None

        current_position = board_bot["position"]
        if not self.goal_position:
            self.goal_position = board.diamonds[0]

        if self.goal_position:
            # We are aiming for a specific position, calculate delta
            delta_x, delta_y = get_direction(current_position["x"], current_position["y"], self.goal_position["x"], self.goal_position["y"])
        return delta_x, delta_y
