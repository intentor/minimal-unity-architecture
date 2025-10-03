# Minimal Unity Architecture

Example of a minimal Unity game architecture.

<img width="521" height="411" alt="Unity" src="https://github.com/user-attachments/assets/7ee08087-28e8-4412-9f84-9cea14087a70" />

## How to Use

Open `Modules/Menu/Menu.unity` to access the main menu.

## Custom Components

All custom compomnents can be found at `Modules/Shared/Scripts/Runtime`:

- `Events`: Event management through reusable `ScriptableObject` assets.
- `Pooling`: A pooling and spawner systems for spawing objects in a game.
- `SceneManagement`: A simple scene loader to allow scene transitions from UI buttons.
- `StateManagement`: A bootstrap StateMachine system able to handle states and transitions.
