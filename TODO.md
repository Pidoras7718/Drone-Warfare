# TODO.md

Текущие рабочие задачи (не путать с ROADMAP.md — здесь только ближайшие конкретные шаги).

## Сейчас
- [ ] Проверить сборку и поведение последних изменений (управление, HoldPosition, взрывной урон, разрушение блоков) в tModLoader через Build + Reload — в контейнере сборки нет dotnet/csc/tModLoader assemblies, поэтому автоматически не проверено
- [ ] DroneDamageSystem — отдельная система урона по игрокам/союзникам/town NPC + FriendlyFire config (см. DECISIONS.md)

## Дальше (в порядке обсуждения)
- [ ] HUD
- [ ] Fuel (топливо/заряд дрона)
- [ ] Signal (сигнал/связь с дроном)
- [ ] CameraController (вынести логику камеры отдельно)
- [ ] Полноценная физика дронов
- [ ] Автопилот (первая заготовка — режим HoldPosition уже есть)
- [ ] ГНСС / наведение по координатам
- [ ] ПВО
- [ ] Мультиплеер / Networking
- [ ] Новые типы БПЛА: Heavy UAV, Loitering munition, Air defense
- [ ] Новые боеприпасы

## Организационное
- [ ] Настроить связку VS Code + Cline (или Roo Code) + DeepSeek API
- [ ] Решить, нужна ли смена стратегии веток (main / develop / feature/*)
- [ ] При желании — завести Docs/PROMPTS/ с CodingStandards.md, ArchitecturePrompt.md, ReviewPrompt.md
