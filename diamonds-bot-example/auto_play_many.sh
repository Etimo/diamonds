#!/bin/bash

COUNT=2
for a in $(seq 1 $COUNT); do
    ./auto_play.sh $a &
    sleep 3.7
done

wait
