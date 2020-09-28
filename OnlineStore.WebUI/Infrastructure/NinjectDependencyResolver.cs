//using System.Web.Mvc;
//using Ninject;

//namespace OnlineStore.WebUI.Infrastructure
//{
//    public class NinjectDependencyResolver : IDependencyResolver
//    {
//        private IKernel kernel;

//        public NinjectDependencyResolver(IKernel kernel)
//        {
//            this.kernel = kernel;
//            AddBindings();
//        }

//        private void AddBindings()
//        {
            
//        }

//        public object GetService(System.Type serviceType)
//        {
//            return kernel.TryGet(serviceType);
//        }

//        public System.Collections.Generic.IEnumerable<object> GetServices(System.Type serviceType)
//        {
//            return kernel.GetAll(serviceType);
//        }
//    }
//}