@echo off

REM Set JDK installation directory path
set JDK_DIR E:\tools\java /m

set JAVA_HOME "%JDK_DIR%" 

REM Update PATH environment variable for system
set PATH "%PATH%;%JDK_DIR%\bin" /m