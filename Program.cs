using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using ListExtensions;

namespace csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Enumerable.Range(0, 20128).ToList();
            var y = x.Paginate(2000);
            var z = y.Select((item, i) => new {item, i})
            .Select(b => {
                if (b.i != 0) {
                    Thread.Sleep(100);
                }
                return b.item;
            }).ToList();

        }
    }
}

namespace ListExtensions {
    public static class ListExtensions {
        public static List<List<int>> Paginate(this List<int> items, int pageSize) => items
            .Select((v, i) => new {v, i})
            .Aggregate(new List<List<int>>(), (acc, x) => {
                acc.Add(items.Skip(x.i * pageSize).Take(pageSize).ToList());
                return acc;
            })
            .Where(c => c.Count > 0)
            .ToList();
    }
}