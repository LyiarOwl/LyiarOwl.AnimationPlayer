# [API Documentation](../API.md)

## Keyframe (class)

Base class for all keyframes.

A keyframe represents a logical time interval inside an animation and can react
to entering, updating, and exiting that interval.

---

### Constructors

| **Definition** | **Description** | **Note** |
|--|--|--|
| `Keyframe()` | Creates an empty keyframe. | Callbacks can be assigned manually after creation. |
| `Keyframe(Action onUpdate, bool runOnce)` | Creates a keyframe with an update callback. | If `runOnce` is `true`, `onUpdate` runs only once when the keyframe starts. |
| `Keyframe(Action onEnter, Action onUpdate, Action onExit, bool runOnce)` | Creates a keyframe with full lifecycle callbacks. | Useful for complex behaviors that require setup and cleanup. |

---

### Public Methods

| **Definition** | **Description** | **Note** |
|--|--|--|
| `virtual void Enter()` | Called when the animation attempts to enter this keyframe. | Responsible for preparing internal state and triggering `OnEnter`. |
| `virtual void Update(float delta)` | Updates this keyframe. | Controls execution of `OnUpdate` based on the keyframe configuration. |
| `virtual void Update(float delta, bool forward)` | Updates this keyframe considering animation direction. | By default, direction is ignored. Override if needed. |
| `virtual void Exit()` | Called when the animation leaves this keyframe. | Responsible for finalization and triggering `OnExit`. |
| `void Reset()` | Resets this keyframe to its initial state. | Allows the keyframe to be reused in future animation runs. |

---

### Public Properties

| **Definition** | **Description** | **Note** |
|--|--|--|
| `TimeSpan Begin` | Time at which this keyframe becomes eligible to run. | Inclusive. |
| `TimeSpan End` | Time at which this keyframe is no longer eligible to run. | Exclusive. |
| `KeyframeState State` | Current execution state of this keyframe. | Read-only. |

---

### Public Callbacks

| **Callback** | **Description** | **When it is called** |
|--|--|--|
| `Action OnEnter` | Callback executed when entering this keyframe. | Triggered once when the keyframe becomes active. |
| `Action OnUpdate` | Callback executed while this keyframe is active. | Triggered every update or once, depending on configuration. |
| `Action OnExit` | Callback executed when exiting this keyframe. | Triggered once when the keyframe stops being active. |

---

### Lifecycle

A keyframe follows a simple lifecycle:

1. **Idle**  
   The keyframe has not yet been entered.

2. **Active**  
   The animation time is within the `[Begin, End)` interval and the keyframe is executing.

3. **Finished**  
   The keyframe has exited and will not execute again until reset.

---

### Notes

- Keyframes are **stateless by default**, but subclasses can store custom state.
- Direction-aware behavior can be implemented by overriding  
  `Update(float delta, bool forward)`.
- The `Reset` method is automatically called by `Animation.Enter`, but can also
  be invoked manually.
- This class is designed to be extended to implement specialized keyframe behaviors.
