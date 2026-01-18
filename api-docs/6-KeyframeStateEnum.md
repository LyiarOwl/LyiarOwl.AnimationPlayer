# [API Documentation](../API.md)

## KeyframeState (enum)

Represents the current execution state of a keyframe.

This enum describes the lifecycle phase of a `Keyframe` during an animation
execution.

---

### Values

| **Value** | **Description** |
|--|--|
| `Idle` | The keyframe has not started yet and is not being updated. |
| `Active` | The keyframe is currently active and receiving update calls. |
| `Finished` | The keyframe has completed its execution and will not run again unless reset. |

---

### Lifecycle Overview

A `Keyframe` transitions between states in the following order:


- A keyframe starts in the `Idle` state.
- When the animation time enters the `[Begin, End)` interval, the keyframe becomes `Active`.
- Once the animation time leaves this interval, the keyframe transitions to `Finished`.
- Calling `Reset()` returns the keyframe to the `Idle` state.

---

### Notes

- The `State` property of a `Keyframe` is read-only and managed internally.
- User code should **not** manually modify the state.
- State transitions are handled automatically by the animation update logic.

