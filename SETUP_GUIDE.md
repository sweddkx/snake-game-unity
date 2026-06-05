# Snake Game Unity - Setup Guide

A polished Snake game for itch.io built with Unity

## Prerequisites

- **Unity 2022.3 LTS** or newer
- **TextMesh Pro** (comes with Unity)
- Basic understanding of Unity Editor

## Project Structure

```
Assets/
├── Scripts/
│   ├── Game/
│   │   ├── GameManager.cs      # Game state & score management
│   │   ├── SnakeController.cs  # Snake movement & collision
│   │   └── FoodManager.cs      # Food spawning
│   ├── UI/
│   │   └── UIManager.cs        # HUD & menu display
│   └── Utility/
│       └── AudioManager.cs     # Sound & music control
├── Scenes/
│   └── Game.unity              # Main game scene
├── Prefabs/
│   ├── SnakeSegment.prefab
│   ├── Food.prefab
│   └── ParticleEffect.prefab
└── Resources/
    ├── Audio/
    ├── Sprites/
    └── Fonts/
```

## Setup Steps

### 1. Create the Main Game Scene

1. In Unity, go to **File → New Scene**
2. Choose **2D Scene** template
3. Save as `Assets/Scenes/Game.unity`

### 2. Create GameObjects in Hierarchy

Create the following GameObjects in your scene:

#### GameManager
- Create empty GameObject named `GameManager`
- Add component: **GameManager** script
- Assign in Inspector:
  - Grid Width: 20
  - Grid Height: 20
  - Cell Size: 1

#### SnakeManager
- Create empty GameObject named `SnakeManager`
- Add component: **SnakeController** script
- Create a prefab for SnakeSegment (white square)
- Assign SnakeSegment prefab to the `segmentPrefab` field
- Add component: **AudioSource**
- Assign eat and die sounds (optional for now)

#### FoodManager
- Create empty GameObject named `FoodManager`
- Add component: **FoodManager** script
- Create a prefab for Food (red circle)
- Assign Food prefab to the `foodPrefab` field

#### AudioManager
- Create empty GameObject named `AudioManager`
- Add component: **AudioManager** script
- Add component: **AudioSource** (for background music)
- Check "Loop" in AudioSource

### 3. Create Canvas for UI

1. **Right-click in Hierarchy → UI → Legacy → Panel**
2. This creates a Canvas automatically
3. Create child UI elements:
   - Text (Score) - name: `ScoreText`
   - Text (High Score) - name: `HighScoreText`
   - Panel (Game Over) - name: `GameOverPanel`
   - Text inside GameOverPanel - name: `GameOverText`
   - Panel (Pause) - name: `PausePanel`

4. Add **UIManager** script to Canvas
5. Assign all UI elements in the Inspector

### 4. Create Prefabs

#### SnakeSegment Prefab
1. Create 2D Sprite in scene (Sprite: Square, Color: White)
2. Scale to (1, 1)
3. Add **BoxCollider2D**
4. Drag to `Assets/Prefabs/SnakeSegment.prefab`

#### Food Prefab
1. Create 2D Sprite in scene (Sprite: Circle, Color: Red)
2. Scale to (0.8, 0.8)
3. Add **BoxCollider2D**
4. Drag to `Assets/Prefabs/Food.prefab`

### 5. Configure Game Settings

1. **Edit → Project Settings → Player**
   - Product Name: "Snake"
   - Default Icon: (optional)
2. **Edit → Project Settings → Quality**
   - Set default quality level

### 6. Test the Game

1. Press **Play** in Unity Editor
2. Use **Arrow Keys** or **WASD** to control snake
3. Eat food to grow and gain points
4. Press **ESC** to pause

## Controls

| Key | Action |
|-----|--------|
| ↑ or W | Move Up |
| ↓ or S | Move Down |
| ← or A | Move Left |
| → or D | Move Right |
| ESC | Pause/Resume |
| R | Restart (after game over) |

## Building for itch.io

### WebGL Build (Recommended for itch.io)
1. **File → Build Settings**
2. Add `Assets/Scenes/Game.unity` to scenes
3. Switch Platform to **WebGL**
4. Player Settings:
   - Company: Your Name
   - Product Name: Snake
   - Resolution: 1280x720
   - WebGL Template: Default
5. Click **Build**
6. Upload `index.html` and `Build` folder to itch.io

### Windows Build
1. **File → Build Settings**
2. Switch Platform to **PC, Mac & Linux Standalone**
3. Target Platform: **Windows**
4. Click **Build**
5. Compress `.exe` and submit to itch.io

## Audio Assets

You'll need to add:
- `eat.wav` - Sound for eating food
- `die.wav` - Sound for game over
- `background.mp3` - Background music

Place in `Assets/Resources/Audio/`

## Customization

### Change Difficulty
Edit `SnakeController.cs`:
```csharp
[SerializeField] private float moveSpeed = 0.15f; // Lower = faster
```

### Change Grid Size
Edit `GameManager.cs`:
```csharp
[SerializeField] private int gridWidth = 20;
[SerializeField] private int gridHeight = 20;
```

### Change Colors
Edit `SnakeController.cs` in `UpdateSegmentColors()`:
```csharp
sr.color = Color.green; // Change head color
sr.color = new Color(0.2f, 0.8f, 0.2f); // Change body color
```

## Troubleshooting

**Snake doesn't move:**
- Make sure SnakeController has GameManager in the scene
- Check that moveSpeed isn't 0

**No sound:**
- Verify AudioSource is in scene
- Check volume isn't muted

**UI not showing:**
- Make sure Canvas is in scene
- Verify UIManager is assigned to all text elements

## Next Steps

1. Add particle effects on food collection
2. Implement difficulty levels
3. Add main menu
4. Create custom sprites
5. Add background music
6. Test on different devices

## License

Free to use and modify for your project!

---

**Ready to build your itch.io game!** 🎮
