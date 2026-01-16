using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Base class of all the keyframes.
    /// </summary>
    public class Keyframe
    {
        private bool _runOnce;
        private bool _started;
        public TimeSpan Begin;
        public TimeSpan End;
        public Action OnUpdate;
        public Action OnEnter;
        public Action OnExit;
        public KeyframeState State { get; private set; } = KeyframeState.Idle;
        public Keyframe()
        {
        }
        public Keyframe(Action onUpdate, bool runOnce)
        {
            OnUpdate = onUpdate;
            _runOnce = runOnce;
        }
        public Keyframe(Action onEnter, Action onUpdate, Action onExit, bool runOnce)
        {
            OnEnter = onEnter;
            OnUpdate = onUpdate;
            OnExit = onExit;
            _runOnce = runOnce;
        }

        public virtual void Enter()
        {
            State = KeyframeState.Active;
            OnEnter?.Invoke();
        }
        public virtual void Update(float delta)
        {
            if (_runOnce && !_started)
            {
                OnUpdate?.Invoke();
                _started = true;
            }

            if (!_runOnce)
            {
                OnUpdate?.Invoke();
            }
        }
        public virtual void Update(float delta, bool forward)
        {
            if (_runOnce && !_started)
            {
                OnUpdate?.Invoke();
                _started = true;
            }

            if (!_runOnce)
            {
                OnUpdate?.Invoke();
            }
        }
        public virtual void Exit()
        {
            State = KeyframeState.Finished;
            OnExit?.Invoke();
            _started = false;
        }
        public void Reset()
        {
            State = KeyframeState.Idle;
            _started = false;
        }
    }
}
