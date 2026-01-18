# [API Documentation](../API.md)

## IntervalKeyframe (class)

Executes callback functions during a defined time interval
(or until the current animation ends).

This class extends `Keyframe` by explicitly defining a time range
in which the keyframe is active.

---

### Inheritance

- [Keyframe](5-KeyframeClass.md)

---

### Constructors

| **Definition** | **Description** | **Note** |
|--|--|--|
| `IntervalKeyframe(Action onUpdate, bool runOnce = true)` | Creates a keyframe that runs during its assigned interval. | If `runOnce` is `true`, `onUpdate` is executed only once when the keyframe starts. |
| `IntervalKeyframe(Action onUpdate, TimeSpan begin, TimeSpan end, bool runOnce = true)` | Creates a keyframe with a defined begin and end time. | The keyframe will be active only within `[begin, end)`. |
| `IntervalKeyframe(Action onEnter, Action onUpdate, Action onExit, bool runOnce = true)` | Creates a keyframe with enter, update, and exit callbacks. | Timing must be assigned manually or via auto-distribution. |
| `IntervalKeyframe(Action onEnter, Action onUpdate, Action onExit, TimeSpan begin, TimeSpan end, bool runOnce = true)` | Creates a fully-defined keyframe with callbacks and timing. | Most explicit and controllable constructor. |

---

### Static Methods

| **Definition** | **Description** | **Note** |
|--|--|--|
| `static Animation AutoDistribute(Animation animation)` | Automatically distributes all keyframes of an animation across its duration. | Keyframes are spaced sequentially and evenly. |

---

### Behavior

- An `IntervalKeyframe` becomes active when the animation time enters its interval.
- While active:
  - `OnEnter` is called once when the interval starts.
  - `OnUpdate` is called either once or continuously, depending on `runOnce`.
  - `OnExit` is called once when the interval ends.
- When the animation leaves the interval, the keyframe transitions to `Finished`.

---

### Auto Distribution

`AutoDistribute` assigns time ranges to all keyframes in an animation by:

- Dividing the animation duration evenly by the number of keyframes.
- Assigning each keyframe a contiguous, non-overlapping interval.
- Preserving the original order of the keyframes.

This is useful when:
- Keyframes are sequential.
- Precise timing is not important.
- You want quick setup with minimal configuration.

---

### Notes

- `IntervalKeyframe` does not manage animation playback by itself.
- Timing can be defined manually or generated automatically.
- Calling `Reset()` on the animation will reset all interval keyframes.
