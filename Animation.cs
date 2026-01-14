using System;

namespace LyiarOwl.AnimationPlayer
{
    public class Animation
    {
        private double _elapsed;
        private TimeSpan _duration;
        private bool _started;
        private bool _playingForward = true;
        public readonly string Name;
        public readonly Keyframe[] Keyframes;
        public event Func<string, string> OnAnimationEnd;
        public AnimationLoop Loop;
        public Animation(string name, Keyframe[] keyframes, TimeSpan duration, AnimationLoop loop = AnimationLoop.NoLoop)
        {
            Name = name;
            Keyframes = keyframes;
            _duration = duration;
            Loop = loop;
        }
        private void UpdateKeyframe(Keyframe keyframe)
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
        public void Update()
        {
            if (!_started) return;

            if (Loop == AnimationLoop.PingPong)
            {
                if (_playingForward)
                {
                    _elapsed += AnimationPlayerCore.DeltaTime;
                    foreach (var keyframe in Keyframes)
                        UpdateKeyframe(keyframe);
                }
                else
                {
                    _elapsed -= AnimationPlayerCore.DeltaTime;
                    for (int i = Keyframes.Length - 1; i > 0; i--)
                        UpdateKeyframe(Keyframes[i]);
                }

                if (_elapsed >= _duration.TotalSeconds || _elapsed <= 0f)
                {
                    _playingForward = !_playingForward;
                    foreach (var kf in Keyframes)
                        kf.Reset();
                }
                return;
            }


            _elapsed += AnimationPlayerCore.DeltaTime;
            foreach (var keyframe in Keyframes)
                UpdateKeyframe(keyframe);
            if (_elapsed >= _duration.TotalSeconds)
            {
                if (Loop == AnimationLoop.Loop)
                {
                    _elapsed -= _duration.TotalSeconds;
                    foreach (var kf in Keyframes)
                        kf.Reset();
                    return;
                }
                else if (Loop == AnimationLoop.NoLoop)
                {
                    OnAnimationEnd?.Invoke(Name);
                    foreach (var kf in Keyframes)
                        kf.Reset();
                    _started = false;
                }
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