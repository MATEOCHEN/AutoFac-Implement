using System;
using System.Collections.Generic;
using Autofac;

namespace AutoFac_Implement
{
    internal class Application
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AdminRepo>().As<IAccountRepo>();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            var container = builder.Build();

            //var loginService = new LoginService(new AdminRepo(), new ConsoleLogger());
            //loginService.Login("steven", "123456");
            //Console.Read();
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

        public void Login(string acc, string pwd)
        {
            var message = "";
            message = Auth(acc, pwd) ? $"{acc} is login success!!" : $"{acc} is not the right account!!";
            _logger.RecordLog(message);
        }

        public bool Auth(string account, string password)
        {
            var result = false;
            //var accountList = _accountRepo.GetAccount();
            //foreach (var adminAccount in accountList)
            //{
            //    if (account != adminAccount) continue;
            //    result = true;
            //    break;
            //}
            if (account == "steven" && password == "123456")
            {
                result = true;
            }
            return result;
        }
    }

    internal interface ILogger
    {
        void RecordLog(string message);
    }

    internal class Logger : ILogger
    {
        public void RecordLog(string message)
        {
            throw new NotImplementedException();
        }
    }

    internal class ConsoleLogger : ILogger
    {
        public void RecordLog(string message)
        {
            Console.WriteLine(message);
        }
    }

    internal interface IAccountRepo
    {
        List<string> GetAccount();
    }

    internal class AdminRepo : IAccountRepo
    {
        public List<string> GetAccount()
        {
            throw new NotImplementedException();
        }
    }
}