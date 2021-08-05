using Autofac;
using GameOfLife.Application;
using GameOfLife.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Game>().As<IGame>();
            builder.RegisterType<Player>().As<IPlayer>();
            builder.RegisterType<GameManager>().As<IGameManager>();
            builder.RegisterType<FileStorage>().As<IDataStorage>();
            builder.RegisterType<Setup>().As<ISetup>();
            builder.RegisterType<SquareField>().As<IField>();
            builder.RegisterType<ConsoleApplication>().As<IApplication>();
            builder.RegisterType<ConsoleKeyControls>().As<IKeyControls>();

            return builder.Build();
        }
    }
}
