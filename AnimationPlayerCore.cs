using System;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Core utilities required for the <seealso cref="AnimationPlayer"/> to work properly.
    /// </summary>
    public static class AnimationPlayerCore
    {
        /// <summary>
        /// Delta time (in seconds) of the current frame.
        /// </summary>
        public static float DeltaTime { get; private set; }
        /// <summary>
        /// Updates the <seealso cref="DeltaTime"/> value used internally by all components
        /// of this module.
        /// </summary>
        /// <param name="time">Current game time.</param>
        public static void Update(TimeSpan time)
        {
            Update((float)time.TotalSeconds);
        }
        /// <summary>
        /// Updates the <seealso cref="DeltaTime"/> value used internally by all components
        /// of this module.
        /// </summary>
        /// <param name="deltaTime">Current delta time.</param>
        public static void Update(float deltaTime)
        {
            if (float.IsNaN(deltaTime) || float.IsInfinity(deltaTime))
            {
#if DEBUG
                Console.WriteLine($"[WARN][AnimationPlayerCore :: Update]: deltaTime is NaN or Infinity. It is being set to 0.0!");
#endif
                DeltaTime = 0f;
                return;
            }
            DeltaTime = Max(0.0f, deltaTime);
        }
        public static float Max(float a, float b)
        {
            if (a > b)
                return a;
            return b;
        }
        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;   
        }
        /// <summary>
        /// Creates a formatted diagnostic message associated with a specific class and method.
        /// </summary>
        /// <typeparam name="T">
        /// The class type where the message originated.
        /// </typeparam>
        /// <param name="methodName">
        /// Name of the method that generated the message.
        /// </param>
        /// <param name="message">
        /// The message content.
        /// </param>
        /// <param name="errorType">
        /// Optional prefix used to categorize the message (for example, warnings or errors).
        /// </param>
        /// <returns>
        /// A formatted string containing the message and its origin context.
        /// </returns>
        public static string CreateMessage<T>(string methodName, string message, string errorType = "")
    where T : class
        {
            return $"{errorType}[{typeof(T).Name} :: {methodName}]: {message}";
        }
        /// <summary>
        /// Creates a formatted warning message associated with a specific class and method.
        /// </summary>
        /// <typeparam name="T">
        /// The class type where the warning originated.
        /// </typeparam>
        /// <param name="methodName">
        /// Name of the method that generated the warning.
        /// </param>
        /// <param name="message">
        /// The warning message content.
        /// </param>
        /// <returns>
        /// A formatted warning string.
        /// </returns>
        public static string CreateWarningMessage<T>(string methodName, string message) where T : class
        {
            return CreateMessage<T>(methodName, message, "[WARN]");
        }
    }
}