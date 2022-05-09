using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FitApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        
        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e) {
            var factory = new LoggerFactory();
            //factory.AddConsoleAdvanced(ConsoleLoggerSettings.HermesDefault);
            //ILogger<Program> logger = new Logger<Program>(factory);

            var exception = (Exception)e.ExceptionObject;
            var innerException = GetInnerMostException(exception);
            /*if (innerException != null)
                logger.LogError(new LogMessage() {
                    Message = innerException.ToString()
                });

            logger.LogError(new LogMessage() {
                Message = exception.ToString()
            });*/
            
            Console.WriteLine(innerException);

            Environment.Exit(1);
        }

        private static Exception GetInnerMostException(Exception ex) {
            Exception currentEx = ex;
            while (currentEx.InnerException != null) {
                currentEx = currentEx.InnerException;
            }

            return currentEx;
        }
    }
}