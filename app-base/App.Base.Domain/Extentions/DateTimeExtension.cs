﻿using System;

namespace App.Base.Domain.Extentions
{
    public static class DateTimeExtension
    {
        public static long ToUnixTimeSeconds(this DateTime time)
        {
            var dto = new DateTimeOffset(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, TimeSpan.Zero);
            return dto.ToUnixTimeMilliseconds();
        }
    }
}
