using System.Collections.Generic;

namespace Sketcher.Extenders
{
    public static class LinkedListExtender
    {
        public static void Reverse<T>(this LinkedList<T> list)
        {
            var nodeL = list.First;
            var nodeR = list.Last;

            for (int i = 0; i < list.Count / 2; i++)
            {
                if (nodeL == null || nodeR == null) return;

                var tmp = nodeL.Value;
                nodeL.Value = nodeR.Value;
                nodeR.Value = tmp;

                nodeL = nodeL.Next;
                nodeR = nodeR.Previous;
            }
        }
    }
}
