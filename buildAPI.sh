#!/bin/bash

unameOut="$(uname -s)"
case "${unameOut}" in
    MINGW*)     machine=npipe:////.//pipe//docker_engine;;
    *)          machine=unix:///var/run/docker.sock
esac



docker build -t aspnet -f ./API/Dockerfile .
terraform init
terraform apply -var dockerhost="${machine}" -auto-approve


