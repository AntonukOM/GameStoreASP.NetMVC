using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Moq;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Concrete;
using GameStore.Domain;
using System.Configuration;

namespace GameStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParams)
        {
            this._kernel = kernelParams;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {  
            //connect with database on local PC
            _kernel.Bind<IGameRepository>().To<GameDbRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            _kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

        }
    }
}