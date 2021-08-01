using System.Collections.Generic;

namespace OneEngine
{
    static class ObjList
    {
        private static List<Objs.Obj> actualContent;

        private static List<Objs.Obj> newContent;

        public static List<Objs.Obj> GetContent() => actualContent;

        public static void SetContent(List<Objs.Obj> objList) => newContent = objList;

        public static void AddContent(Objs.Obj obj) => newContent.Add(obj);

        public static void UpdateContent() => actualContent = new List<Objs.Obj>(newContent);

    }
}
