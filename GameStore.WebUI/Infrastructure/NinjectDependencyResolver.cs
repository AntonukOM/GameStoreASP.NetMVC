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
            /*
            //use a list for demonstration
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            LocalGameDBContext localGames = new LocalGameDBContext();
            mock.Setup(m => m.Games).Returns(localGames.Games);
            _kernel.Bind<IGameRepository>().ToConstant(mock.Object);
             * */
        }
    }
}