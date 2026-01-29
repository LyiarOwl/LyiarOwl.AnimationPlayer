## 0.4.0 (Stable)
- **BREAKING**: Removed MonoGame `GameTime` dependency.
- Added `Update(float)` and `Update(TimeSpan)` to `AnimationPlayerCore`.
- `AnimationPlayer` is now fully engine-agnostic.
- `InterpolationKeyframe` now works correctly with `AutoDistribute`.
  - New overload added to `InterpolationKeyframe`
- Improved compatibility with Unity, FNA and legacy .NET runtimes.
- Module is now `netstandard2.0`.


### Internal & Safety Improvements
- `AnimationPlayerCore.Update` now sanitizes `deltaTime`:
  - `NaN`, `Infinity`, or negative values are clamped to `0.0`.
- All internal `Console.WriteLine` warnings are now wrapped in preprocessor macros (`#if DEBUG`), eliminating overhead in release builds.
- `Animation.Keyframes` storage changed from `Keyframe[]` to `List<Keyframe` for improved flexibility and internal consistency.


## 0.3.1
- The `Add` method now prints the name of the animation if it has already been added.

## 0.3.0
- Added `Stop` method to the animation API.
- Improved and expanded API documentation.
- Documentation fixes and clarifications across public methods.

## 0.2.0
- `SortKeyframesByDuration` renamed to `AutoDistribute`.

## 0.1.0
- Initial public release.
