using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace GamesAPI.Logging
{
    public class CustomerLogger : ILogger
    {
        private readonly string loggerName;
        private readonly CustomLoggerProviderConfiguration loggerConfig;
        private DateTime lastCleanupDate = DateTime.Now;
        private string logFilePath = "Log.txt";

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            int currentEventId = loggerConfig.EventId;

            string mensagem = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss} {logLevel.ToString()}: {currentEventId} - {formatter(state, exception)}";

            EscreverTextoNoArquivo(mensagem);
        }

        private void EscreverTextoNoArquivo(string mensagem)
        {
            DateTime currentDate = DateTime.Now;

            // Verifique se é uma nova semana em relação à data da última limpeza
            if (currentDate.Subtract(lastCleanupDate).TotalDays >= 7)
            {
                lastCleanupDate = currentDate;

                // Crie um novo arquivo de log e limpe o anterior
                string newLogFilePath = $"{logFilePath.Replace(".txt", "")}_{currentDate:ddMMyyyy}.txt";

                if (File.Exists(logFilePath))
                {
                    File.Delete(logFilePath); // Exclua o arquivo anterior, pois começamos uma nova semana
                }

                using (StreamWriter newStreamWriter = new StreamWriter(newLogFilePath))
                {
                    newStreamWriter.WriteLine(mensagem);
                }
            }
            else
            {
                // Adicione a mensagem ao arquivo de log atual
                using (StreamWriter streamWriter = new StreamWriter(logFilePath, true))
                {
                    streamWriter.WriteLine(mensagem);
                }
            }
        }

    }
}
