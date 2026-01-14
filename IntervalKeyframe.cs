using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// This keyframe will execute a callback function for the defined duration 
    /// (or until the current animation ends).
    /// </summary>
    public class IntervalKeyframe : Keyframe
    {
        public IntervalKeyframe(Action onUpdate, bool runOnce = true) : base(onUpdate, runOnce)
        {
        }
        public IntervalKeyframe(Action onUpdate, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onUpdate, runOnce)
        {
            Begin = begin;
            End = end;
        }
        public IntervalKeyframe(Action onEnter, Action onUpdate, Action onExit, TimeSpan begin, TimeSpan end, bool runOnce = true) : base(onEnter, onUpdate, onExit, runOnce)
        {
            Begin = begin;
            End = end;
        }
        public static void SortKeyframesByDuration(Animation animation)
        {
            double interval = animation.Duration.TotalSeconds / animation.Keyframes.Length;
            for (int i = 0; i < animation.Keyframes.Length; i++)
            {
                var kf = animation.Keyframes[i];
                kf.Begin = TimeSpan.FromSeconds(interval * i);
                kf.End = TimeSpan.FromSeconds(interval + interval * i);
            }
        }
    }
}
