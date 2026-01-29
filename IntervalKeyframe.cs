using System;
using System.Security.Cryptography;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Executes callback functions during a defined time interval
    /// (or until the current animation ends).
    /// </summary>
    public class IntervalKeyframe : Keyframe
    {
        /// <summary>
        /// Creates a keyframe that lasts for the specified interval.
        /// </summary>
        /// <param name="onUpdate">
        /// Callback executed while this keyframe is active.
        /// </param>
        /// <param name="runOnce">
        /// If <c>false</c>, the update callback will be called continuously
        /// while this keyframe is active instead of just once.
        /// </param>
        public IntervalKeyframe(Action onUpdate, bool runOnce = true) : base(onUpdate, runOnce)
        {
        }
        /// <summary>
        /// Creates a keyframe that lasts for the specified interval.
        /// </summary>
        /// <param name="onUpdate">
        /// Callback executed while this keyframe is active.
        /// </param>
        /// <param name="begin">Time at which this keyframe starts.</param>
        /// <param name="end">Time at which this keyframe ends.</param>
        /// <param name="runOnce">
        /// If <c>false</c>, the update callback will be called continuously
        /// while this keyframe is active instead of just once.
        /// </param>
        public IntervalKeyframe(Action onUpdate, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onUpdate, runOnce)
        {
            Begin = begin;
            End = end;
        }
        /// <summary>
        /// Creates a keyframe with callbacks for enter, update, and exit events.
        /// </summary>
        /// <param name="onEnter">
        /// Callback executed when this keyframe begins.
        /// </param>
        /// <param name="onUpdate">
        /// Callback executed while this keyframe is active.
        /// </param>
        /// <param name="onExit">
        /// Callback executed when this keyframe ends.
        /// </param>
        /// <param name="runOnce">
        /// If <c>false</c>, the update callback will be called continuously
        /// while this keyframe is active instead of just once.
        /// </param>
        public IntervalKeyframe(Action onEnter, Action onUpdate, Action onExit, bool runOnce = true) : base(onEnter, onUpdate, onExit, runOnce)
        {
        }
        /// <summary>
        /// Creates a keyframe with callbacks for enter, update, and exit events
        /// and a defined time interval.
        /// </summary>
        /// <param name="onEnter">
        /// Callback executed when this keyframe begins.
        /// </param>
        /// <param name="onUpdate">
        /// Callback executed while this keyframe is active.
        /// </param>
        /// <param name="onExit">
        /// Callback executed when this keyframe ends.
        /// </param>
        /// <param name="begin">Time at which this keyframe starts.</param>
        /// <param name="end">Time at which this keyframe ends.</param>
        /// <param name="runOnce">
        /// If <c>false</c>, the update callback will be called continuously
        /// while this keyframe is active instead of just once.
        /// </param>
        public IntervalKeyframe(Action onEnter, Action onUpdate, Action onExit, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onEnter, onUpdate, onExit, runOnce)
        {
            Begin = begin;
            End = end;
        }
        /// <summary>
        /// <para>
        /// Automatically distributes all <seealso cref="Keyframe"/> instances
        /// of an <seealso cref="Animation"/> across its total duration.
        /// </para>
        /// <para>
        /// Useful when keyframes follow a sequential pattern and do not require
        /// manually defined timing.
        /// </para>
        /// </summary>
        /// <param name="animation">The animation whose keyframes will be distributed.</param>
        /// <returns>The same animation instance, with updated keyframe timing.</returns>
        public static Animation AutoDistribute(Animation animation)
        {
            double interval = animation.Duration.TotalSeconds / animation.Keyframes.Count;
            for (int i = 0; i < animation.Keyframes.Count; i++)
            {
                var kf = animation.Keyframes[i];
                kf.Begin = TimeSpan.FromSeconds(interval * i);
                kf.End = TimeSpan.FromSeconds(interval + interval * i);
                if (kf is InterpolationKeyframe ikf)
                    ikf.RecalculateDuration();
            }
            return animation;
        }
    }
}
