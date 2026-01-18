# [API Documentation](../API.md)

## InterpolationType (enum)

Defines the interpolation curve used to calculate intermediate values
during an `InterpolationKeyframe`.

Interpolation types control **how the value progresses over time**,
affecting the perceived motion and smoothness of animations.

---

### Values

| **Name** | **Description** |
|--|--|
| `Linear` | Interpolates with constant speed from start to end. |
| `EaseInOut` | Smooth interpolation that accelerates at the beginning and decelerates at the end. |
| `EaseIn` | Interpolation that starts slowly and accelerates over time. |
| `EaseOut` | Interpolation that starts quickly and decelerates over time. |

---

### Behavior Overview

- Interpolation curves affect **only the progression of time**, not the final values.
- All interpolation types map a normalized time value (`0 → 1`) to another normalized value.
- The resulting value is then used to interpolate between `from` and `to`.

---

### Visual Intuition

- **Linear**  
  Steady, mechanical motion.

- **EaseIn**  
  Starts slow, builds momentum.

- **EaseOut**  
  Starts fast, smoothly comes to rest.

- **EaseInOut**  
  Natural, smooth motion — ideal for most transitions.

---

### Usage Notes

- Commonly used with [InterpolationKeyframe](8-InterpolationKeyframeClass.md).
- Choosing the right interpolation type greatly improves animation feel.
- `EaseInOut` is usually a safe default for natural-looking animations.

---

### Example Use Cases

- UI transitions and fades → `EaseInOut`
- Anticipation or wind-up motions → `EaseIn`
- Deceleration or settling effects → `EaseOut`
- Technical or time-accurate animations → `Linear`
