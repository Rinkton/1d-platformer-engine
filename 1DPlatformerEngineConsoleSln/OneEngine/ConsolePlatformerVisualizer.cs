﻿using System;
using SunshineConsole;
using OpenTK.Graphics;
using System.Linq;
using OpenTK.Input;

namespace OneEngine
{
    public class ConsolePlatformerVisualizer : Visualizer
    {
        private ConsoleWindow console;

        public ConsolePlatformerVisualizer(int rows, int columns, string windowName) : base(rows, columns, windowName)
        {
            this.console = new ConsoleWindow(rows, columns, windowName);
        }

        public override void Main(Objs.Obj[] objs)
        {
            clearConsole();

            var blockType = new Objs.Block().GetType();
            var playerType = new Objs.Player().GetType();

            foreach (Objs.Obj obj in objs)
            {
                int x = obj.X;
                int y = obj.Y;

                if(blockType == obj.GetType())
                {
                    drawSymbol(x, y, '*', Configurator.DefaultColor);
                }
                else if(playerType == obj.GetType())
                {
                    for (int yy = 0; yy < new Objs.Player().Height; yy++)
                    {
                        for (int xx = 0; xx < new Objs.Player().Width; xx++)
                        {
                            drawSymbol(x+xx, y+yy, ')', Configurator.DefaultColor);
                        }
                    }
                }
            }

            if (!console.WindowUpdate())
            {
                //TODO: Not so good decision, but I hope it's temporarily
                throw new Exception("WindowUpdate return false.");
            }
        }

        public override Key GetKey()
        {
            return console.GetKey();
        }

        private void clearConsole()
        {
            for (int i = 0; i < console.Rows; i++)
            {
                for (int j = 0; j < console.Cols; j++)
                {
                    console.Write(i, j, ' ', Color4.Black);
                }
            }
        }

        private void drawSymbol(int x, int y, char symbol, Color4 color)
        {
            console.Write(y, x, symbol, color);
        }
    }
}
