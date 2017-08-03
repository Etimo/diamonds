#!/usr/bin/env bash

eval $(docker-machine env etimo-app-server)
docker-compose -f docker-compose.yml up -d --build
