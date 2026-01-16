using Microsoft.Xna.Framework;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Essential part that makes the <seealso cref="AnimationPlayer"/> works properly.
    /// </summary>
    public static class AnimationPlayerCore
    {
        public static float DeltaTime { get; private set; }
        /// <summary>
        /// Updates the <seealso cref="DeltaTime" /> property that will be used by all the components of this
        /// module.
        /// </summary>
        public static void Update(GameTime time)
        {
            DeltaTime = (float)time.ElapsedGameTime.TotalSeconds;
        }
        public static string CreateMessage<T>(string methodName, string message, string errorType = "")
    where T : class
        {
            return $"{errorType}[{typeof(T).Name} :: {methodName}]: {message}";
        }
        public static string CreateWarningMessage<T>(string methodName, string message) where T : class
        {
            return CreateMessage<T>(methodName, message, "[WARN]");
        }
    }
}