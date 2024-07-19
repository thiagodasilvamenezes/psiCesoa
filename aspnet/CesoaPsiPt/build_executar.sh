#!/bin/bash

# Caminho para a solução
SOLUTION_PATH="CesoaPsiPt.sln"

# Caminhos para os projetos
HOST_PROJECT_PATH="CesoaPsiPt.FrontEnd.HOST/CesoaPsiPt.FrontEnd.HOST.csproj"
SIMULADOR_PROJECT_PATH="CesoaPsiPt.Simulador/CesoaPsiPt.Simulador.csproj"

# Função para compilar e executar um projeto
run_project() {
  PROJECT_PATH=$1
  echo "Building project $PROJECT_PATH"
  dotnet build $PROJECT_PATH
  if [ $? -eq 0 ]; then
    echo "Running project $PROJECT_PATH"
    dotnet run --project $PROJECT_PATH
  else
    echo "Build failed for project $PROJECT_PATH"
    exit 1
  fi
}

# Compilar e executar os projetos
run_project $HOST_PROJECT_PATH &
run_project $SIMULADOR_PROJECT_PATH &

# Aguardar todos os processos em background concluírem
wait

echo "Todos os projetos foram executados."
