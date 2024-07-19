@echo off
setlocal

REM Caminho para a solução
set SOLUTION_PATH=CesoaPsiPt.sln

REM Caminhos para os projetos
set HOST_PROJECT_PATH=CesoaPsiPt.FrontEnd.HOST\CesoaPsiPt.FrontEnd.HOST.csproj
set SIMULADOR_PROJECT_PATH=CesoaPsiPt.Simulador\CesoaPsiPt.Simulador.csproj

REM Função para compilar e executar um projeto
call :run_project %HOST_PROJECT_PATH%
call :run_project %SIMULADOR_PROJECT_PATH%

REM Aguardar todos os processos em background concluírem
wait
echo Todos os projetos foram executados.
exit /b

:run_project
set PROJECT_PATH=%1
echo Building project %PROJECT_PATH%
dotnet build %PROJECT_PATH%
if %errorlevel% neq 0 (
    echo Build failed for project %PROJECT_PATH%
    exit /b 1
)
start "Running %PROJECT_PATH%" dotnet run --project %PROJECT_PATH%
exit /b 0
