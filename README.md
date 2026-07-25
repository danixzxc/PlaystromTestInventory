
# Smart Inventory - Unity 2D Test Project

## Overview

A 2D interactive inventory system where players open chests to receive random loot drops. Loot items fall to the ground, can be collected by clicking, and fly to their respective UI indicators. Collecting multiple identical items quickly triggers a temporary bonus multiplier.

## Features

- **Click-to-open chest** with spawn, idle, and open animations
- **Random loot drops** (coins, crystals, health potions) with weighted probability
- **Item collection** with DoTween fly-to-target animations
- **UI indicators** for coins, crystals, and health bar
- **Object pooling** for drop items to minimize allocations
- **Quick-collect bonus** - collecting 3 identical items within a time window activates a temporary multiplier
- **Bonus UI** with timer bar and color-coded item type indicator
- **Automatic chest respawn** cycle after all drops are collected
- **Layered item rendering** - overlapping items resolve clicks to the topmost item

## Architecture

### Pattern: MVP (Model-View-Presenter)

| Layer | Responsibility |
|-------|---------------|
| **Model** | Pure C# classes. Hold state, data, and business logic. No Unity dependencies. |
| **View** | MonoBehaviour classes. Handle rendering, animations, and user input. Fire UnityEvents for user actions. |
| **Presenter** | Pure C# classes. Subscribe to View events and Model changes. Orchestrate interactions via services and EventBus. |

### Communication: EventBus

A static generic EventBus decouples modules. Struct-based events implement `IEvent`. Models fire events on state changes. Presenters and services subscribe to react.


### Dependency Injection: Zenject

All bindings configured in `MainInstaller`. Models, services, pools, and factories are singletons. Views are injected from scene/prefab instances. Presenters are created with `NonLazy` for automatic initialization.

## Technology Stack

| Technology | Purpose |
|------------|---------|
| **Zenject** | Dependency injection container |
| **DoTween** | Tweening animations (fly-to-target, spawn, fade, punch scale) |
| **UniTask** | Async bonus timer with cancellation support |
| **UnityEngine.Pool** | Not used. Custom `ObjectPool` for full control |
| **ScriptableObject** | Config data for chest drop tables, inventory defaults, bonus thresholds |
| **Unity Events** | View-to-Presenter communication within MVP pattern |

