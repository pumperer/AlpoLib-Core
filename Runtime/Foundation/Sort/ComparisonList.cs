using System;
using System.Collections.Generic;
using System.Linq;

namespace alpoLib.Core.Foundation.Sort
{
    public class ComparisonList<T>
    {
        private IList<Comparison<T>> comparisonList = null;
        public OrderType OrderType { get; }

        public ComparisonList(IList<Comparison<T>> list, OrderType orderType)
        {
            comparisonList = list;
            OrderType = orderType;
        }

        public int Compare(T l, T r)
        {
            return (from comparer in comparisonList
                where comparer != null
                select comparer.Invoke(l, r)).FirstOrDefault(result => result != 0);
        }

        public void AddFront(Comparison<T> comparison)
        {
            comparisonList.Insert(0, comparison);
        }
    }
}