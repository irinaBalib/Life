using Autofac;
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

            builder.RegisterType<GameManager>().As<IGameManager>();
            builder.RegisterType<FileStorage>().As<IDataStorage>();
            builder.RegisterType<PlayerSetup>().As<IPlayerSetup>();
            builder.RegisterType<SquareField>().As<IField>();
            //builder.RegisterInstance(new Cell()).As<ICell>();
            //builder.RegisterType<Cell[,]>().As<ICell[,]>();
            builder.RegisterType<ConsoleApplication>().As<IApplication>();

            return builder.Build();
        }
    }
}
