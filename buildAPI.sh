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


if [ machine -n ]
 then
    terraform apply -var dockerhost="${machine}" -auto-approve
else    
  terraform apply -var dockerhost="${machine}" -auto-approve
fi


