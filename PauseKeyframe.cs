using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Pauses the execution of the animation until the condition be satisfied.
    /// </summary>
    public class PauseKeyframe : Keyframe
    {
        /// <summary>
        /// Condition that will skip this keyframe.
        /// </summary>
        public readonly Func<bool> Condition;
        /// <summary>
        /// Creates a keyframe that pauses the execution of the animation until a condition be satisfied.
        /// </summary>
        /// <param name="onUpdate">What should happen when this keyframe update.</param>
        /// <param name="condition">Condition that need to be satisfied in order to skip this keyframe.</param>
        /// <param name="begin">Where/When this keyframe should start.</param>
        /// <param name="end">Where/When this keyframe should end.</param>
        /// <param name="runOnce">If <c>false</c> the update callback will be called while this keyframe is active
        /// (instead of just once).</param>
        public PauseKeyframe(Action onUpdate, Func<bool> condition, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onUpdate, runOnce)
        {
            Condition = condition;
            Begin = begin;
            End = end;
        }
        /// <summary>
        /// Creates a keyframe that pauses the execution of the animation until a condition be satisfied.
        /// </summary>
        /// <param name="onUpdate">What should happen when this keyframe update.</param>
        /// <param name="onEnter">What should happen when this keyframe begin.</param>
        /// <param name="onExit">What should happen when this keyframe end.</param>
        /// <param name="condition">Condition that need to be satisfied in order to skip this keyframe.</param>
        /// <param name="begin">Where/When this keyframe should start.</param>
        /// <param name="end">Where/When this keyframe should end.</param>
        /// <param name="runOnce">If <c>false</c> the update callback will be called while this keyframe is active
        /// (instead of just once).</param>
        public PauseKeyframe(Action onEnter, Action onUpdate, Action onExit, Func<bool> condition, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onEnter, onUpdate, onExit, runOnce)
        {
            Condition = condition;
            Begin = begin;
            End = end;
        }
    }
}
