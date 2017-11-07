import random
from ..util import get_direction


class TeleporterShake(object):
    def __init__(self):
        self.goal_position = None

    def next_move(self, board_bot, board):
        # Analyze new state
            # Move towards first diamond on board
        self.goal_position = board.gameObjects[0].get("position");
        if self.goal_position:
            # Calculate move according to goal position
            current_position = board_bot["position"]
            delta_x, delta_y = get_direction(current_position["x"], current_position["y"],
                    self.goal_position["x"], self.goal_position["y"])
            if delta_x == 0 and delta_y == 0: 
                return 1,1; 
            return delta_x, delta_y

        return 0, 0
