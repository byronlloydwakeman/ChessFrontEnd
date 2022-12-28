using APILibrary.Endpoints;
using APILibrary.Validation;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.Library.AutoFac
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GameEndpoint>().As<IGameEndpoint>();

            return builder.Build();
        }
    }
}
