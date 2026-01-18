using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Base class for all keyframes.
    /// </summary>
    /// <remarks>
    /// A keyframe represents a logical interval inside an animation and can react to
    /// entering, updating, and exiting that interval.
    /// </remarks>
    public class Keyframe
    {
        private bool _runOnce;
        private bool _started;
        /// <summary>
        /// Time at which this keyframe becomes eligible to run.
        /// </summary>
        public TimeSpan Begin;
        /// <summary>
        /// Time at which this keyframe is no longer eligible to run.
        /// </summary>
        public TimeSpan End;
        /// <summary>
        /// Callback executed while this keyframe is active.
        /// </summary>
        public Action OnUpdate;
        /// <summary>
        /// Callback executed when this keyframe is entered.
        /// </summary>
        /// <remarks>
        /// This method is called when the animation flow enters this keyframe.
        /// </remarks>
        public Action OnEnter;
        /// <remarks>
        /// This method is called when the animation flow leaves this keyframe.
        /// </remarks>
        public Action OnExit;
        /// <summary>
        /// Current state of this keyframe.
        /// </summary>
        public KeyframeState State { get; private set; } = KeyframeState.Idle;
        /// <summary>
        /// Creates an empty keyframe.
        /// </summary>
        public Keyframe()
        {
        }
        /// <summary>
        /// Creates a keyframe with an update callback.
        /// </summary>
        /// <param name="onUpdate">Callback executed during the update phase.</param>
        /// <param name="runOnce">
        /// If <c>true</c>, <paramref name="onUpdate"/> will be executed only once when the keyframe starts.
        /// </param>
        public Keyframe(Action onUpdate, bool runOnce)
        {
            OnUpdate = onUpdate;
            _runOnce = runOnce;
        }
        /// <summary>
        /// Creates a keyframe with enter, update, and exit callbacks.
        /// </summary>
        /// <param name="onEnter">Callback executed when entering this keyframe.</param>
        /// <param name="onUpdate">Callback executed while this keyframe is active.</param>
        /// <param name="onExit">Callback executed when exiting this keyframe.</param>
        /// <param name="runOnce">
        /// If <c>true</c>, <paramref name="onUpdate"/> will be executed only once when the keyframe starts.
        /// </param>
        public Keyframe(Action onEnter, Action onUpdate, Action onExit, bool runOnce)
        {
            OnEnter = onEnter;
            OnUpdate = onUpdate;
            OnExit = onExit;
            _runOnce = runOnce;
        }
        /// <summary>
        /// Called when the animation attempts to enter this keyframe.
        /// </summary>
        /// <remarks>
        /// This method is responsible for preparing the keyframe state and
        /// may influence whether the keyframe actually becomes active.
        /// </remarks>
        public virtual void Enter()
        {
            State = KeyframeState.Active;
            OnEnter?.Invoke();
        }
        /// <summary>
        /// Updates this keyframe.
        /// </summary>
        /// <param name="delta">Elapsed time since the last update.</param>
        /// <remarks>
        /// This method controls how and when <see cref="OnUpdate"/> is executed,
        /// depending on whether the keyframe is configured to run once or continuously.
        /// </remarks>
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
        /// <summary>
        /// Updates this keyframe considering the animation direction.
        /// </summary>
        /// <param name="delta">Elapsed time since the last update.</param>
        /// <param name="forward">Indicates whether the animation is playing forward.</param>
        /// <remarks>
        /// By default, direction is ignored. Override this method if direction matters.
        /// </remarks>
        public virtual void Update(float delta, bool forward)
        {
            Update(delta);
        }
        /// <summary>
        /// Called when the animation leaves this keyframe.
        /// </summary>
        /// <remarks>
        /// This method is responsible for cleaning up or finalizing the keyframe state
        /// after it is no longer active.
        /// </remarks>
        public virtual void Exit()
        {
            State = KeyframeState.Finished;
            OnExit?.Invoke();
            _started = false;
        }
        /// <summary>
        /// Resets this keyframe to its initial state.
        /// </summary>
        /// <remarks>
        /// This clears the internal execution state, allowing the keyframe to run again.
        /// </remarks>
        public void Reset()
        {
            State = KeyframeState.Idle;
            _started = false;
        }
    }
}
