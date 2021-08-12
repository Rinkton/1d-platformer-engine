using System;
using OpenTK.Graphics;
using SunshineConsole;

namespace OneEngine.Windows.Presets
{
    class VisualizerSunshineConsole : Visualizer
    {
        public ConsoleWindow Console;

        public VisualizerSunshineConsole(ConsoleWindow console)
        {
            Console = console;
        }

        public override bool Visualize()
        {
            throw new NotImplementedException();
        }

        protected void clearConsole()
        {
            for (int i = 0; i < Console.Rows; i++)
            {
                for (int j = 0; j < Console.Cols; j++)
                {
                    Console.Write(i, j, ' ', Color4.Black);
                }
            }
        }

        protected void drawSymbol(int x, int y, char symbol, Color4 color)
        {
            Console.Write(y, x, symbol, color);
        }
    }
}
