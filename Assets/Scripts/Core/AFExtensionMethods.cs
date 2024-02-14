using System;
using System.Collections.Generic;

namespace AFSInterview.Core
{
    public static class AFExtensionMethods
    {
        private static Random DefaultRNG = new ();  
        
        public static void Shuffle<T>(this IList<T> list, Random rng = null)
        {
            rng ??= DefaultRNG;

            for (var i = list.Count; i > 0; i--)
            {
                list.Swap(0, rng.Next(0, i));
            }
        }

        private static void Swap<T>(this IList<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}