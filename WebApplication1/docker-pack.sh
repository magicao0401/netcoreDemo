#!/bin/bash

#环境变量
APP_ENV='sit'

# 仓库及镜像定义
DOCKER_NAME="mc/netcoredemo-${APP_ENV}" # 需修改
DOCKER_FILE="Dockerfile"
REPO_DOMAIN="localhost:8082"

# 镜像版本
VERSION=$1
if [ ! -n "$1" ];then
  VERSION=`date '+%Y%m%d%H%M%S'`
fi


# 生成docker镜像
docker build -t "$DOCKER_NAME:latest" -f $DOCKER_FILE .

# 打docker镜像tag
docker tag "$DOCKER_NAME:latest" "$REPO_DOMAIN/$DOCKER_NAME:$VERSION"

# push到腾讯云
docker push "$REPO_DOMAIN/$DOCKER_NAME:$VERSION"