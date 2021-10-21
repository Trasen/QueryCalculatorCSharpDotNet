terraform {
  required_providers {
    docker = {
      source  = "kreuzwerker/docker"
      version = ">= 2.13.0"
    }
  }
}

provider "docker" {
  host = "npipe:////.//pipe//docker_engine"
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
