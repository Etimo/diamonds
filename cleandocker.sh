function cleanByString {
    echo Cleaning all containers and images matching string: $1
docker ps -a|grep $1|awk '{print $1}'|xargs docker stop
docker ps -a|grep $1|awk '{print $1}'|xargs docker rm
docker images|grep $1|awk '{print $3}'|xargs docker image rm
}
cleanByString diamond
cleanByString none
docker volume prune
