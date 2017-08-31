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

