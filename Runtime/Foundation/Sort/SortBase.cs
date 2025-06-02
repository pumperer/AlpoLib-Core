using System;

namespace alpoLib.Core.Foundation.Sort
{
    public enum OrderType
    {
        Asc,
        Desc
    };
    
    public abstract class SortOrder
    {
        public OrderType Order { get; set; } = OrderType.Desc;
    }
    
    public abstract class SortBase<T> : SortOrder
    {
        protected int CompareOrder(bool l, bool r)
        {
            return Order switch
            {
                OrderType.Asc => l.CompareTo(r),
                OrderType.Desc => r.CompareTo(l),
                _ => 0
            };
        }
        
        protected int CompareOrder(int l, int r)
        {
            return Order switch
            {
                OrderType.Asc => l.CompareTo(r),
                OrderType.Desc => r.CompareTo(l),
                _ => 0
            };
        }
        
        protected int CompareOrder(string l, string r)
        {
            return Order switch
            {
                OrderType.Asc => string.Compare(l, r, StringComparison.InvariantCulture),
                OrderType.Desc => string.Compare(r, l, StringComparison.InvariantCulture),
                _ => 0
            };
        }
        
        protected int CompareOrder(float l, float r)
        {
            return Order switch
            {
                OrderType.Asc => l.CompareTo(r),
                OrderType.Desc => r.CompareTo(l),
                _ => 0
            };
        }
        
        protected int CompareOrder(double l, double r)
        {
            return Order switch
            {
                OrderType.Asc => l.CompareTo(r),
                OrderType.Desc => r.CompareTo(l),
                _ => 0
            };
        }
        
        protected int CompareOrder(DateTime l, DateTime r)
        {
            return Order switch
            {
                OrderType.Asc => l.CompareTo(r),
                OrderType.Desc => r.CompareTo(l),
                _ => 0
            };
        }

        protected int CompareAction(T l, T r, Func<T, bool> action)
        {
            return CompareOrder(action(l), action(r));
        }
        
        protected int CompareAction(T l, T r, Func<T, int> action)
        {
            return CompareOrder(action(l), action(r));
        }
        
        protected int CompareAction(T l, T r, Func<T, string> action)
        {
            return CompareOrder(action(l), action(r));
        }
        
        protected int CompareAction(T l, T r, Func<T, float> action)
        {
            return CompareOrder(action(l), action(r));
        }
        
        protected int CompareAction(T l, T r, Func<T, double> action)
        {
            return CompareOrder(action(l), action(r));
        }
        
        protected int CompareAction(T l, T r, Func<T, DateTime> action)
        {
            return CompareOrder(action(l), action(r));
        }
    }
}