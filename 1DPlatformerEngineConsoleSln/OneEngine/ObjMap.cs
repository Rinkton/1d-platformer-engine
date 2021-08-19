using System;
using System.Collections.Generic;

namespace OneEngine
{
    static class ObjMap
    {
        // List<Objs.Obj>[,] - 2D map of Obj Lists(Lists needed to have in one coord more than one Obj)
        private static List<Objs.Obj>[,] actualContent;

        private static List<Objs.Obj>[,] newContent;

        public static List<Objs.Obj>[,] GetContent() => actualContent;

        public static void SetContent(List<Objs.Obj>[,] objList) => newContent = objList;

        public static void AddContent(Objs.Obj obj, int x, int y)
        {
            obj.X = x;
            obj.Y = y;
            newContent[y, x].Add(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="number">Number in cell's list. Default - last element</param>
        /// <returns><see cref="Obj"/> in those coordinates. 
        /// If there is no <see cref="Obj"/>, then returns <see cref="NullObj"/></returns>
        public static Objs.Obj GetObj(int x, int y, int number=-1)
        {
            if(validateCoords(x, y) == false)
            {
                return new Objs.NullObj();
            }

            List<Objs.Obj> objCell = actualContent[y, x];
            if(number == -1)
            {
                if(objCell.Count != 0)
                {
                    return objCell[objCell.Count - 1];
                }
            }
            else
            {
                if (objCell.Count > number)
                {
                    return objCell[number];
                }
            }

            return new Objs.NullObj();
        }

        public static bool ExistObj(int x, int y)
        {
            if (validateCoords(x, y) == false)
            {
                return false;
            }
            List<Objs.Obj> objCell = actualContent[y, x];
            return objCell.Count > 0;
        }

        public static Objs.Obj FindFirstObjByThisType(Type type)
        {
            foreach(List<Objs.Obj> objList in actualContent)
            {
                foreach(Objs.Obj obj in objList)
                {
                    if(obj.GetType() == type)
                    {
                        return obj;
                    }
                }
            }

            return null;
        }

        public static void UpdateContent() => actualContent = newContent.Clone() as List<Objs.Obj>[,];

        private static bool validateCoords(int x, int y)
        {
            bool negative = x < 0 || y < 0;
            if(negative)
            {
                return false;
            }

            bool notInRange = actualContent.GetLength(0) - 1 < y || actualContent.GetLength(1) - 1 < x;
            if(notInRange)
            {
                return false;
            }

            return true;
        }
    }
}
