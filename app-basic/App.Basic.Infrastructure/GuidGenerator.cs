using HashidsNet;
using System;
using System.Threading;

namespace App.Basic.Infrastructure
{
    public class GuidGenerator
    {
        private static Hashids _HashIds;
        private static volatile int _ConTick = 1;

        public static void Init(string salt, int minLen = 16)
        {
            _HashIds = new Hashids(salt, minLen, "BCDFHJKNPQRSTUVWXYZMEGA0123456789");
        }

        public static string NewGUID()
        {
            if (_HashIds == null) Init(Guid.NewGuid().ToString());

            return _HashIds.EncodeLong(Interlocked.Increment(ref _ConTick));
        }
    }
}
