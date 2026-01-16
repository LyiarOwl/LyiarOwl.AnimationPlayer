using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Interpolates between two values during an interval (or until the current animation ends).
    /// </summary>
    public class InterpolationKeyframe : Keyframe
    {
        private double _elapsed;
        private Action<float> _setter;
        private float _from;
        private float _to;
        private TimeSpan _duration;
        private double _invDuration => 1.0 / _duration.TotalSeconds;
        private InterpolationType _type;
        /// <summary>
        /// Creates a keyframe that interpolation any numerical value from a point to another.
        /// </summary>
        /// <param name="setter">The <c>float</c> parameter is the current interpolation value.</param>
        /// <param name="from">Where the interpolation should start.</param>
        /// <param name="to">Where the interpolation should end.</param>
        /// <param name="begin">Where/When this keyframe should start.</param>
        /// <param name="end">Where/When this keyframe should end.</param>
        /// <param name="type">Type of the interpolation.</param>
        public InterpolationKeyframe(Action<float> setter, float from, float to, TimeSpan begin, TimeSpan end, InterpolationType type = InterpolationType.Linear)
        {
            _setter = setter;
            _from = from;
            _to = to;
            Begin = begin;
            End = end;
            _duration = end - begin;
            _type = type;
        }
        public sealed override void Update(float delta, bool forward)
        {
            _elapsed += delta;

            float value = 0f;
            float t = (float)(_elapsed * _invDuration);
            t = float.Clamp(t, 0f, 1f);
            if (!forward)
                t = 1f - t;

            if (_type == InterpolationType.Linear)
                value = Interpolate(t);
            if (_type == InterpolationType.EaseInOut)
            {
                float easedT = t < 0.5f
                    ? 4f * t * t * t
                    : 1f - float.Pow(-2f * t + 2f, 3f) * 0.5f;
                value = Interpolate(easedT);
            }
            if (_type == InterpolationType.EaseIn)
            {
                float easedT = t * t * t;
                value = Interpolate(easedT);
            }
            if (_type == InterpolationType.EaseOut)
            {
                float easedT = 1f - float.Pow(1f - t, 3f);
                value = Interpolate(easedT);
            }

            _setter?.Invoke(value);
        }
        private float Interpolate(float t)
        {
            return _from + (_to - _from) * t;
        }

        public sealed override void Enter()
        {
            base.Enter();
            _elapsed = 0d;
        }

        public sealed override void Exit()
        {
            base.Exit();
            _setter?.Invoke(_to);
        }
    }
}
