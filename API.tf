terraform {
  required_providers {
    docker = {
      source  = "kreuzwerker/docker"
      version = ">= 2.13.0"
    }
  }
}

variable "dockerhost" {
  default = "npipe:////.//pipe//docker_engine"
}

variable "internalport" {
  default = 80
}

variable "externalport" {
  default = 8087
}
        
        

provider "docker" {
  
  host = var.dockerhost
}

resource "docker_container" "aspnet" {

  image = docker_image.aspnet.latest
  name  = "QueryCalculator"
  ports {
    internal = 80
    external = 8087
  }
}


resource "docker_image" "aspnet" {
  name = "aspnet"
  keep_locally = true
}
