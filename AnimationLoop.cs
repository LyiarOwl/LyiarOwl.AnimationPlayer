namespace LyiarOwl.AnimationPlayer
{
    public enum AnimationLoop
    {
        /// <summary>
        /// The animation will play only once.
        /// </summary>
        NoLoop,
        /// <summary>
        /// The animation will repeat continuously.
        /// </summary>
        Loop,
        /// <summary>
        /// The animation will repeat, playing forward and backward alternately.
        /// </summary>
        PingPong
    }
}