# [API Documentation](../API.md)

## AnimationPlayerCore (class)

Core utilities required for the animation system to work properly.  
This class is responsible for providing timing information and internal diagnostic helpers.

---


### Public Properties

| **Definition** | **Description** | **Note** |
| --- | --- | --- |
| `static float DeltaTime` | Delta time (in seconds) of the current frame. | Read-only. Updated internally via `Update(GameTime)` and used by all time-dependent components. |

---

### Public Methods

| **Definition** | **Description** | **Note** |
| --- | --- | --- |
| `static void Update(GameTime time)` | Updates the internal `DeltaTime` value used by all time-based systems. | **Must be called once per frame**, globally. If not called, animations will not update correctly. |
| `static string CreateMessage<T>(string methodName, string message, string errorType = "")` | Creates a formatted diagnostic message associated with a specific class and method. | Intended for internal logging and non-critical diagnostics. |
| `static string CreateWarningMessage<T>(string methodName, string message)` | Creates a formatted warning message with a `[WARN]` prefix. | Convenience wrapper around `CreateMessage`. |

---

### Notes

- `AnimationPlayerCore` acts as a **global dependency** for the animation system.
- The `DeltaTime` property replaces the need to manually propagate `GameTime` through all animation-related classes.
- Diagnostic helpers are meant for **non-critical issues** that do not justify throwing exceptions.
