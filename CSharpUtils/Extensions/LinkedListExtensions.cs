using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class LinkedListExtensions
    {
        #region ForEach
        /// <summary>
        /// 遍历链表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        public static void ForEach<T>( this LinkedList<T> list, Action<T> action )
        {
            var node = list.First;

            while( node != null )
            {
                var next = node.Next;

                action( node.Value );

                node = next;
            }
        }
        #endregion
    }
}
