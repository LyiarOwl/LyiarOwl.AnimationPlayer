# [API Documentation](../API.md)

## Animation (class)

Represents a time-based animation composed of keyframes and driven by callback functions.

---

### Constructors

| **Definition** | **Description** | **Note** |
|--|--|--|
| `Animation(string name, Keyframe[] keyframes, TimeSpan duration, [AnimationLoop loop = NoLoop])` | Creates a new animation instance. | The animation does not start automatically. Use `AnimationPlayer.Play` or call `Enter` manually. |

---

### Public Methods

| **Definition** | **Description** | **Note** |
|--|--|--|
| `void Update(float delta)` | Updates all keyframes of the animation based on the elapsed time. | Usually called internally by `AnimationPlayer`. |
| `void Enter()` | Prepares and starts the animation. | Resets the state of all keyframes and starts playback. |
| `void Exit()` | Stops the animation. | Does **not** reset the state of the keyframes. |
| `bool IsFinished()` | Indicates whether the animation has stopped. | Returns `true` when the animation is no longer playing. |
| `void SetPlayDirection(bool forward)` | Sets the playback direction of the animation. | If `false`, the animation will play from end to beginning. |

---

### Public Properties

| **Definition** | **Description** | **Note** |
|--|--|--|
| `string Name` | Name of this animation. | Read-only. |
| `Keyframe[] Keyframes` | Keyframes that compose this animation. | Order matters; keyframes are evaluated based on time. |
| `TimeSpan Duration` | Total duration of the animation. | Read-only. |
| `AnimationLoop Loop` | Defines how the animation behaves when it reaches the end. | See [AnimationLoop](3-AnimationLoopEnum.md) for details. |
| `Action<string> OnAnimationEnd` | Event triggered when the animation ends. | The `string` parameter is the animation name. |

---

### Notes

- An animation is **time-driven**, not frame-driven.
- The animation itself does not update automatically; it must be updated manually or through `AnimationPlayer`.
- Keyframes are responsible for defining behavior during specific time intervals.
- The final state of the animation persists after it finishes unless explicitly reset.
