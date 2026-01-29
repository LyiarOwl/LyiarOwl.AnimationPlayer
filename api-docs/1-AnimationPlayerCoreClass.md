# [API Documentation](../API.md)

## AnimationPlayerCore (class)

Core utilities required for the animation system to work properly.  
This class is responsible for providing timing information and internal diagnostic helpers.

---

### Public Properties

| **Definition** | **Description** | **Note** |
| --- | --- | --- |
| `static float DeltaTime` | Delta time (in seconds) of the current frame. | Read-only. Updated internally via `Update(TimeSpan)` or `Update(float)` and used by all time-dependent components. |

---

### Public Methods

| **Definition** | **Description** | **Note** |
| --- | --- | --- |
| `static void Update(TimeSpan time)` | Updates the internal `DeltaTime` value using a `TimeSpan`. | **Must be called once per frame**, globally. |
| `static void Update(float deltaTime)` | Updates the internal `DeltaTime` value using a raw delta time (in seconds). | Values that are `NaN`, `Infinity`, or negative are sanitized to `0.0`. |
| `static string CreateMessage<T>(string methodName, string message, string errorType = "")` | Creates a formatted diagnostic message associated with a specific class and method. | Intended for internal logging and non-critical diagnostics. |
| `static string CreateWarningMessage<T>(string methodName, string message)` | Creates a formatted warning message with a `[WARN]` prefix. | Convenience wrapper around `CreateMessage`. |

---

### Notes

- `AnimationPlayerCore` acts as a **global dependency** for the animation system.
- The `DeltaTime` property replaces the need to manually propagate engine-specific timing objects.
- The `Update` method **must be called once per frame**; otherwise, animations will not advance.
- Invalid `deltaTime` values (`NaN`, `Infinity`, or negative) are automatically clamped to `0.0` to ensure stability.
- Diagnostic helpers are intended for **non-critical issues** and are silenced in release builds.
