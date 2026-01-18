# [API Documentation](../API.md)

## AnimationLoop (enum)

Defines how an animation behaves when it reaches its end.

---

### Values

| **Name** | **Description** |
|--|--|
| `NoLoop` | The animation plays only once and stops when it reaches the end. |
| `Loop` | The animation repeats continuously, restarting from the beginning after it ends. |
| `PingPong` | The animation repeats by alternating between forward and backward playback. |

---

### Notes

- `NoLoop` will trigger `OnAnimationEnd` when the animation finishes.
- `Loop` restarts the animation immediately after reaching the end.
- `PingPong` reverses the playback direction every time the animation reaches either end.
