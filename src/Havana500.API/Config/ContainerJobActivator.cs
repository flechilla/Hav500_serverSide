using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;

namespace Havana500.Config
{
    public class ContainerJobActivator : JobActivator
    {
        private IServiceContainer _container;

        public ContainerJobActivator(IServiceContainer container)
        {
            _container = container;
        }

        public override object ActivateJob(Type type)
        {
            return _container.GetService(type);
        }
    }
}
