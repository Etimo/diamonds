Simple bot
==========

How to install
--------------

`pip install requests colorama`


Register bot
------------

First you need to register one or more bots if not already done. This can be done using the following command:

`python simple.py --name <name> --email <email>`

The application will out a token if the registration was successful. Don't loose this token!


Run a game session
------------------

When you have a token you can start a new game session (or continue an existing one) using the following command:

`python simple.py --token <token>`

The bot will play until game over. You can then run the bot again without having to register a new one.

Register multiple bots and play them all at once if you like!