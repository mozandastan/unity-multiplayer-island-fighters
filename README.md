# Island Fighters Multiplayer

Welcome to the Multiplayer Game project! This project is a Unity-based multiplayer game using Photon for networking. The game features real-time player interactions, including combat mechanics and a dynamic scoreboard.

![Menu1](https://github.com/mozandastan/unity-multiplayer-island-fighters/assets/151640771/f377cb6b-8bbd-421c-ab66-40b730870e9a) ![Menu2](https://github.com/mozandastan/unity-multiplayer-island-fighters/assets/151640771/96c84d15-a10a-4c31-8d41-720f82d8082a)
![Game3](https://github.com/mozandastan/unity-multiplayer-island-fighters/assets/151640771/b15f5b79-5953-499a-95e1-b06a0a66b9ba) ![Game 4](https://github.com/mozandastan/unity-multiplayer-island-fighters/assets/151640771/eb7e576f-9aae-47fb-bbb8-c7f911632290)
![Game2](https://github.com/mozandastan/unity-multiplayer-island-fighters/assets/151640771/11111eff-5906-47a1-aec6-dd2c2cc04f37)


## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Gameplay](#gameplay)
- [Code Structure](#code-structure)
- [Assets](#assets)

## Features

- Real-time multiplayer with Photon
- Players can create, join, and leave rooms.
- Combat mechanics with attack and push features
- Dynamic scoreboard showing player kills and deaths
- Respawn system
- 3D positional audio effects

## Installation

To get started with this project, follow these steps:

1. Clone the repository:
    ```bash
    git clone https://github.com/mozandastan/unity-multiplayer-island-fighters.git
    ```
2. Open the project in Unity:
    - Open Unity Hub.
    - Click on the "Add" button.
    - Select the cloned repository folder.
3. Install required packages:
    - Ensure you have the Photon Unity Networking (PUN) 2 package installed.
    - Go to `Window > Package Manager` and install any other dependencies if prompted.
4. Configure Photon:
    - Create a Photon account at [Photon Engine](https://www.photonengine.com/).
    - Create a new Photon App and get the App ID.
    - In Unity, go to `Window > Photon Unity Networking > PUN Wizard` and enter your App ID.

## Usage

### Running the Game

1. Open the `MenuScene` scene.
2. Click on the "Play" button in Unity to start the game.
3. Play the game in the Unity editor or build and run the game.

## Gameplay

### Objective

- Defeat other players by depleting their health or pushing them off the platform.
- Your kills and deaths are tracked on a dynamic scoreboard.

### Controls

- **W/A/S/D:** Move the player.
- **Space:** Attack.
- **Escape:** Open the pause menu.
- **Tab:** Open the leaderboard.

## Code Structure

#### MenuController.cs
Manages the main menu, room creation, and room joining.

#### RoomManager.cs
Manages room-related functionality, including spawning players and handling room events.

#### PlayerManager.cs
Handles player initialization, properties (kills and deaths), and updates to the player's custom properties on Photon.

#### ThirdPersonController.cs
Handles player movement and animation.

#### PlayerCombat.cs
Manages player combat mechanics, including attacking other players, dealing damage, and applying force to other players. Synchronizes attack animations and effects across the network.

#### PlayerHealth.cs
Handles player health management, including taking damage, updating the health UI, and managing player death and respawn. Plays sound effects for hits and respawns.

#### Scoreboard.cs
Manages the in-game scoreboard, updating player stats when properties change.

## Assets

This project uses various assets for game functionality and visuals. Below is a list of the main assets used:

- [PUN 2 - FREE](https://assetstore.unity.com/packages/tools/network/pun-2-free-119922): Used for real-time multiplayer functionality.
- [FlatPoly: Floating Islands](https://assetstore.unity.com/packages/3d/environments/landscapes/flatpoly-floating-islands-100809): Used for creating the game's environment with floating islands.
- [Quirky Series - FREE Animals Pack](https://assetstore.unity.com/packages/3d/characters/animals/quirky-series-free-animals-pack-178235): Used for adding animal characters in the game.
- [Cartoon FX Remaster Free](https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-remaster-free-109565): Used for visual effects such as explosions and magic spells.
- [Customizable Skybox](https://assetstore.unity.com/packages/2d/textures-materials/sky/customizable-skybox-174576): Used for creating a dynamic sky background.
- [Hyper Casual Mobile GUI](https://assetstore.unity.com/packages/2d/gui/hyper-casual-mobile-gui-268659): Used for the game's user interface elements.

