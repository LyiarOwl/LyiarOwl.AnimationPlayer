using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// This keyframe will interpolate between two values during the interval (or until the current animation ends).
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
