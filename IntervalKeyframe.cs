using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// This keyframe will execute a callback function for the defined duration 
    /// (or until the current animation ends).
    /// </summary>
    public class IntervalKeyframe : Keyframe
    {
        public IntervalKeyframe(Action onUpdate, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onUpdate, runOnce)
        {
            _begin = begin;
            _end = end;
        }
        public IntervalKeyframe(Action onEnter, Action onUpdate, Action onExit, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onEnter, onUpdate, onExit, runOnce)
        {
            _begin = begin;
            _end = end;
        }
    }
}
