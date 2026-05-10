# ❌⭕ XO Game (Tic-Tac-Toe) — C# Windows Forms

![C#](https://img.shields.io/badge/Language-C%23-purple?style=flat-square&logo=csharp)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey?style=flat-square&logo=windows)
![Framework](https://img.shields.io/badge/Framework-.NET%204.7.2-blueviolet?style=flat-square)
![UI](https://img.shields.io/badge/UI-Windows%20Forms-blue?style=flat-square)
![Type](https://img.shields.io/badge/Type-2--Player%20Game-orange?style=flat-square)
![Status](https://img.shields.io/badge/Status-Complete-brightgreen?style=flat-square)
![License](https://img.shields.io/badge/License-MIT-yellow?style=flat-square)

A fully functional **2-player Tic-Tac-Toe (XO) game** built with C# Windows Forms. Features custom image-based X and O pieces, a hand-drawn grid using GDI+, live turn tracking, winner detection with visual highlight, draw detection, and a full restart system.

---

## 📸 Preview

```
┌──────────────────────────────────────────────────────────┐
│                   ❌⭕  XO GAME  ❌⭕                     │
│                                                          │
│  Turn   : Player 1      ┌───────┬───────┬───────┐       │
│  Winner : In Progress   │  ❌   │   ⭕  │       │       │
│                         ├───────┼───────┼───────┤       │
│                         │       │  ❌   │       │       │
│  [ Restart Game ]       ├───────┼───────┼───────┤       │
│                         │       │       │  ❌   │       │
│                         └───────┴───────┴───────┘       │
│                                                          │
│  → Winner: Player 1  (winning cells highlight green 🟢)  │
└──────────────────────────────────────────────────────────┘
```

---

## ✨ Features

- 🎮 **2-player local gameplay** — Player 1 = X, Player 2 = O
- 🖼️ **Image-based pieces** — custom X.png, O.png, and question-mark placeholder images
- 🎨 **Custom GDI+ grid** — drawn with `Graphics.DrawLine()` using a white Pen on `Form_Paint`
- 🏆 **Winner detection** — checks all 8 winning combinations after every move
- 🟢 **Visual win highlight** — winning 3 cells turn GreenYellow instantly
- 🤝 **Draw detection** — triggers when all 9 cells filled with no winner
- 🔄 **Full restart** — resets all 9 buttons, scores, turn, and winner label
- 🚫 **Already-played cell guard** — shows error if player clicks an occupied cell
- 🔒 **Game-over lock** — blocks further clicks after game ends

---

## 🗂️ Project Structure

```
XO-Game/
│
├── Program.cs              # Entry point
├── Form1.cs                # All game logic
├── Form1.Designer.cs       # UI layout — 9 buttons + labels + restart
│
├── Resources/
│   ├── X.png               # X piece image
│   ├── O.png               # O piece image
│   └── question-mark-96.png # Default cell image (unplayed)
│
└── README.md
```

---

## 🧱 Code Architecture

### Data Structures

```csharp
struct stGameStatus {
    public enWinner Winner;   // who won (or InProgress / Draw)
    public bool GameOver;     // is the game finished?
    public byte PlayCount;    // total moves made (max 9)
}

enum enPlayer  { Player1, Player2 }
enum enWinner  { Player1, Player2, Draw, InProgress }
```

### Core Methods

| Method | Description |
|---|---|
| `ChangeImage(Button btn)` | Handles a cell click — places X or O image, updates Tag, switches turn, calls `CheckWinner()` |
| `CheckValues(btn1, btn2, btn3)` | Checks if 3 buttons share the same non-empty Tag — highlights green and triggers `EndGame()` if true |
| `CheckWinner()` | Calls `CheckValues()` for all 8 winning combos (3 rows + 3 cols + 2 diagonals) |
| `EndGame()` | Sets `lblPlayerTurn` to "Game Over", sets winner label, shows MessageBox |
| `RestButton(Button btn)` | Resets a single cell — Tag = "?", image = question mark, BackColor = Transparent |
| `RestartGame()` | Calls `RestButton()` on all 9 cells, resets turn, count, and status |
| `Form1_Paint()` | Draws the 4 grid lines using GDI+ `Graphics.DrawLine()` with a white rounded Pen |

### Tag-Based Cell State

Each button uses its `.Tag` property to track cell state — clean and no extra arrays needed:

| Tag value | Meaning |
|---|---|
| `"?"` | Empty cell — can be played |
| `"X"` | Player 1's move |
| `"O"` | Player 2's move |

```csharp
if (btn.Tag.ToString() == "?") {
    // valid move
    btn.Image = Resources.X;
    btn.Tag   = "X";
}
```

### Win Condition Check (all 8 combos)

```csharp
void CheckWinner() {
    // Rows
    if (CheckValues(button1, button2, button3)) return;
    if (CheckValues(button4, button5, button6)) return;
    if (CheckValues(button7, button8, button9)) return;
    // Columns
    if (CheckValues(button1, button4, button7)) return;
    if (CheckValues(button2, button5, button8)) return;
    if (CheckValues(button3, button6, button9)) return;
    // Diagonals
    if (CheckValues(button1, button5, button9)) return;
    if (CheckValues(button3, button5, button7)) return;
}
```

### Custom GDI+ Grid Drawing

```csharp
private void Form1_Paint(object sender, PaintEventArgs e) {
    Pen myPen = new Pen(Color.White);
    myPen.Width    = 10;
    myPen.StartCap = LineCap.Round;
    myPen.EndCap   = LineCap.Round;

    // 2 horizontal lines
    e.Graphics.DrawLine(myPen, 400, 300, 1050, 300);
    e.Graphics.DrawLine(myPen, 400, 460, 1050, 460);
    // 2 vertical lines
    e.Graphics.DrawLine(myPen, 610, 140, 610, 620);
    e.Graphics.DrawLine(myPen, 840, 140, 840, 620);
}
```

---

## 🚀 Getting Started

### Prerequisites
- **Visual Studio 2019+**
- **.NET Framework 4.7.2**
- Windows OS

### Run
1. Open `MyFirstWindowsForm.sln`
2. Press `Ctrl + F5`

---

## 📊 Big O

| Operation | Time | Notes |
|---|---|---|
| `ChangeImage()` | O(1) | Tag check + image swap |
| `CheckWinner()` | O(1) | Fixed 8 comparisons always |
| `RestartGame()` | O(1) | Fixed 9 cells always |

> The entire game logic is **O(1)** — board size is constant (3×3).

---

## 🔮 Possible Improvements

- [ ] Add **score tracking** across multiple rounds (Player 1: 3 wins, Player 2: 2 wins)
- [ ] Add **custom player names** input at game start
- [ ] Add a **single-player AI mode** (minimax algorithm)
- [ ] Use a `Button[,]` 2D array instead of `button1`...`button9` to remove repetition
- [ ] Add **sound effects** on win / draw
- [ ] Add **animations** on winning highlight

---

## 👨‍💻 Author

> Built with ❤️ as part of a C# Windows Forms learning journey.

Feel free to fork, star ⭐, or contribute!

---

## 📄 License

This project is licensed under the **MIT License** — free to use and modify.
