import random
from ..util import get_direction, position_equals
import random


class CrashInMiddle(object):
    def __init__(self):
        self.goal_position = None
    def plus_or_minus_one():
        value = random.randint(0,9)
        return 1 if value < 5 else -1
    #This bot will move towards it's target square and will step out in a random direction once there.
    def next_move(self, board_bot, board):
        target= {
          "x":board.width/2,
          "y":board.height/2
        }
        current_position = board_bot["position"]
        self.goal_position = target;
        if self.goal_position:
            # Calculate move according to goal position
            delta_x, delta_y = get_direction(current_position["x"], current_position["y"], self.goal_position["x"], self.goal_position["y"])
            if delta_x == 0 and delta_y == 0:
                delta_x = self.plus_or_minus_one
            return delta_x, delta_y

        return 0, 0
