# PROJECT_STATE.md

## Current milestone
v0.1 Alpha

## Current branch
codex/-terraria

## Структура репозитория
```
DroneWarfare/
├── README.md
├── CHANGELOG.md
├── ROADMAP.md
├── INSTALLATION.md
├── .gitignore
├── DroneWarfare/
│   ├── build.txt
│   ├── description.txt
│   ├── DroneWarfare.cs
│   ├── Common/
│   │   ├── DroneConfig.cs
│   │   ├── DroneInstance.cs
│   │   ├── DroneKeys.cs
│   │   ├── DroneMode.cs
│   │   └── DroneSystem.cs
│   ├── Content/
│   │   ├── Items/FPVController.cs
│   │   ├── Players/DronePlayer.cs
│   │   └── Projectiles/FPVDroneProjectile.cs
│   ├── Localization/
│   │   ├── en-US.hjson
│   │   └── ru-RU.hjson
│   └── Assets/
└── Docs/
    ├── DroneWarfare_Project_Summary.md
    ├── Codex_Workflow.md
    ├── AI_CONTEXT.md
    ├── PROJECT_STATE.md
    ├── DECISIONS.md
    ├── TODO.md
    ├── DroneArchitecture.md
    ├── Networking.md
    ├── Guidance.md
    └── HUD.md
```

## Finished
- [x] Репозиторий создан, структура согласована
- [x] Базовая документация (README, CHANGELOG, ROADMAP, INSTALLATION)
- [x] build.txt, description.txt
- [x] DroneWarfare.cs, базовая архитектура (DroneInstance, DroneSystem, DroneConfig, DroneMode, DroneKeys)
- [x] Переназначаемые кнопки управления дроном (DroneMoveUp/Down/Left/Right, по умолчанию W/S/A/D) + локализация en-US/ru-RU
- [x] Режим HoldPosition (заготовка "зависнуть на месте", пока просто гасит скорость)
- [x] Переключение камеры на активный drone projectile во время управления
- [x] Заморозка движения игрока при активном управлении дроном (гасится горизонтальная скорость, отключаются control-флаги)
- [x] Клавиша F — переключение "управляю дроном / возвращаюсь к игроку"
- [x] Клавиша V — переключение режима Manual / HoldPosition
- [x] Урон переделан: касание больше не наносит урон, урон только при взрыве (friendly projectile damage, ExplosionDamage = 80)
- [x] Столкновение дрона с блоками → подрыв
- [x] Разрушение небольшого радиуса обычных блоков вокруг взрыва (важные/dungeon-тайлы пропускаются)
- [x] Коммит a7d8614 (feat: improve fpv drone controls), подготовлен PR через make_pr

## In progress / Not started
- [ ] DroneDamageSystem / Friendly Fire config (урон по игрокам и мирным NPC — отдельная задача, см. TODO.md и DECISIONS.md)
- [ ] Полноценная физика дронов
- [ ] Автопилот
- [ ] ГНСС (GNSS)
- [ ] HUD
- [ ] ПВО (Air defense)
- [ ] Мультиплеер / Networking
- [ ] Несколько типов БПЛА (FPV / Heavy UAV / Loitering munition уже реализован в первом приближении как FPV)
- [ ] Новые боеприпасы

## Known issues
- В контейнере сборки нет dotnet/csc/mcs и assemblies tModLoader/Terraria — сборку и поведение in-game нужно проверять вручную через Build + Reload в tModLoader.

## Организация разработки
- Архитектурные решения и алгоритмы обсуждаются в чате с ассистентом.
- Написание/изменение кода — через агента (Cline/Roo Code) поверх DeepSeek API, крупные архитектурные задачи — через DeepSeek V4 Pro, рутинные — через DeepSeek V4 Flash.
- ChatGPT Codex — резервный вариант, лимит которого обновится через месяц.
