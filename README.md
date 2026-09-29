# Escape the Hacker's Network

> **A 2D Cybersecurity Serious Game & Educational Simulation (PBL Project)**  
> Developed for college Project-Based Learning (PBL) review and practical threat defense education.

---

## 🎮 Project Overview
**Escape the Hacker’s Network** is a 2D cyber-intelligence game built around an interactive vertical slice. The player takes the role of **Rei (Cyber Detective)** guided by **AIRI (Holographic AI Companion)** to investigate perimeter breaches orchestrated by the rogue hacker **ZERO**.

### 🌟 Key Features
- **Screen 1 (Main Menu):** Title screen with looping cyberpunk original soundtrack (`OST.mp3`).
- **Screen 2 (Cyber Operations Center):** 2D character exploration with proximity trigger (`[E]` to interact) at the central terminal.
- **Screen 3 (Phishing Detective Terminal):** Interactive email client with 5 real-world attack vectors (domain typosquatting, deceptive links, fake urgency, and dangerous executable attachments).
- **Screen 4 (Firewall Restored Milestone):** Dynamic scoring, threat accuracy, performance rating (Rank S / A), and victory badge.

---

## 📂 Repository Structure

```
├── Game_Assets/
│   ├── Audio/               # OST, Boot Beep, Siren Alarm, Success Chime
│   ├── Characters/          # Player Sprite Sheet, AIRI Companion, ZERO Antagonist
│   ├── Environment/         # Cyber Room Background, City Skyline Backdrops
│   ├── Props_Items/         # Interactive Terminal, Security Blast Doors, Keycards
│   ├── Scripts/             # Modular Unity 2D C# Scripts
│   │   ├── EmailData.cs
│   │   ├── PlayerController2D.cs
│   │   ├── TerminalInteractable.cs
│   │   ├── PhishingGameManager.cs
│   │   ├── AudioManager.cs
│   │   ├── MainMenuManager.cs
│   │   └── VictoryManager.cs
│   └── UI_Minigames/        # Phishing Detective UI Canvas & Achievement Badge
├── Playable_Demo/
│   └── index.html           # Standalone HTML5 playable web simulation
└── README.md
```

---

## 🕹️ Quick Start (Play in Browser)
You can test the entire 4-screen vertical slice immediately without opening Unity:
1. Navigate to the `Playable_Demo/` folder.
2. Double-click `index.html` to open it in Chrome, Edge, or Firefox.
3. Use **Arrow Keys / WASD** to move and **E** to access the terminal.

---

## 🛠️ Unity Engine Integration
To import into **Unity 2022 LTS / Unity 6**:
1. Create a new **Unity 2D (URP or Built-in)** project.
2. Drag the `Game_Assets/` folder directly into your Unity `Assets` panel.
3. In `Assets/Game_Assets/Characters/Player_Sprite_Sheet.jpg`, set:
   - **Texture Type:** `Sprite (2D and UI)`
   - **Sprite Mode:** `Multiple`
   - Open **Sprite Editor** and slice the frames.
4. Attach `PlayerController2D.cs` to the Player GameObject and `PhishingGameManager.cs` to the Terminal Canvas.

---

## 👥 Authors & Academic Context
* **Subject:** Project-Based Learning (PBL) — Cybersecurity Simulation
* **Version:** 1.0 (Vertical Slice MVP)
