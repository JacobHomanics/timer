# Supercharged Vector2 Framework Documentation

## Table of Contents

- [Overview](#overview)
- [Components](#components)
  - [Vector2 Class](#vector2-class)
  - [Health Class](#health-class)
  - [Timer Class](#timer-class)
- [Events](#events)
  - [Vector2 Events](#vector2-events)
  - [Health Events](#health-events)
  - [Timer Events](#timer-events)
- [Usage Examples](#usage-examples)
  - [Vector2](#vector2)
  - [Health](#health)
  - [Timer](#timer)

## Overview

The Supercharged Vector2 Framework provides an advanced implementation of Unity's `Vector2` structure with added functionality, including:

- Event-driven programming with UnityEvents.
- Helper methods for manipulating and querying `Vector2` values.
- Specialized components for health management and timers using `Vector2` values.

## Components

### Vector2 Class

**Namespace:** `JacobHomanics.Core.SuperchargedVector2`

The `Vector2` class is a core component that extends Unity's `Vector2` with additional functionality.

#### Properties

- `float X` - Represents the x-component of the vector.
- `float Y` - Represents the y-component of the vector.

#### Methods

- `void OffsetX(float value)` - Adds a value to the x-component.
- `void OffsetY(float value)` - Adds a value to the y-component.
- `float GetDifferenceXY()` - Returns the difference between X and Y.
- `float GetDifferenceYX()` - Returns the difference between Y and X.
- `bool IsXEqualToY()` - Checks if X equals Y.
- `bool IsXZero()` - Checks if X is zero.
- `bool IsYZero()` - Checks if Y is zero.

#### Context Menu Options

- Add/Subtract predefined values to/from X.

### Health Class

**Namespace:** `JacobHomanics.Core.SuperchargedVector2`

The `Health` class provides a health management system using the `Vector2` class.

#### Properties

- `float Current` - Represents the current health (mapped to `X` in `Vector2`).
- `float Max` - Represents the maximum health (mapped to `Y` in `Vector2`).

#### Methods

- `void Decrease(float amount)` - Decreases the current health.
- `void Increase(float amount)` - Increases the current health.
- `void Restore()` - Restores health to maximum.
- `bool IsMaxHealth()` - Checks if health is at its maximum.
- `bool IsZero()` - Checks if health is zero.

#### Context Menu Options

- Damage or heal by predefined values.

### Timer Class

**Namespace:** `JacobHomanics.Core.SuperchargedVector2`

The `Timer` class provides a timing system using the `Vector2` class.

#### Properties

- `float ElapsedTime` - Represents the elapsed time (mapped to `X` in `Vector2`).
- `float Duration` - Represents the timer duration (mapped to `Y` in `Vector2`).

#### Methods

- `void Tick(float delta)` - Updates the elapsed time.
- `float GetTimeLeft()` - Returns the remaining time.
- `bool IsDurationReached()` - Checks if the timer duration has been reached.
- `void ResetElapsedTime()` - Resets the elapsed time.

#### Tick Types

- `DeltaTime`
- `UnscaledDeltaTime`
- `SmoothDeltaTime`
- `FixedDeltaTime`
- `FixedUnscaledDeltaTime`

## Events

### Vector2 Events

- `UnityEvent OnXSet` - Triggered whenever `X` is set.
- `UnityEvent OnXIncrease` - Triggered whenever `X` increases.
- `UnityEvent OnXDecrease` - Triggered whenever `X` decreases.
- `UnityEvent OnYSet` - Triggered whenever `Y` is set.
- `UnityEvent OnYIncrease` - Triggered whenever `Y` increases.
- `UnityEvent OnYDecrease` - Triggered whenever `Y` decreases.

### Health Events

- `UnityEvent OnHealthIncrease` - Triggered when health increases.
- `UnityEvent OnHealthDecrease` - Triggered when health decreases.
- `UnityEvent OnDeath` - Triggered when health reaches zero.
- `UnityEvent OnHealthSetToMax` - Triggered when health is fully restored.

### Timer Events

- `UnityEvent OnTick` - Triggered every tick.
- `UnityEvent OnDurationElapsed` - Triggered when the duration is reached.
- `UnityEvent OnElapsedTimeReset` - Triggered when elapsed time is reset.

## Usage Examples

### Vector2

```csharp
var vector2 = new Vector2();
vector2.X = 10;
vector2.Y = 20;
Debug.Log(vector2.GetDifferenceXY()); // Outputs -10
```

### Health

```csharp
var health = gameObject.AddComponent<Health>();
health.Max = 100;
health.Current = 50;
health.Increase(10);
Debug.Log(health.IsMaxHealth()); // Outputs false
```

### Timer

```csharp
var timer = gameObject.AddComponent<Timer>();
timer.Duration = 5f;
timer.Tick(1f);
Debug.Log(timer.GetTimeLeft()); // Outputs 4
```
