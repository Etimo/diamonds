# Etimo Diamonds Docker Readme

Prerequisites:
1. docker
2. docker-compose

Bring everything up locally:

    docker-compose up --build
(--build flag rebuilds the application, which is for now necessary until we've set up proper dev containers)

Bring everything down locally:

    docker-compose down

To bring everything up with debug version of API:

1. Set DIAMONDS_API_CONFIG to debug (e.g. EXPORT DIAMONDS_API_CONFIG=debug)
2. docker-compose up --build

When running with debug config, program will be compiled including debug info and debugger vsdbg will be included in Docker image for API.

The default is to use prod config and thus Dockerfile.prod for the API (this is specified in the .env file).

## If you're running docker toolbox on Windows and can't connect to diamonds-viewer using the container ip address
When using docker toolbox, docker is running in a virtual machine and you need to connect via the virtual machine. Find the ip of the virtual machine:

    docker-machine ip
    
Also, temporarily modify docker-compose.yml by changing `- '127.0.0.1:81:80'` to `- '81:80'`. Do not commit this change.
