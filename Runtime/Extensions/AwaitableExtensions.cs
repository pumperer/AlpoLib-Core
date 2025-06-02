using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UnityEngine
{
    public static class AwaitableHelper
    {
        public static async Awaitable WaitUntil(Func<bool> predicate, CancellationToken cancellationToken = default)
        {
            while (!predicate())
                await Awaitable.NextFrameAsync(cancellationToken);
        }

        public static async Awaitable WhenAll(IEnumerable<Awaitable> awaitables)
        {
            await WhenAll(awaitables.ToArray());
        }
        
        public static async Awaitable WhenAll(params Awaitable[] awaitables)
        {
            if (awaitables.Length == 0)
                return;

            while (true)
            {
                var completed = true;
                for (var i = 0; i < awaitables.Length; i++)
                {
                    var awaitable = awaitables[i];
                    completed &= awaitable == null || awaitable.IsCompleted;
                }

                if (completed)
                    break;
                
                await Awaitable.NextFrameAsync();
            }
        }

        
    }
}