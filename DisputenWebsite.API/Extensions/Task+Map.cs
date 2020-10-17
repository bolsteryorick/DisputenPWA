using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.Extensions
{
    public static class TaskMapExtension
    {
        public static async Task<TResult> Map<TSource, TResult>(this Task<TSource> task, Func<TSource, TResult> selector)
            => selector(await task);
    }
}
