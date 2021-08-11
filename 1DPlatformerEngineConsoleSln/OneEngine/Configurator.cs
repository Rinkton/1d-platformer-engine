using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;

namespace OneEngine
{
    public static class Configurator
    {
        public static int Fps = 60;

        public static Color4 DefaultColor = Color4.LightGray;

        public static int turnLimit = 100;

        public static int turnCooldownTime = 500;
    }
}
