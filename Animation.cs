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

        public void Update()
        {
            if (!_started) return;

            double direction = 1.0;
            if (Loop == AnimationLoop.PingPong)
                direction = _playingForward ? 1.0 : -1.0;

            _elapsed += AnimationPlayerCore.DeltaTime * direction;

            UpdateAllKeyframes(_playingForward);

            if (HasReachedEnd())
            {
                HandleEnd();
            }
        }
        private void UpdateAllKeyframes(bool forward)
        {
            if (forward)
            {
                foreach (var kf in Keyframes)
                    UpdateKeyframe(kf);
            }
            else
            {
                for (int i = Keyframes.Length - 1; i>= 0; i--)
                {
                    UpdateKeyframe(Keyframes[i]);
                }
            }
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
        private bool HasReachedEnd()
        {
            return _elapsed >= _duration.TotalSeconds || _elapsed <= 0.0;
        }
        private void HandleEnd()
        {
            switch (Loop)
            {
                case AnimationLoop.PingPong:
                    _playingForward = !_playingForward;
                    ResetKeyframes();
                    break;
                case AnimationLoop.Loop:
                    if (_elapsed >= _duration.TotalSeconds)
                        _elapsed -= _duration.TotalSeconds;

                    ResetKeyframes();
                    break;
                case AnimationLoop.NoLoop:
                    OnAnimationEnd?.Invoke(Name);
                    ResetKeyframes();
                    _started = false;
                    break;
            }
        }
        private void ResetKeyframes()
        {
            foreach (var kf in Keyframes)
                kf.Reset();
        }
        public void Enter()
        {
            _elapsed = 0.0;
            _started = true;
            _playingForward = true;
            ResetKeyframes();
        }
        public void Exit()
        {
            _started = false;
        }
        public bool IsFinished() => !_started;
    }
}