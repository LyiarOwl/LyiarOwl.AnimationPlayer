using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Animation based on callback functions.
    /// </summary>
    public class Animation
    {
        private double _elapsed;
        private TimeSpan _duration;
        private bool _started;
        private bool _continue = true;
        private bool _playingForward = true;

        /// <summary>
        /// Name of this animation.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Keyframes that compose this animation.
        /// </summary>
        public readonly Keyframe[] Keyframes;
        /// <summary>
        /// <para>Called when the animation ends.</para>
        /// <para>The <c>string</c> parameter is the name of this animation.</para>
        /// </summary>
        public event Action<string> OnAnimationEnd;
        /// <summary>
        /// Duration of this animation.
        /// </summary>
        public TimeSpan Duration => _duration;
        /// <summary>
        /// Defines whether this animation should loop, and the type of looping.
        /// </summary>
        public AnimationLoop Loop;
        /// <summary>
        /// Creates a new animation.
        /// </summary>
        /// <param name="name">Identifier of this animation.</param>
        /// <param name="keyframes">Keyframes that make up this animation.</param>
        /// <param name="duration">Total duration of this animation.</param>
        /// <param name="loop">Defines whether this animation should loop.</param>
        public Animation(string name, Keyframe[] keyframes, TimeSpan duration,
            AnimationLoop loop = AnimationLoop.NoLoop)
        {
            Name = name;
            Keyframes = keyframes;
            _duration = duration;
            Loop = loop;
        }

        /// <summary>
        /// Updates all keyframes of this animation.
        /// </summary>
        /// <param name="delta">Delta time of the current frame.</param>
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
        /// <summary>
        /// <para>Prepares and starts this animation.</para>
        /// <para>This method resets the state of all keyframes.</para>
        /// </summary>
        public void Enter()
        {
            _started = true;
            _continue = true;

            _elapsed = _playingForward ? 0.0 : _duration.TotalSeconds;

            ResetKeyframes();
        }

        /// <summary>
        /// <para>Stops this animation.</para>
        /// <para>This method does not reset the state of the keyframes.</para>
        /// </summary>
        public void Exit()
        {
            _started = false;
        }
        /// <summary>
        /// Indicates whether this animation has stopped.
        /// </summary>
        /// <returns><c>true</c> if the animation is stopped.</returns>
        public bool IsFinished() => !_started;
        /// <summary>
        /// Sets the playback direction of this animation.
        /// </summary>
        /// <param name="forward">
        /// If <c>false</c>, the animation will be configured to play from the end to the beginning.
        /// </param>
        public void SetPlayDirection(bool forward)
        {
            _playingForward = forward;
            if (!_playingForward)
                _elapsed = _duration.TotalSeconds;
        }
    }
}