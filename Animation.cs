using System;

namespace LyiarOwl.AnimationPlayer
{
    public class Animation
    {
        private double _elapsed;
        private TimeSpan _duration;
        private bool _started;
        public readonly string Name;
        public readonly Keyframe[] Keyframes;
        public event Func<string, string> OnAnimationEnd;
        public bool Loop;
        public Animation(string name, Keyframe[] keyframes, TimeSpan duration, bool loop = true)
        {
            Name = name;
            Keyframes = keyframes;
            _duration = duration;
            Loop = loop;
        }
        public void Update()
        {
            if (!_started) return;

            _elapsed += AnimationPlayerCore.DeltaTime;
            foreach (var keyframe in Keyframes)
            {
                bool insideInterval = _elapsed >= keyframe.Begin.TotalSeconds &&
                    _elapsed <= keyframe.End.TotalSeconds;

                /* enter keyframe */
                if (insideInterval && keyframe.State == KeyframeState.Idle)
                {
                    keyframe.Enter();
                }

                /* update keyframe */
                if (insideInterval && keyframe.State == KeyframeState.Active)
                    keyframe.Update();

                /* exit keyframe */
                if (!insideInterval && keyframe.State == KeyframeState.Active)
                    keyframe.Exit();
            }

            /* animation end */
            if (_elapsed >= _duration.TotalSeconds)
            {
                OnAnimationEnd?.Invoke(Name);

                if (Loop)
                {
                    _elapsed -= _duration.TotalSeconds;
                    foreach (var kf in Keyframes)
                        kf.Reset();

                    return;
                }

                _started = false;
            }
        }
        public void Enter()
        {
            _elapsed = 0.0;
            _started = true;

            foreach (var kf in Keyframes)
                kf.Reset();
        }
        public void Exit()
        {
            _started = false;
        }
        public bool IsFinished() => !_started;
    }
}