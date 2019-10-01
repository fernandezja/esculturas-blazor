using Autofac;
using Esculturas.Core.Configuration;
using Esculturas.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Esculturas.Core.Data
{
    public class DataModule: Autofac.Module
    {
        public ICurrentConfiguration CurrentConfiguration { get; set; }
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType(typeof(EsculturaRepository))
              .As(typeof(IEsculturaRepository))
              //.WithProperty("ConnectionString", ConnectionString)
              .AsImplementedInterfaces()
              .InstancePerDependency();

        }
    }
}
