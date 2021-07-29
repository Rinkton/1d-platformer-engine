using System;
using Objs;
using SunshineConsole;
using OpenTK.Graphics;
using System.Linq;

namespace OneEngine
{
    public class ConsolePlatformerVisualizer : Visualizer
    {
        private ConsoleWindow console;

        public ConsolePlatformerVisualizer(int rows, int columns, string windowName) : base(rows, columns, windowName)
        {
            this.console = new ConsoleWindow(rows, columns, windowName);
        }

        public override void Main(Obj[] objs)
        {
            clearConsole();

            var blockType = new Block().GetType();
            var playerType = new Player().GetType();

            foreach (Obj obj in objs)
            {
                int x = obj.X;
                int y = obj.Y;

                if(blockType == obj.GetType())
                {
                    drawSymbol(x, y, '*', Color4.LightGray);
                }
                else if(playerType == obj.GetType())
                {
                    drawSymbol(x, y, ')', Color4.LightGray);
                }
            }

            if (!console.WindowUpdate())
            {
                //TODO: Not so good decision, but I hope it's temporarily
                throw new Exception("WindowUpdate return false.");
            }
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
