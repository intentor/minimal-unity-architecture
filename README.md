# Minimal Unity Architecture

Sample of a minimal Unity game architecture.

<img width="521" height="411" alt="Unity" src="https://github.com/user-attachments/assets/7ee08087-28e8-4412-9f84-9cea14087a70" />

## How to Use

Open `Modules/Menu/Menu.unity` to access the main menu and start the game.

It's a *Tag* game in which the player must run from the enemies while trying to move them through an arc.

## Custom Components

All custom components can be found at `Modules/Shared/Scripts/Runtime`:

- `Events`: Event management through decoupled `ScriptableObject` assets.
- `Pooling`: Pooling and spawner systems for spawing objects in a game.
- `SceneManagement`: Scene loader to allow scene transitions from UI buttons.
- `StateManagement`: Bootstrap State Machine system to handle states and transitions.

## License

Licensed under the [The MIT License (MIT)](http://opensource.org/licenses/MIT). Please see [LICENSE](LICENSE) for more information.
