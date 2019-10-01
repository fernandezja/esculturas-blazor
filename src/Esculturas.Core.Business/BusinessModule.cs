using Autofac;
using Esculturas.Core.Business.Interfaces;
using Esculturas.Core.Configuration;
using Esculturas.Core.Data;
using Esculturas.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Esculturas.Core.Business
{
    public class BusinessModule: Autofac.Module
    {
        public ICurrentConfiguration CurrentConfiguration { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DataModule()
            {
                CurrentConfiguration = CurrentConfiguration
            });

            builder.RegisterType(typeof(EsculturaBusiness))
              .As(typeof(IEsculturaBusiness))
              //.WithProperty("ConnectionString", ConnectionString)
              .AsImplementedInterfaces()
              .InstancePerDependency();

        }
    }
}
