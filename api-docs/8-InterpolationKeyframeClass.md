# [API Documentation](../API.md)

## InterpolationKeyframe (class)

Interpolates a numeric value between two points during a defined time interval
(or until the current animation ends).

This keyframe is useful for smoothly animating values such as position,
scale, rotation, opacity, or any other numeric property.

---

### Inheritance

- [Keyframe](5-KeyframeClass.md)

---

### Constructors

| **Definition** | **Description** | **Note** |
|--|--|--|
| `InterpolationKeyframe(Action<float> setter, float from, float to, TimeSpan begin, TimeSpan end, InterpolationType type = InterpolationType.Linear)` | Creates a keyframe that interpolates a value from `from` to `to` over a time interval. | The `setter` callback is called every update with the interpolated value. |

---

### Overridden Methods

| **Definition** | **Description** | **Note** |
|--|--|--|
| `override void Update(float delta, bool forward)` | Updates the interpolation based on elapsed time and playback direction. | Supports forward and backward playback. |
| `override void Enter()` | Prepares the interpolation when the keyframe becomes active. | Initializes internal timing state. |
| `override void Exit()` | Finalizes the interpolation when the keyframe ends. | Ensures the final value is applied correctly. |

---

### Behavior

- When the animation enters the keyframe:
  - Internal elapsed time is initialized.
  - The keyframe becomes active.
- While active:
  - A normalized interpolation factor `t` is calculated.
  - The interpolation function is applied based on `InterpolationType`.
  - The resulting value is passed to the `setter` callback.
- When playing backward:
  - The interpolation direction is inverted automatically.
- When the animation leaves the keyframe:
  - The keyframe is finalized and marked as finished.

---

### Interpolation Types

The interpolation curve is defined by `InterpolationType`.

Examples may include:
- Linear
- EaseIn
- EaseOut
- EaseInOut  
(depending on the enum implementation)

Each type modifies how the interpolation factor `t` evolves over time.

---

### Notes

- [InterpolationKeyframe](9-InterpolationTypeEnum.md) overrides direction-aware update logic.
- Unlike basic keyframes, it maintains its own internal elapsed time.
- Designed to work correctly with:
  - Backward playback
  - Ping-pong animations
  - Variable animation speed
- Ideal for continuous, smooth transitions.
- When using `AutoDistribute`, `begin` and `end` values are ignored and automatically recalculated.

---

### Common Use Cases

- Animating transform properties (position, scale, rotation).
- Fading values (opacity, volume, intensity).
- Smooth transitions between states without manual timing logic.
