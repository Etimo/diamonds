# Etimo Diamonds

This readme is for developers that want to develop a bot to play Etimo Diamonds. When your bot is playing, you can watch it in realtime at http://diamonds.etimo.se.

If you want to run the backend on your own computer (which you don't have to) then read the [docker readme](DOCKER.md) instead.

# Game rules
The purpose of the game is to collect as many diamonds as possible and deliver them in the base within one minute. The bot can carry at most 5 diamonds, so it needs to deliver the diamonds to the base often.

Red diamonds count as 2 diamonds (and take up 2 spaces in your inventory) so they are twice as valuable as blue diamonds. The ratio between red and blue diamonds changes on each regeneration.

There is a red button available on the board that, when stepped on, triggers a regeneration of all diamonds on the board.

Diamonds will respawn whenever depleted.

The multi-colored discs are teleporters.

# API

The base url to the API is http://diamonds.etimo.se/api and API documentation is available at http://diamonds.etimo.se/api/docs.

Here is some info to get you started:
1. Use endpoint `Register Bot` to register a bot. The endpoint returns a token that you should keep and treat as a password.
2. Use endpoint `Get all boards` to get the ID of the board. Currently there is only one board.
3. Use endpoint `Get board by id` to get information about the board. You will need to call this endpoint quite often to get board information such as where diamonds are, where your bot is etc.

   Field `minimumDelayBetweenMoves` indicates how long time in milliseconds your bot must wait between each move.

   All bots on the board are returned in an array and you need to know the name of your bot to find your bot in the array. You set the name of the bot when you registered the bot. If you forget the name, you can get it by using endpoint `Get bot by token`.
4. Use endpoint `Join board` to join the board.
5. Use endpoint `Move bot` to move your bot one step in some direction.

# Example bots
We also have some [example bots](diamonds-bot-example/README.md) to get you up and running faster.

# Issues
You can create issues at https://github.com/Etimo/diamonds/issues
