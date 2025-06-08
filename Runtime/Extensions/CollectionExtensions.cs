using System.Linq;

namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {
        public static T GetRandom<T>(this ICollection<T> col)
        {
            switch (col.Count)
            {
                case 0:
                    throw new IndexOutOfRangeException("Collection needs at least one element to call GetRandom().");
                case 1:
                    return col.ElementAt(0);
                default:
                {
                    var randomIndex = UnityEngine.Random.Range(0, col.Count);
                    return col.ElementAt(randomIndex);
                }
            }
        }
        
        private static readonly Random Random = new();
        
        public static void Shuffle<T>(this IList<T> values)
        { 
            for (var i = values.Count - 1; i > 0; i--) 
            {
                var k = Random.Next(i + 1);
                (values[k], values[i]) = (values[i], values[k]);
            }
        }
    }
}