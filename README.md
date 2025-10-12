<table>
  <tr>
    <td align="left" width="50%">
      <img width="100%" alt="gif1" src="https://github.com/NGnolep/ICDA---GameSeed/blob/main/Assets/Asset/HERepogif1.gif">
    </td>
    <td align="right" width="50%">
      <img width="100%" alt="gif2" src="https://github.com/NGnolep/ICDA---GameSeed/blob/main/Assets/Asset/HERepogif2.gif">
    </td>
  </tr>
</table>

##  Scripts and Features

A lot of feature in the game like items, sprinting, walking, hiding and so much more can be done thanks to tons of scripts that has been implemented to the game.<br>
Here are some of the main mechanic in this game
|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `GhostChase.cs` | Responsible for the Enemy in this game |
| `SpawnerScript.cs` | Responsible for all the items spawning in this game |
| `PlayerMovement.cs`  | Responsible for all the player movement in this game |
| `PlayerBar.cs`  | Responsible for the sanity and stamina bar in this game |
| `PlayerDeath.cs`  | Responsible for the condition of losing in this game|
| `etc`  | |

<br>

## About
Haunting Echoes is a 2D horror game that makes you wonder what is happening and if you are alone or not in this game. You gotta survive, hide and search for clues around the map to find what is happening. 
<br>

## Developer & Contributions
- Nhoel Goei (Gameplay(Enemy, Player), Cutscene/transition, Item Behavior Programmer)
- Clifftoven Wicaksono (Game Programmer)
- Pieter Nathanael (Sound Artist)
- Nelvin Anderson (Game Designer)
- Felix Olivier (Game Artist)
<br>

```
├── ICDA---GameSeed                   # Contain everything needed for Please Survive to works.
   ├── .vscode                        # Contains configuration files for Visual Studio Code (VSCode) when it's used as the code editor for the project.
      ├── extensions.json             # Contains settings and configurations for debugging, code formatting, and IntelliSense. This folder is related to Visual Studio Code integration.
      ├── launch.json                 # Contains the configuration necessary to start debugging Unity C# scripts within VSCode.                     
      ├── setting.json                # Contains workspace-specific settings for VSCode that are applied when working within the Unity project.
   ├── Assets                         # Contains every assets that have been worked with unity to create the game like the scripts and the art.
      ├── Animation                   # Contains all animation clip and animator controller for this game.
      ├── Asset                       # Contains all the game art like the sprites, background, even the character.
      ├── Font                        # Contains Font that is used in the game.
      ├── Prefab                      # Contains every pre-configured, reusable game object that can be instantiated in the game scene.
      ├── Scenes                      # Contains all scenes that exist in the game for it to interconnected with each other.
      ├── Script                      # Contains all scripts needed to make the gane get goings like PlayerMovement scripts.
      ├── Sound                       # Contains every sound used for the game like music and sound effects.
   ├── Packages                       # Contains game packages that responsible for managing external libraries and packages used in your project.
      ├── Manifest.json               # Contains the lists of all the packages that your project depends on and their versions.
      ├── Packages-lock.json          # Contains packages that ensuring your project always uses the same versions of all dependencies and their sub-dependencies.
   ├── Project Settings               # Contains the configuration of your game to control the quality settings, icon, or even the cursor settings
├── README.md                         # The description of Please Survive file from About til the developers and the contribution for this game.
```

## Game controls

The following controls are bound in-game, for gameplay and testing.

| Key Binding       | Function          |
| ----------------- | ----------------- |
| A,W,S,D          | Standard Player Movement |
| E             | Interact           |
| Left Shift    | Sprint |

<br>
