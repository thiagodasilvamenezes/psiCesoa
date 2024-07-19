#!/bin/bash

# Definir os nomes das imagens e contêineres
FRONTEND_IMAGE="cesoapsipt_frontend_host"
FRONTEND_CONTAINER="cesoapsipt_frontend_host_container"
SIMULADOR_IMAGE="cesoapsipt_simulador"
SIMULADOR_CONTAINER="cesoapsipt_simulador_container"

# Caminhos para os diretórios dos projetos
FRONTEND_PATH="CesoaPsiPt.FrontEnd.HOST"
SIMULADOR_PATH="CesoaPsiPt.Simulador"

# Construir a imagem Docker para o projeto frontend
echo "Building Docker image for Frontend..."
docker build -t $FRONTEND_IMAGE $FRONTEND_PATH
if [ $? -ne 0 ]; then
    echo "Docker build failed for Frontend"
    exit 1
fi

# Construir a imagem Docker para o projeto simulador
echo "Building Docker image for Simulador..."
docker build -t $SIMULADOR_IMAGE $SIMULADOR_PATH
if [ $? -ne 0 ]; then
    echo "Docker build failed for Simulador"
    exit 1
fi

# Executar o contêiner Docker para o projeto frontend
echo "Running Docker container for Frontend..."
docker run -d  --name $FRONTEND_CONTAINER $FRONTEND_IMAGE
if [ $? -ne 0 ]; then
    echo "Docker run failed for Frontend"
    exit 1
fi

# Executar o contêiner Docker para o projeto simulador
echo "Running Docker container for Simulador..."
docker run -d  --name $SIMULADOR_CONTAINER $SIMULADOR_IMAGE
if [ $? -ne 0 ]; then
    echo "Docker run failed for Simulador"
    exit 1
fi

echo "All containers are running."
