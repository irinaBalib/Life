using Autofac;
using GameOfLife.Application;
using GameOfLife.Grid;
using GameOfLife.Input;
using GameOfLife.Logic;
using GameOfLife.SaveGame;
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
            builder.RegisterType<AvailableOptions>().As<IOptions>();
            builder.RegisterType<InputValidator>().As<IValidator>();
            builder.RegisterType<GameManager>().As<IGameManager>();
            builder.RegisterType<GameFile>().As<IGameStorage>();
            builder.RegisterType<PlayerInputCapture>().As<IPlayerInputCapture>();
            builder.RegisterType<FieldFactory>().As<IFieldFactory>();
            builder.RegisterType<FieldManager>().As<IFieldManager>();
            builder.RegisterType<SquareField>().As<IField>();
            builder.RegisterType<ConsoleApplication>().As<IApplication>();
            builder.RegisterType<ConsoleKeyControls>().As<IKeyControls>();

            return builder.Build();
        }
    }
}
