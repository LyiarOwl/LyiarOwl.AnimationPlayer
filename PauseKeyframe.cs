using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Pauses the execution of the animation until a condition is satisfied.
    /// </summary>
    public class PauseKeyframe : Keyframe
    {
        /// <summary>
        /// Condition that must be satisfied in order to skip this keyframe.
        /// </summary>
        public readonly Func<bool> Condition;
        /// <summary>
        /// Creates a keyframe that pauses the execution of the animation until a condition is satisfied.
        /// </summary>
        /// <param name="onUpdate">What should happen while this keyframe is active.</param>
        /// <param name="condition">Condition that must be satisfied to skip this keyframe.</param>
        /// <param name="begin">When this keyframe should start.</param>
        /// <param name="end">When this keyframe should end.</param>
        /// <param name="runOnce">
        /// If <c>false</c>, the update callback will be called continuously while this keyframe is active
        /// (instead of just once).
        /// </param>
        public PauseKeyframe(Action onUpdate, Func<bool> condition, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onUpdate, runOnce)
        {
            Condition = condition;
            Begin = begin;
            End = end;
        }
        /// <summary>
        /// Creates a keyframe that pauses the execution of the animation until a condition is satisfied.
        /// </summary>
        /// <param name="onEnter">What should happen when this keyframe starts.</param>
        /// <param name="onUpdate">What should happen while this keyframe is active.</param>
        /// <param name="onExit">What should happen when this keyframe ends.</param>
        /// <param name="condition">Condition that must be satisfied to skip this keyframe.</param>
        /// <param name="begin">When this keyframe should start.</param>
        /// <param name="end">When this keyframe should end.</param>
        /// <param name="runOnce">
        /// If <c>false</c>, the update callback will be called continuously while this keyframe is active
        /// (instead of just once).
        /// </param>
        public PauseKeyframe(Action onEnter, Action onUpdate, Action onExit, Func<bool> condition, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onEnter, onUpdate, onExit, runOnce)
        {
            Condition = condition;
            Begin = begin;
            End = end;
        }
    }
}
