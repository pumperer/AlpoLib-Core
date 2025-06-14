using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public static async void Run(Func<Awaitable> awaitable, [CallerMemberName] string caller = "")
        {
            try
            {
                await awaitable();
            }
            catch (Exception e)
            {
                global::Debug.LogError($"An exception occurred in Run Awaitable : {caller}\n{e}");
            }
        }

        public static async void Run<T>(Func<Awaitable<T>> awaitable, Action<T> onResult = null, [CallerMemberName] string caller = "")
        {
            try
            {
                var result = await awaitable();
                onResult?.Invoke(result);
            }
            catch (Exception e)
            {
                global::Debug.LogError($"An exception occurred in Run Awaitable<T> : {caller}\n{e}");
            }
        }
    }
}