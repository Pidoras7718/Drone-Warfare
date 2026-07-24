# Drone Warfare

Drone Warfare — проект мода для Terraria на tModLoader, ориентированный на беспилотники, FPV-дроны, полезные нагрузки, наведение, ПВО и мультиплеерную игру с друзьями.

Цель проекта — не сделать один предмет «на коленке», а постепенно построить расширяемую систему, где логика дронов, отображение, управление, HUD, полезные нагрузки и сетевая синхронизация разделены по модулям.

## Документация

- [Сводка проекта](Docs/DroneWarfare_Project_Summary.md)
- [Как работать в Codex](Docs/Codex_Workflow.md)

## Планируемая структура

```text
DroneWarfare/
├── README.md
├── CHANGELOG.md
├── ROADMAP.md
├── .gitignore
├── DroneWarfare/
│   ├── build.txt
│   ├── description.txt
│   ├── DroneWarfare.cs
│   ├── Common/
│   ├── Content/
│   ├── Assets/
│   └── Localization/
└── Docs/
    ├── DroneWarfare_Project_Summary.md
    ├── Codex_Workflow.md
    ├── DroneArchitecture.md
    ├── Networking.md
    ├── Guidance.md
    └── HUD.md
```

## Ближайшая цель

Первый этап — подготовить v0.1.0 Alpha: минимальный, но собираемый фундамент мода с базовой структурой tModLoader, FPV Controller, регистрацией клавиш, заготовкой `DroneInstance` и первым ручным FPV-дроном.
