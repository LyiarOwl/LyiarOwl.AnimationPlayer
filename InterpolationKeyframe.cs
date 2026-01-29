using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Interpolates between two numeric values during a time interval
    /// (or until the current animation ends).
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
        /// Creates a keyframe that interpolates a numeric value from one point to another.
        /// </summary>
        /// <param name="setter">
        /// Callback used to apply the interpolated value.
        /// The <c>float</c> parameter represents the current interpolation value.
        /// </param>
        /// <param name="from">Starting value of the interpolation.</param>
        /// <param name="to">Ending value of the interpolation.</param>
        /// <param name="begin">Time at which this keyframe starts.</param>
        /// <param name="end">Time at which this keyframe ends.</param>
        /// <param name="type">Interpolation type to be used.</param>
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
        /// <summary>
        /// Creates a keyframe that interpolates a numeric value from one point to another.
        /// </summary>
        /// <param name="setter">
        /// Callback used to apply the interpolated value.
        /// The <c>float</c> parameter represents the current interpolation value.
        /// </param>
        /// <param name="from">Starting value of the interpolation.</param>
        /// <param name="to">Ending value of the interpolation.</param>
        /// <param name="type">Interpolation type to be used.</param>
        public InterpolationKeyframe(Action<float> setter, float from, float to, InterpolationType type = InterpolationType.Linear)
        {
            _setter = setter;
            _from = from;
            _to = to;
            Begin = TimeSpan.Zero;
            End = TimeSpan.Zero;
            _duration = TimeSpan.Zero;
            _type = type;
        }
        /// <summary>
        /// Updates the interpolation value for the current frame.
        /// </summary>
        /// <param name="delta">Delta time of the current frame.</param>
        /// <param name="forward">
        /// Indicates whether the animation is playing forward or backward.
        /// </param>
        public sealed override void Update(float delta, bool forward)
        {
            _elapsed += delta;

            float value = 0f;
            float t = (float)(_elapsed * _invDuration);
            if (float.IsNaN(t) || float.IsInfinity(t))
                t = 0.0f;
            t = AnimationPlayerCore.Clamp(t, 0f, 1f);
            if (!forward)
                t = 1f - t;

            if (_type == InterpolationType.Linear)
                value = Interpolate(t);
            if (_type == InterpolationType.EaseInOut)
            {
                float easedT = t < 0.5f
                    ? 4f * t * t * t
                    : 1f - (float)Math.Pow(-2f * t + 2f, 3f) * 0.5f;
                value = Interpolate(easedT);
            }
            if (_type == InterpolationType.EaseIn)
            {
                float easedT = t * t * t;
                value = Interpolate(easedT);
            }
            if (_type == InterpolationType.EaseOut)
            {
                float easedT = 1f - (float)Math.Pow(1f - t, 3f);
                value = Interpolate(easedT);
            }

            _setter?.Invoke(value);
        }
        private float Interpolate(float t)
        {
            return _from + (_to - _from) * t;
        }

        /// <summary>
        /// Called when the animation attempts to enter this keyframe.
        /// </summary>
        /// <remarks>
        /// This method is responsible for preparing the keyframe state and
        /// may influence whether the keyframe actually becomes active.
        /// </remarks>
        public sealed override void Enter()
        {
            base.Enter();
            _elapsed = 0d;
        }

        /// <summary>
        /// Called when the animation leaves this keyframe.
        /// </summary>
        /// <remarks>
        /// This method is responsible for cleaning up or finalizing the keyframe state
        /// after it is no longer active.
        /// </remarks>
        public sealed override void Exit()
        {
            base.Exit();
            _setter?.Invoke(_to);
        }

        public void RecalculateDuration()
        {
            _duration = End - Begin;
        }
    }
}
