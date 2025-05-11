@echo off
echo Building JungleSanctuaryRotations.dll...
dotnet build -c Release
echo.
echo If the build was successful, the DLL is located at:
echo bin\x64\Release\net7.0-windows\JungleSanctuaryRotations.dll
echo.
echo Copy this DLL to your RotationSolver CustomRotations folder:
echo %%AppData%%\XIVLauncher\pluginConfigs\RotationSolver\CustomRotations
echo.
pause
