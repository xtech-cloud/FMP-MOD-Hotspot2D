
@echo off

REM !!! Generated by the fmp-cli 1.86.0.  DO NOT EDIT!

md Hotspot2D\Assets\3rd\fmp-xtc-hotspot2d

cd ..\vs2022
dotnet build -c Release

copy fmp-xtc-hotspot2d-lib-mvcs\bin\Release\netstandard2.1\*.dll ..\unity2021\Hotspot2D\Assets\3rd\fmp-xtc-hotspot2d\
