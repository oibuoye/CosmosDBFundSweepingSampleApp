using System;
using System.Collections.Generic;
using System.Text;
using CosmosDBFundSweepingSampleApp.Core.Logger.Contracts;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace CosmosDBFundSweepingSampleApp.Core.Logger
{
    public class FileLogger : ILogger
    {
        private readonly IHostingEnvironment _host;
        public FileLogger(IHostingEnvironment host)
        {
            _host = host;
        }

        public void ChargeInfo(string message)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path + $"{DateTime.Now.ToString("yyyy-MM-dd")}-charge.log";
            try
            {
                using (var sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::Charge::{message}");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("denied"))
                {
                    CreateDirectory(path);
                    using (var sw = new StreamWriter(filePath, true))
                    {
                        sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::Charge::{message}");
                    }
                }
            }
        }

        public void Debug(string message)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path + $"{DateTime.Now.ToString("yyyy-MM-dd")}-debug.log";
            try
            {
                using (var sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::DEBUG::{message}");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("denied"))
                {
                    CreateDirectory(path);
                    using (var sw = new StreamWriter(filePath, true))
                    {
                        sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::DEBUG::{message}");
                    }
                }
            }
        }

        public void Debug(string message, Exception exception)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path + $"{DateTime.Now.ToString("yyyy-MM-dd")}-debug.log";

            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::DEBUG::{message}  {exception.Message + exception.StackTrace + exception.InnerException}");
            }
        }

        public void Error(string message)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path + $"{DateTime.Now.ToString("yyyy-MM-dd")}-debug.log";

            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::ERROR::{message}");
            }
        }

        public void Error(string message, Exception exception)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path + $"{DateTime.Now.ToString("yyyy-MM-dd")}-debug.log";

            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::ERROR::{message}  {exception.Message} {exception}");
            }
        }

        public void Fatal(string message)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path + $"{DateTime.Now.ToString("yyyy-MM-dd")}-debug.log";

            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::FATAL::{message}");
            }
        }

        public void Fatal(string message, Exception exception)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path + $"{DateTime.Now.ToString("yyyy-MM-dd")}-debug.log";

            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::FATAL::{message}  {exception.Message + exception.StackTrace + exception.InnerException}");
            }
        }

        public void Info(string message, Exception exception)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path + $"{DateTime.Now.ToString("yyyy-MM-dd")}-debug.log";

            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::INFO::{message}  {exception.Message + exception.StackTrace + exception.InnerException}");
            }
        }

        public void Info(string message)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path+$"{ DateTime.Now.ToString("yyyy-MM-dd")}-debug.log";

            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::INFO::{message}");
            }
        }

        public void Warn(string message)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path + $"{DateTime.Now.ToString("yyyy-MM-dd")}-update.log";

            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::UPDATE::{message}");
            }
        }

        public void Warn(string message, Exception exception)
        {
            string path = _host.ContentRootPath + $@"\Logs\";
            string filePath = path + $"{DateTime.Now.ToString("yyyy-MM-dd")}-update.log";

            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}:::UPDATE::{message}  {exception.Message + exception.StackTrace + exception.InnerException}");
            }
        }

        private bool CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return true;
        }
    }
}
