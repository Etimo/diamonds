#!/usr/bin/env

eval $(docker-machine env etimo-app-server)
docker-compose -f docker-compose.yml up -d --build
