#!/bin/bash

unameOut="$(uname -s)"
case "${unameOut}" in
    Linux*)     machine=;;
    Darwin*)    machine=;;
    CYGWIN*)    machine=;;
    MINGW*)     machine=npipe:////.//pipe//docker_engine;;
    *)          machine="UNKNOWN:${unameOut}"
esac



docker build -t aspnet -f ./API/Dockerfile .
terraform init

terraform apply -var dockerhost="${machine}" -auto-approve
