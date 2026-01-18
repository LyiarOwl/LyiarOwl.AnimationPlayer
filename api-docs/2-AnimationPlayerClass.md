# [API Documentation](../API.md)

## AnimationPlayer (class)

Manages and controls animations.

---

### Constructors

| **Definition** | **Description** | **Note** |
|--|--|--|
| `AnimationPlayer()` | Creates a new animation manager instance. | |

---

### Public Methods

| **Definition** | **Description** | **Note** |
|--|--|--|
| `void Update()` | Updates the animation player and the currently active animation. | Must be called every frame. |
| `void Add(Animation animation)` | Adds an animation to the animation player. | If the animation already exists, the operation is ignored and a warning is printed. If this is the first animation added, it becomes the current animation automatically. |
| `void Remove(string name)` | Removes the animation with the specified `name`. | If no animation with this name exists, the operation is ignored and a warning is printed. |
| `void Play(string name, [bool backward = false])` | Plays the animation with the specified `name`. | If the animation does not exist, the operation is ignored and a warning is printed. If the animation is already playing, nothing happens. If `backward` is `true`, the animation plays in reverse. |
| `void Pause()` | Pauses the current animation. | If there is no animation, this operation is ignored. |
| `void Reset()` | Resets the current animation and all its keyframes. | If there is no animation, this operation is ignored. |
| `void Resume()` | Resumes the current animation if it was paused. | If there is no animation, this operation is ignored. |
| `void Stop()` | Stops the current animation and resets its state. | If there is no animation, this operation is ignored. This method fully resets the animation and its keyframes. |

---

### Public Properties

| **Definition** | **Description** | **Note** |
|--|--|--|
| `Animation CurrentAnimation` | The current animation being played or ready to be played (**get-only**). | May be `null` if no animations were added. |
| `bool IsPlaying` | Indicates whether an animation is currently playing (**get-only**). | `true` if playing, `false` otherwise. |
| `Action<string> OnAnimationEnd` | Action invoked when the current animation ends. | The `string` argument is the name of the animation that ended. |
| `float Speed` | Playback speed of the animation. | `0.0` (minimal) pauses the animation. Values between `0.0` and `1.0` slow it down. Values greater than `1.0` speed it up. |
