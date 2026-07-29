# AI_CONTEXT.md

Этот файл — обязательная точка входа для любой модели (DeepSeek, GPT/Codex, Claude, Gemini и др.), прежде чем она начнёт менять код проекта. Перед задачей прочитай также `PROJECT_STATE.md` и `DECISIONS.md`.

## Project
Drone Warfare

## Genre
Terraria mod (tModLoader)

## Language
C#

## Target
tModLoader 1.4.4

## Архитектура
- `DroneInstance` — логика/состояние дрона
- `DroneSystem` — управляющая система (режимы, переключение и т.д.)
- `DronePlayer` — модификатор игрока (заморозка движения при управлении дроном)
- `FPVDroneProjectile` — сам projectile дрона в мире
- `DroneConfig` — конфигурация мода
- `DroneKeys` — переназначаемые keybind'ы
- `DroneMode` — enum режимов (Manual, HoldPosition, ...)
- Renderer / переключение камеры на активный drone projectile
- Networking — пока не реализован (мультиплеер в планах)

## Правила (Never use / Always)
- Никаких монолитных классов — модули должны быть разделены.
- Физика дрона не должна зависеть от UI.
- Урон реализован через Summon Damage + взрывной hitbox (не через касание/touch damage).
- Friendly Fire включён как концепция, но система урона по игрокам/союзным NPC пока НЕ реализована — см. DECISIONS.md.

## Управление (текущее)
- W/S/A/D (переназначаемые: DroneMoveUp/Down/Left/Right) — движение дрона
- F — переключение "управляю дроном / возвращаюсь к игроку"
- V — переключение режима Manual / HoldPosition
- G — подрыв дрона

## Будущие типы дронов
- FPV
- Heavy UAV
- Loitering munition (барражирующий боеприпас)
- Air defense (ПВО)

## Текущий milestone
v0.1 Alpha

## Инструментарий разработки
- Редактор: VS Code
- Репозиторий: GitHub (https://github.com/Pidoras7718/Drone-Warfare, ветка codex/-terraria)
- Агент для работы с проектом: Cline / Roo Code (читает проект, меняет файлы, предлагает commit)
- Модель: DeepSeek V4 Flash — рутинные задачи (Item, Projectile, UI, Config, NetworkPacket); DeepSeek V4 Pro — архитектурные изменения (автопилот, ГНСС, физика, мультиплеер)
- ChatGPT Codex использовался ранее, лимит обновится через месяц; переход на DeepSeek — не временная мера, а осознанный выбор для независимости от месячных лимитов
