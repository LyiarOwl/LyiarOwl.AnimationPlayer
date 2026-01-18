namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Represents the current execution state of a keyframe.
    /// </summary>
    public enum KeyframeState
    {
        /// <summary>
        /// The keyframe has not started yet.
        /// </summary>
        Idle,
        /// <summary>
        /// The keyframe is currently active and being updated.
        /// </summary>
        Active,

        /// <summary>
        /// The keyframe has finished its execution.
        /// </summary>
        Finished
    }
}