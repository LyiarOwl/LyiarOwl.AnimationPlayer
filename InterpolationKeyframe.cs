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
        private InterpolationType _type;
        public InterpolationKeyframe(Action<float> setter, float from, float to, TimeSpan begin, TimeSpan end, InterpolationType type = InterpolationType.Linear)
        {
            _setter = setter;
            _from = from;
            _to = to;
            _begin = begin;
            _end = end;
            _duration = end - begin;
            _type = type;
        }
        public sealed override void Update()
        {
            _elapsed += AnimationPlayerCore.DeltaTime;
            float value = 0f;
            double t = _elapsed / _duration.TotalSeconds;
            t = double.Clamp(t, 0d, 1d);
            if (_type == InterpolationType.Linear)
            {
                value = _from + (_to - _from) * (float)t;
            }
            _setter?.Invoke(value);
        }

        public sealed override void Enter()
        {
            _elapsed = 0d;
            base.Enter();
        }

        public sealed override void Exit()
        {
            base.Exit();
            _setter?.Invoke(_to);
        }
    }
}
