using System;
using System.Collections.Generic;
using Autofac;

namespace AutoFac_Implement
{
    internal interface IAccountRepo
    {
        List<string> GetAccount();
    }

    internal interface ILogger
    {
        void RecordLog(string message);
    }

    internal class AdminRepo : IAccountRepo
    {
        public List<string> GetAccount()
        {
            return new List<string>()
            {
                "maruko",
                "guy",
                "kyo",
                "angel",
                "april"
            };
        }
    }

    internal class Application
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AdminRepo>().As<IAccountRepo>();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<LoginService>();
            var container = builder.Build();
            var loginService = container.Resolve<LoginService>();
            loginService.Login("maruko", "123456");
            Console.Read();
        }
    }

    internal class ConsoleLogger : ILogger
    {
        public void RecordLog(string message)
        {
            Console.WriteLine(message);
        }
    }

    internal class Logger : ILogger
    {
        public void RecordLog(string message)
        {
            throw new NotImplementedException();
        }
    }

    internal class LoginService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly ILogger _logger;

        public LoginService(IAccountRepo accountRepo, ILogger logger)
        {
            _accountRepo = accountRepo;
            _logger = logger;
        }

        public bool Auth(string account, string password)
        {
            var result = false;
            var accountList = _accountRepo.GetAccount();
            foreach (var adminAccount in accountList)
            {
                if (account != adminAccount) continue;
                result = true;
                break;
            }
            if (account == "steven" && password == "123456")
            {
                result = true;
            }
            return result;
        }

        public void Login(string acc, string pwd)
        {
            var message = "";
            message = Auth(acc, pwd) ? $"{acc} is login success!!" : $"{acc} is not the right account!!";
            _logger.RecordLog(message);
        }
    }
}