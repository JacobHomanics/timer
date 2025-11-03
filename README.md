# Timer Package

A flexible and easy-to-use timer system for Unity with built-in UI components and visual feedback.

## Features

- **Simple Timer Component**: Track elapsed time with configurable duration
- **Multiple Tick Types**: Support for `DeltaTime`, `UnscaledDeltaTime`, `SmoothDeltaTime`, `FixedDeltaTime`, and `FixedUnscaledDeltaTime`
- **Unity Events**: Built-in events for `OnTick` and `OnDurationReached`
- **UI Components**: Ready-to-use components for displaying timer values
  - **TimerText**: Display timer values as text (Duration, Elapsed Time, or Time Left)
  - **TimerImage**: Visual progress bar using Unity Image fillAmount
  - **TimerSlider**: Visual progress bar using Unity Slider
- **Color Feedback**: Automatic color changes when duration is reached
  - **TextColor**: Changes text color when timer completes
  - **ImageColor**: Changes image color when timer completes
  - **SliderColor**: Changes slider fill color when timer completes
- **Custom Editor**: Visual timer bar in the Unity Inspector with quick reset buttons

## Requirements

- Unity 6000.0.31f1 or later
- Unity UI (com.unity.ugui) 2.0.0 or later

## Installation

Add this package to your Unity project via the Package Manager using the git URL, or place it in your `Packages` folder.

## Getting Started

### Basic Timer Setup

1. Add a `Timer` component to any GameObject
2. Set the `Duration` in seconds
3. Choose a `Tick Type` (default is `DeltaTime`)
4. Optionally hook up Unity Events:
   - `OnTick`: Fires every frame while the timer is running
   - `OnDurationReached`: Fires when elapsed time reaches or exceeds the duration

### Example: Simple Countdown

```csharp
using JacobHomanics.TimerSystem;
using UnityEngine;

public class CountdownExample : MonoBehaviour
{
    public Timer timer;

    void Start()
    {
        timer.Duration = 10f; // 10 second countdown
        timer.ElapsedTime = 0f; // Start at zero

        // Subscribe to events
        timer.OnDurationReached.AddListener(OnCountdownComplete);
    }

    void OnCountdownComplete()
    {
        Debug.Log("Countdown finished!");
    }
}
```

## Components

### Timer

The core timer component that tracks elapsed time and duration.

**Properties:**

- `Duration` (float): The target duration in seconds
- `ElapsedTime` (float): Current elapsed time in seconds
- `TickType` (enum): The time source to use for ticking
  - `DeltaTime`: Standard time.deltaTime (affected by time scale)
  - `UnscaledDeltaTime`: Time.unscaledDeltaTime (not affected by time scale)
  - `SmoothDeltaTime`: Time.smoothDeltaTime
  - `FixedDeltaTime`: Time.fixedDeltaTime
  - `FixedUnscaledDeltaTime`: Time.fixedUnscaledDeltaTime

**Events:**

- `OnTick`: Invoked every frame while the timer is running
- `OnDurationReached`: Invoked when `ElapsedTime >= Duration`

**Methods:**

- `Tick(float delta)`: Manually advance the timer by a delta value
- `GetTimeLeft()`: Returns `Duration - ElapsedTime`
- `IsDurationReached()`: Returns `true` if elapsed time has reached or exceeded duration

### TimerText

Displays timer values as formatted text using TextMeshPro.

**Properties:**

- `timer` (Timer): Reference to the Timer component
- `text` (TMP_Text): The TextMeshPro text component to update
- `displayType` (enum): What value to display
  - `Duration`: Shows the timer's duration
  - `ElapsedTime`: Shows the elapsed time
  - `TimeLeft`: Shows the remaining time
- `format` (string): Number format string (default: "#,##0.####################")
- `clampToZero` (bool): Clamp displayed value to minimum of 0
- `clampToMax` (bool): Clamp displayed value to maximum of Duration
- `roundUp` (bool): Round values up using `Mathf.Ceil`

**Usage:**

1. Add `TimerText` component to a GameObject with a TMP_Text component
2. Assign the `Timer` reference
3. Assign the `text` reference
4. Choose the `displayType` you want to show

### TimerImage

Displays timer progress using Unity Image fillAmount.

**Properties:**

- `timer` (Timer): Reference to the Timer component
- `image` (Image): The Unity Image component to update
- `reverseFill` (bool): If true, fill decreases as time progresses (countdown style)

**Usage:**

1. Add `TimerImage` component to a GameObject with an Image component
2. Ensure the Image component has `Image Type` set to `Filled`
3. Assign the `Timer` and `image` references
4. Set `reverseFill` based on your desired visual style

### TimerSlider

Displays timer progress using Unity Slider.

**Properties:**

- `timer` (Timer): Reference to the Timer component
- `slider` (Slider): The Unity Slider component to update
- `reverseFill` (bool): If true, slider value decreases as time progresses

**Usage:**

1. Add `TimerSlider` component to a GameObject with a Slider component
2. Assign the `Timer` and `slider` references
3. Set `reverseFill` based on your desired visual style

### Color Components

These components automatically change colors when the timer duration is reached.

#### TextColor

Changes the color of TextMeshPro text when the timer completes.

**Properties:**

- `timer` (Timer): Reference to the Timer component
- `text` (TMP_Text): The TextMeshPro text component
- `colorOnDurationReached` (Color): Color to use when duration is reached

#### ImageColor

Changes the color of an Image component when the timer completes.

**Properties:**

- `timer` (Timer): Reference to the Timer component
- `image` (Image): The Image component
- `colorOnDurationReached` (Color): Color to use when duration is reached

#### SliderColor

Changes the color of a Slider's fill or background when the timer completes.

**Properties:**

- `timer` (Timer): Reference to the Timer component
- `slider` (Slider): The Slider component
- `changeFillRect` (bool): If true, changes the fill color; if false, changes the background color
- `colorOnDurationReached` (Color): Color to use when duration is reached

## Inspector Features

The custom editor provides:

- **Visual Timer Bar**: See timer progress at a glance in the Inspector
- **Interactive Sliders**: Adjust elapsed time and time left directly in the Inspector
- **Quick Actions**: Reset to 0 or set to duration with one click
- **Duration Reached Indicator**: Visual feedback when the timer has completed

## Examples

### Complete UI Timer Setup

1. Create a GameObject with a `Timer` component
2. Set duration to 30 seconds
3. Create a UI Canvas with:
   - A TextMeshPro text component with `TimerText` showing "Time Left"
   - An Image with `TimerImage` showing progress
   - A Slider with `TimerSlider` showing progress
   - Add `TextColor` to change text color when timer completes

### Unscaled Time Timer

For a timer that continues even when the game is paused:

```csharp
timer.tickType = Timer.TickType.UnscaledDeltaTime;
```

### Manual Timer Control

For cases where you want to manually control the timer:

```csharp
// Disable the Timer component
timer.enabled = false;

// Manually tick
timer.Tick(0.5f); // Advance by 0.5 seconds
```

## License

See LICENSE.md for license information.

## Author

Jacob Homanics

- Email: homanicsjake@gmail.com
- Website: jacobhomanics.com
