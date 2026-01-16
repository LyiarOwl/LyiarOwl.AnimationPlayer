using System;

namespace LyiarOwl.AnimationPlayer
{
    public class Animation
    {
        private double _elapsed;
        private TimeSpan _duration;
        private bool _started;
        private bool _startedToInterpolate;
        private bool _continue = true;
        private bool _playingForward = true;
        public readonly string Name;
        public readonly Keyframe[] Keyframes;
        public event Action<string> OnAnimationEnd;
        public TimeSpan Duration => _duration;
        public AnimationLoop Loop;
        public Animation(string name, Keyframe[] keyframes, TimeSpan duration,
            AnimationLoop loop = AnimationLoop.NoLoop)
        {
            Name = name;
            Keyframes = keyframes;
            _duration = duration;
            Loop = loop;
        }

        public void Update(float delta)
        {
            if (!_started) return;

            double direction = 1.0;

            // if (Loop == AnimationLoop.PingPong)
            //     direction = _playingForward ? 1.0 : -1.0;
            // else
            direction = _playingForward ? 1.0 : -1.0;

            if (_continue)
                _elapsed += delta * direction;

            UpdateAllKeyframes(_playingForward, delta);

            if (HasReachedEnd())
                HandleEnd();
        }
        private void UpdateAllKeyframes(bool forward, float delta)
        {
            if (forward)
            {
                foreach (var kf in Keyframes)
                {
                    UpdateKeyframe(kf, delta);
                }
            }
            else
            {
                for (int i = Keyframes.Length - 1; i >= 0; i--)
                {
                    UpdateKeyframe(Keyframes[i], delta);
                }
            }
        }
        private void UpdateKeyframe(Keyframe keyframe, float delta)
        {
            bool insideInterval = _elapsed >= keyframe.Begin.TotalSeconds &&
                    _elapsed <= keyframe.End.TotalSeconds;

            /* enter keyframe */
            if (insideInterval && keyframe.State == KeyframeState.Idle)
            {
                if (keyframe is PauseKeyframe pauseKf)
                    if (!pauseKf.Condition())
                        _continue = false;

                keyframe.Enter();
            }

            /* update keyframe */
            if (insideInterval && keyframe.State == KeyframeState.Active)
            {
                if (keyframe is PauseKeyframe pauseKf)
                    if (pauseKf.Condition())
                        _continue = true;

                if (keyframe is InterpolationKeyframe interpolationKf)
                {
                    keyframe.Update(delta, _playingForward);
                }

                keyframe.Update(delta);
            }

            /* exit keyframe */
            if (!insideInterval && keyframe.State == KeyframeState.Active)
            {
                keyframe.Exit();
            }
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
                    else
                        _elapsed += _duration.TotalSeconds;

                    ResetKeyframes();
                    break;
                case AnimationLoop.NoLoop:
                    OnAnimationEnd?.Invoke(Name);
                    ResetKeyframes();
                    _started = false;
                    _continue = true;
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
            _started = true;
            _continue = true;
            
            _elapsed = _playingForward ? 0.0 : _duration.TotalSeconds;

            ResetKeyframes();
        }
        public void Exit()
        {
            _started = false;
        }
        public bool IsFinished() => !_started;
        public void SetPlayDirection(bool forward)
        {
            _playingForward = forward;
            if (!_playingForward)
                _elapsed = _duration.TotalSeconds;
        }
    }
}