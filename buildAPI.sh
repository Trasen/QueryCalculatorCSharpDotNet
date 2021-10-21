#!/bin/bash
docker build -t aspnet -f ./API/Dockerfile .
terraform init
terraform apply -auto-approve
