using System.Collections.Generic;
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

            var blockType = new Objs.Block(0, 0).GetType();
            var playerType = new Objs.Player(0, 0, null).GetType();

            Objs.Player playerObj = (Objs.Player)ObjMap.FindFirstObjByThisType(playerType);

            foreach (List<Objs.Obj> objList in ObjMap.GetContent())
            {
                if(objList.Count == 0)
                {
                    continue;
                }
                Objs.Obj obj = objList.Last();

                int x = obj.X;
                int y = obj.Y;

                if (blockType == obj.GetType())
                {
                    drawSymbol(x, y, '*', Configurator.DefaultColor);
                }
                else if (playerType == obj.GetType())
                {
                    for (int yy = 0; yy < playerObj.Height; yy++)
                    {
                        for (int xx = 0; xx < playerObj.Width; xx++)
                        {
                            bool turnedRight = playerObj.TurnedRight;
                            char symbol = turnedRight ? ')' : '(';
                            drawSymbol(x + xx, y + yy, symbol, Configurator.DefaultColor);
                        }
                    }
                    int viewX = playerObj.X + (playerObj.Width - 1);
                    int viewY = playerObj.Y;
                    drawSymbol(viewX, viewY, ']', Configurator.DefaultColor);
                }
            }

            return !Console.WindowUpdate();
        }
    }
}
