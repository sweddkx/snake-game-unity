# 🐍 Snake Game Unity - itch.io Ready

A polished, feature-rich Snake game built with Unity. Perfect for itch.io submission with smooth gameplay, high scores, and satisfying visual feedback.

![Snake Game](https://img.shields.io/badge/Unity-2022.3+-blue) ![License](https://img.shields.io/badge/License-MIT-green) ![Status](https://img.shields.io/badge/Status-Ready%20to%20Build-brightgreen)

---

## ✨ Features

### Core Gameplay
- ✅ **Smooth Grid-Based Movement** - Arrow keys or WASD controls
- ✅ **Collision Detection** - Wall and self-collision
- ✅ **Food Spawning System** - Random food placement avoiding snake
- ✅ **Score Tracking** - Real-time score display with high score persistence
- ✅ **Game States** - Playing, Paused, Game Over

### Polish & itch.io Ready
- ✅ **High Score Persistence** - Auto-saved with PlayerPrefs
- ✅ **Sound Effects** - Eat and die sounds
- ✅ **Background Music** - Looping background track
- ✅ **Particle Effects** - Visual feedback on food collection
- ✅ **Pause System** - ESC to pause/resume
- ✅ **Responsive UI** - Clean HUD with TextMesh Pro
- ✅ **Difficulty Settings** - Adjustable snake speed

---

## 🎮 Controls

| Input | Action |
|-------|--------|
| **↑ / W** | Move Up |
| **↓ / S** | Move Down |
| **← / A** | Move Left |
| **→ / D** | Move Right |
| **ESC** | Pause / Resume |
| **R** | Restart (Game Over screen) |

---

## 🚀 Quick Start

### Prerequisites
- **Unity 2022.3 LTS** or newer
- **TextMesh Pro** (included with Unity)
- Git (optional, for version control)

### Setup Instructions

1. **Clone or download this repository**
   ```bash
   git clone https://github.com/sweddkx/snake-game-unity.git
   cd snake-game-unity
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Open" and select the project folder
   - Unity will import all assets

3. **Follow SETUP_GUIDE.md**
   - Create GameObjects in scene
   - Assign scripts to objects
   - Create simple prefabs (colored squares for snake/food)
   - Run and play!

### Create Simple Assets (No Art Required!)

**SnakeSegment Prefab:**
- 2D Sprite: Use built-in square sprite
- Color: Green for head, Light Green for body
- Size: 1x1

**Food Prefab:**
- 2D Sprite: Use built-in circle sprite
- Color: Red
- Size: 0.8x0.8

---

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── Game/
│   │   ├── GameManager.cs      # Game state, scoring, pause logic
│   │   ├── SnakeController.cs  # Snake movement & collision
│   │   └── FoodManager.cs      # Food spawning & particles
│   ├── UI/
│   │   └── UIManager.cs        # HUD, menus, UI updates
│   └── Utility/
│       └── AudioManager.cs     # Music & sound management
├── Scenes/
│   └── Game.unity              # Main game scene
├── Prefabs/
│   ├── SnakeSegment.prefab
│   └── Food.prefab
└── Resources/
    ├── Audio/                  # Place .mp3/.wav files here
    ├── Sprites/                # (Optional) Custom graphics
    └── Fonts/                  # (Optional) Custom fonts
```

---

## 🎯 Game Mechanics

### Snake Movement
- Snake moves continuously in the current direction
- Input buffers next direction change
- Prevents 180° turns (can't move directly backwards)
- Grid-based positioning for clean collision detection

### Food System
- Food spawns at random valid grid position
- Never spawns on snake body
- Eating food increases snake length and adds 10 points
- Visual/audio feedback on consumption

### Game Over Conditions
- **Wall Collision** - Snake hits boundary
- **Self Collision** - Snake collides with own body
- **Game Over Panel** - Shows final score and high score

### Pause System
- Press **ESC** anytime to pause
- Game freezes but UI remains responsive
- Press **ESC** again to resume
- Pause panel displays on screen

---

## 🔧 Customization

### Adjust Difficulty
**SnakeController.cs - Line 16:**
```csharp
[SerializeField] private float moveSpeed = 0.15f; 
// Lower values = faster snake (harder)
// 0.10f = Hard, 0.15f = Medium, 0.20f = Easy
```

### Change Grid Size
**GameManager.cs - Lines 8-9:**
```csharp
[SerializeField] private int gridWidth = 20;
[SerializeField] private int gridHeight = 20;
```

### Change Colors
**SnakeController.cs - UpdateSegmentColors() method:**
```csharp
sr.color = Color.green;                      // Head color
sr.color = new Color(0.2f, 0.8f, 0.2f);     // Body color
```

### Adjust Score
**SnakeController.cs - Line 110:**
```csharp
GameManager.Instance.AddScore(10);  // Points per food
```

---

## 🎵 Adding Audio

### Required Audio Files
Place in `Assets/Resources/Audio/`:
- **eat.wav** - Short beep/pop sound (0.3-0.5 seconds)
- **die.wav** - Buzzer/negative sound (0.5-1 second)
- **background.mp3** - Loop-friendly music (30+ seconds)

### Free Audio Sources
- **Freesound.org** - Free sound effects (CC0/CC-BY)
- **Zapsplat.com** - Free music & effects
- **OpenGameArt.org** - Game-ready assets
- **Pixabay.com** - Royalty-free music

### Assignment
1. Drag audio files to `Assets/Resources/Audio/`
2. In SnakeController inspector - assign eat/die sounds
3. In AudioManager inspector - assign background music

---

## 📦 Building for itch.io

### Option 1: WebGL (Recommended - Play in Browser)
```
1. File → Build Settings
2. Add scene: Assets/Scenes/Game.unity
3. Switch Platform → WebGL
4. Player Settings:
   - Company: Your Name
   - Product Name: Snake
   - Resolution: 1280x720
5. Build → Create folder "WebGL"
6. Upload to itch.io (WebGL format)
```

### Option 2: Windows Executable
```
1. File → Build Settings
2. Add scene: Assets/Scenes/Game.unity
3. Switch Platform → PC, Mac & Linux Standalone
4. Target Platform: Windows x86_64
5. Build → Create folder "Windows"
6. Zip contents and upload to itch.io
```

### itch.io Submission
1. Go to [itch.io](https://itch.io)
2. Create new project
3. Upload build files
4. Set as playable (WebGL) or downloadable (Windows)
5. Customize page with screenshots, description
6. Publish!

---

## 📝 Scripts Overview

### GameManager.cs
- Singleton pattern for global access
- Tracks game state (playing/paused/over)
- Manages scoring and high score persistence
- Emits events for UI updates
- **Key Methods:** `AddScore()`, `GameOver()`, `TogglePause()`

### SnakeController.cs
- Handles snake movement with input buffering
- Detects wall and self-collisions
- Spawns/destroys body segments dynamically
- Manages color differentiation (head vs body)
- Plays sound effects on eat/die
- **Key Methods:** `MoveSnake()`, `HandleInput()`, `Die()`

### FoodManager.cs
- Spawns food at random valid positions
- Avoids snake body positions
- Creates particle effects on spawn
- Exposes food position for collision detection
- **Key Methods:** `SpawnFood()`, `GetFoodPosition()`

### UIManager.cs
- Updates HUD with current score
- Displays high score
- Shows game over panel with final score
- Shows pause menu overlay
- Subscribes to GameManager events
- **Key Methods:** `UpdateScore()`, `ShowGameOver()`, `ShowPauseMenu()`

### AudioManager.cs
- Singleton for global audio control
- Plays background music on loop
- Manages master volume
- Persists volume setting with PlayerPrefs
- **Key Methods:** `SetVolume()`, `GetVolume()`

---

## 🐛 Troubleshooting

| Problem | Solution |
|---------|----------|
| Snake doesn't move | Verify SnakeController has GameManager in scene |
| No sound | Check AudioSource exists and volume isn't 0 |
| UI not visible | Ensure Canvas is in scene, UIManager is assigned |
| Score not saving | Check PlayerPrefs isn't disabled in Player Settings |
| Game runs slow | Lower Grid Width/Height or increase moveSpeed |

---

## 📚 Learning Resources

- **Unity Manual:** [docs.unity3d.com](https://docs.unity3d.com)
- **TextMesh Pro Guide:** Setup in SETUP_GUIDE.md
- **itch.io Guide:** [itch.io/docs](https://itch.io/docs)
- **Game Design Patterns:** Check code comments for patterns used

---

## 🎓 What You'll Learn

- **Game Architecture** - Singleton pattern, event systems
- **Input Handling** - Keyboard controls with input buffering
- **Collision Detection** - Grid-based and object-based
- **UI Management** - TextMesh Pro, dynamic updates
- **Audio Integration** - Background music, sound effects
- **Data Persistence** - PlayerPrefs for high scores
- **Build & Deploy** - WebGL and standalone builds

---

## 📄 License

This project is open source and free to use for personal and commercial projects.

---

## 🤝 Contributing

Want to improve the game? Feel free to:
- Fork the repository
- Add new features (power-ups, enemies, levels)
- Improve visuals with custom sprites
- Optimize performance
- Submit pull requests!

---

## 🎉 Ready to Ship!

Your Snake game is ready for itch.io! Follow these steps:
1. ✅ Test gameplay thoroughly
2. ✅ Add audio files
3. ✅ Create polished build
4. ✅ Write engaging description
5. ✅ Add screenshots
6. ✅ Deploy to itch.io
7. 🚀 Share with the world!

**Happy developing! 🎮**

---

**Built with ❤️ using Unity**
