namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Defines the interpolation curve used to calculate intermediate values.
    /// </summary>
    public enum InterpolationType
    {
        /// <summary>
        /// Linear interpolation with constant speed.
        /// </summary>
        Linear,

        /// <summary>
        /// Smooth interpolation that accelerates at the beginning
        /// and decelerates at the end.
        /// </summary>
        EaseInOut,

        /// <summary>
        /// Interpolation that starts slowly and accelerates over time.
        /// </summary>
        EaseIn,

        /// <summary>
        /// Interpolation that starts quickly and decelerates over time.
        /// </summary>
        EaseOut
    }
}