using System.Linq;
using OpenTK.Graphics;
using SunshineConsole;

namespace OneEngine.Windows.ConsolePlatformer
{
    class Visualizer : Presets.VisualizerSunshineConsole
    {
        public Visualizer(ConsoleWindow console) : base(console) { }

        public override bool Visualize()
        {
            clearConsole();

            var blockType = new Objs.Block().GetType();
            var playerType = new Objs.Player().GetType();

            foreach (Objs.Obj obj in ObjList.GetContent())
            {
                int x = obj.X;
                int y = obj.Y;

                if (blockType == obj.GetType())
                {
                    drawSymbol(x, y, '*', Configurator.DefaultColor);
                }
                else if (playerType == obj.GetType())
                {
                    for (int yy = 0; yy < new Objs.Player().Height; yy++)
                    {
                        for (int xx = 0; xx < new Objs.Player().Width; xx++)
                        {
                            bool turnedRight = ObjList.GetContent().OfType<Objs.Player>().First().TurnedRight;
                            char symbol = turnedRight ? ')' : '(';
                            drawSymbol(x + xx, y + yy, symbol, Configurator.DefaultColor);
                        }
                    }
                    Objs.Player playerObj = ObjList.GetContent().OfType<Objs.Player>().First();
                    int viewX = playerObj.X + (playerObj.Width - 1);
                    int viewY = playerObj.Y;
                    drawSymbol(viewX, viewY, ']', Configurator.DefaultColor);
                }
            }

            return !Console.WindowUpdate();
        }
    }
}
