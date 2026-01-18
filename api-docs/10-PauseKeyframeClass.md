# [API Documentation](../API.md)

## PauseKeyframe (class)

Pauses the execution flow of an animation until a user-defined condition
is satisfied.

A `PauseKeyframe` behaves like a regular `Keyframe`, but it can **block
animation progression** while its condition evaluates to `false`.

---

### Inheritance

- [Keyframe](5-KeyframeClass.md)

---

### Constructors

| **Definition** | **Description** | **Note** |
|--|--|--|
| `PauseKeyframe(Action onUpdate, Func<bool> condition, TimeSpan begin, TimeSpan end, [bool runOnce = true])` | Creates a pause keyframe with an update callback and a condition. | While `Condition` returns `false`, the animation remains paused at this keyframe. |
| `PauseKeyframe(Action onEnter, Action onUpdate, Action onExit, Func<bool> condition, TimeSpan begin, TimeSpan end, [bool runOnce = true])` | Creates a pause keyframe with enter, update, and exit callbacks. | Enter and exit callbacks behave like a regular `Keyframe`, but execution is blocked until the condition is met. |

---

### Public Fields

| **Definition** | **Description** |
|--|--|
| `Func<bool> Condition` | Condition that must return `true` for the animation to proceed past this keyframe. |

---

### Behavior

- When the animation reaches a `PauseKeyframe`, execution **stops advancing in time**.
- The keyframe remains active while `Condition()` returns `false`.
- As soon as `Condition()` returns `true`, the animation continues normally.
- Update callbacks may be executed once or continuously, depending on `runOnce`.

---

### Usage Notes

- Ideal for synchronizing animations with **game events**, **input**, or **state changes**.
- The condition is evaluated every update while the keyframe is active.
- Avoid expensive logic inside the condition function.

---

### Example Use Cases

- Wait for player input before continuing an animation.
- Pause an animation until a resource finishes loading.
- Synchronize cutscene timing with gameplay logic.

---

### Example

```csharp
new PauseKeyframe(
    onUpdate: () => ShowPressAnyKeyPrompt(),
    condition: () => Input.AnyKeyPressed,
    begin: TimeSpan.FromSeconds(1),
    end: TimeSpan.FromSeconds(2)
);
