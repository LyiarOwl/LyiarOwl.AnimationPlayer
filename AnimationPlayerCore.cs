using Microsoft.Xna.Framework;

namespace LyiarOwl.AnimationPlayer
{
    public static class AnimationPlayerCore
    {
        public static float DeltaTime { get; private set; }
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