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
        public InterpolationKeyframe(Action<float> setter, float from, float to, TimeSpan begin, TimeSpan end)
        {
            _setter = setter;
            _from = from;
            _to = to;
            Begin = begin;
            End = end;
            _duration = end - begin;
        }
        public sealed override void Update()
        {
            _elapsed += AnimationPlayerCore.DeltaTime;
            double t = _elapsed / _duration.TotalSeconds;
            t = double.Clamp(t, 0d, 1d);

            float value = _from + (_to - _from) * (float)t;
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