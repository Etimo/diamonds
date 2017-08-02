#!/bin/bash

eval $(docker-machine env etimo-app-server)
docker-compose down
