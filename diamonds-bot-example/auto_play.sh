#!/bin/bash
HOST=http://diamonds.etimo.se/api
PYTHON=python3
if [ ! -f "./token/.auto_token" ]; then
    echo "Generating bot and token..."
    TOKEN=$($PYTHON main.py --host $HOST --email bot@etimo.se --name Etimo --logic FirstDiamond | grep "Bot registered" | awk '{print $4}')
    if [ "$TOKEN" == "" ]; then
        echo "Unable to get token."
        exit 1
    fi
    echo $TOKEN > ./token/.auto_token
    echo "Done. Got token $TOKEN"
else
    TOKEN=$(cat ./token/.auto_token)
fi

echo "Playing using token: $TOKEN"
$PYTHON main.py --host $HOST --token $TOKEN --logic FirstDiamond
