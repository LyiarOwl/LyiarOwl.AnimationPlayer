using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// This keyframe will pause the execution of the animation until the condition be satisfied.
    /// </summary>
    public class PauseKeyframe : Keyframe
    {
        private readonly Func<bool> _condition;
        public PauseKeyframe(Action onUpdate, Func<bool> condition, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onUpdate, runOnce)
        {
            _condition = condition;
            Begin = begin;
            End = end;
        }
        public PauseKeyframe(Action onEnter, Action onUpdate, Action onExit, Func<bool> condition, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onEnter, onUpdate, onExit, runOnce)
        {
            _condition = condition;
            Begin = begin;
            End = end;
        }
    }
}