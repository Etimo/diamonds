# diamonds

Prerequisites:
1. docker
2. docker-compose
3. optional: docker-machine (necessary for remote deployment only)

Bring everything up locally:
    docker-compose up
Bring everything down locally:
    docker-compose down
Deploy to etimo-app-server (only possible if you've been granted ssh access!):
    ./deploy.sh
Take down on etimo-app-server:
    ./undeploy.sh
    
