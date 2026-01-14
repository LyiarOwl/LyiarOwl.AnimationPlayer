using System;

namespace LyiarOwl.AnimationPlayer
{
    public class Keyframe
    {
        private bool _runOnce;
        private bool _started;
        protected TimeSpan _begin;
        protected TimeSpan _end;
        public TimeSpan Begin
        {
            get => _begin;
            set
            {
                if (value > _end)
                {
                    var log = AnimationPlayerCore.CreateWarningMessage<Keyframe>(
                        nameof(Begin),
                        "Begin must be small or equal to End. Begin is being set to Zero!"
                    );
                    Console.WriteLine(log);
                    _begin = TimeSpan.Zero;
                    return;
                }
                _begin = value;
            }
        }
        public TimeSpan End
        {
            get => _end;
            set
            {
                if (value < _begin)
                {
                    var log = AnimationPlayerCore.CreateWarningMessage<Keyframe>(
                         nameof(Begin),
                         "End must be greater or equal to Begin. End is being set to Zero!"
                     );
                    Console.WriteLine(log);
                    _end = TimeSpan.Zero;
                    return;
                }
                value = TimeSpan.Zero;
            }
        }
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
        public virtual void Update()
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