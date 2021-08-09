using Autofac;
using System;
using System.IO;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IGame>();
                app.Run();
            }
        }
    }
}
