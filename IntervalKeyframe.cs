using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Executes a callback function for the defined duration 
    /// (or until the current animation ends).
    /// </summary>
    public class IntervalKeyframe : Keyframe
    {
        /// <summary>
        /// Creates a keyframe that lasts the interval specified.
        /// </summary>
        /// <param name="onUpdate">What should happen when this keyframe update.</param>
        /// <param name="runOnce">If <c>false</c> the update callback will be called while this keyframe is active
        /// (instead of just once).</param>
        public IntervalKeyframe(Action onUpdate, bool runOnce = true) : base(onUpdate, runOnce)
        {
        }
        /// <summary>
        /// Creates a keyframe that lasts the interval specified.
        /// </summary>
        /// <param name="onUpdate">What should happen when this keyframe update.</param>
        /// <param name="begin">Where/When this keyframe should start.</param>
        /// <param name="end">Where/When this keyframe should end.</param>
        /// <param name="runOnce">If <c>false</c> the update callback will be called while this keyframe is active
        /// (instead of just once).</param>
        public IntervalKeyframe(Action onUpdate, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onUpdate, runOnce)
        {
            Begin = begin;
            End = end;
        }
        /// <summary>
        /// Creates a keyframe that lasts the interval specified.
        /// </summary>
        /// <param name="onUpdate">What should happen when this keyframe update.</param>
        /// <param name="onEnter">What should happen when this keyframe begin.</param>
        /// <param name="onExit">What should happen when this keyframe end.</param>
        /// <param name="runOnce">If <c>false</c> the update callback will be called while this keyframe is active
        /// (instead of just once).</param>
        public IntervalKeyframe(Action onEnter, Action onUpdate, Action onExit, bool runOnce = true) : base(onEnter, onUpdate, onExit, runOnce)
        {
        }
        /// <summary>
        /// Creates a keyframe that lasts the interval specified.
        /// </summary>
        /// <param name="onUpdate">What should happen when this keyframe update.</param>
        /// <param name="onEnter">What should happen when this keyframe begin.</param>
        /// <param name="onExit">What should happen when this keyframe end.</param>
        /// <param name="begin">Where/When this keyframe should start.</param>
        /// <param name="end">Where/When this keyframe should end.</param>
        /// <param name="runOnce">If <c>false</c> the update callback will be called while this keyframe is active
        /// (instead of just once).</param>
        public IntervalKeyframe(Action onEnter, Action onUpdate, Action onExit, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onEnter, onUpdate, onExit, runOnce)
        {
            Begin = begin;
            End = end;
        }
        /// <summary>
        /// <para>Automatically distribute all the <seealso cref="IntervalKeyframe"/> of this 
        /// <seealso cref="Animation"/> according with it duration.</para>
        /// <para>Useful when your keyframes follow a sequence.</para>
        /// </summary>
        /// <param name="animation"></param>
        /// <returns>Returns the own animation.</returns>
        public static Animation AutoDistribute(Animation animation)
        {
            double interval = animation.Duration.TotalSeconds / animation.Keyframes.Length;
            for (int i = 0; i < animation.Keyframes.Length; i++)
            {
                var kf = animation.Keyframes[i];
                kf.Begin = TimeSpan.FromSeconds(interval * i);
                kf.End = TimeSpan.FromSeconds(interval + interval * i);
            }
            return animation;
        }
    }
}
