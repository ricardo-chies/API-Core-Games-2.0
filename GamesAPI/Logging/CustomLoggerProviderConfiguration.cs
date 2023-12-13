namespace GamesAPI.Logging
{
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        private static int eventIdCounter = 0;
        public int EventId => Interlocked.Increment(ref eventIdCounter);
    }
}
