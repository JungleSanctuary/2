
# JungleSanctuary Custom Rotations

This is a fork of RotationSolverReborn with custom rotations, including a specialized Pictomancer PVP rotation.

## Available Rotations

- **Jungle Sanctuary PvP (Pictomancer)**: A custom PVP rotation for Pictomancer with enhanced burst damage and defensive capabilities.

## How to Use

### Option 1: Use the Custom Repository

1. Open FFXIV and log in
2. Open the RotationSolver plugin configuration
3. Go to the "Custom Rotations" tab
4. In the "Custom Rotation Repo" field, enter:
   ```
   https://raw.githubusercontent.com/JungleSanctuary/2/main/pluginmaster.json
   ```
5. Click "Save" or "Apply"
6. Click "Update Custom Rotations"
7. Restart the plugin or game if necessary
8. Your custom rotations should now be available in the rotation selection

### Option 2: Manual Installation

1. Download the latest `JungleSanctuaryRotations.dll` from the [Releases](https://github.com/JungleSanctuary/2/releases) page
2. Place the DLL in your RotationSolver CustomRotations folder:
   ```
   %AppData%\XIVLauncher\pluginConfigs\RotationSolver\CustomRotations
   ```
3. Restart the plugin or game
4. Your custom rotations should now be available in the rotation selection

## How to Compile the DLL

If you want to compile the DLL yourself:

1. Make sure you have .NET 7.0 SDK installed
2. Open a command prompt in the repository directory
3. Run the following command:
   ```
   dotnet build -c Release
   ```
4. The compiled DLL will be in the `bin\x64\Release\net7.0-windows` directory

## Original RotationSolverReborn

This is a fork of [RotationSolverReborn](https://github.com/FFXIV-CombatReborn/RotationSolverReborn), a community-made fork of the original RotationSolver plugin for Final Fantasy XIV.
