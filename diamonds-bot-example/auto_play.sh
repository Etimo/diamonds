#!/bin/bash
INDEX=$1
HOST=http://diamonds.etimo.se/api
LOGICS[0]="FirstDiamond"
LOGICS[1]="RandomDiamond"

NR_LOGICS=${#LOGICS[@]}
CHOSEN_LOGIC_INDEX=$(($RANDOM % $NR_LOGICS))
CHOSEN_LOGIC=${LOGICS[$CHOSEN_LOGIC_INDEX]}
TOKEN_FILENAME="./token/token.$INDEX"
PYTHON=python3
if [ ! -f "$TOKEN_FILENAME" ]; then
    echo "Generating bot and token..."
    TOKEN=$($PYTHON main.py --host $HOST --email bot$INDEX@etimo.se --name Etimo$INDEX --time-factor=3 --logic $CHOSEN_LOGIC | grep "Bot registered" | awk '{print $4}')
    if [ "$TOKEN" == "" ]; then
        echo "Unable to get token."
        exit 1
    fi
    echo $TOKEN > $TOKEN_FILENAME
    echo "Done. Got token $TOKEN"
else
    TOKEN=$(cat $TOKEN_FILENAME)
fi

echo "Playing using token: $TOKEN"
$PYTHON main.py --host $HOST --token $TOKEN --logic $CHOSEN_LOGIC --time-factor=3
