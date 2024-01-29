// Copyright 2017 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace Serilog.Sinks.File
{
    static class RollingIntervalExtensions
    {
        /// <summary>
        ///     GetFormat with Regex string
        /// </summary>
        /// <returns>
        ///     Item1 - format
        ///     Item2 - regex string
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        public static Tuple<string, string> GetFormat(this RollingInterval interval)
        {
            switch (interval)
            {
                case RollingInterval.Infinite:
                    return new Tuple<string, string>("", "");
                case RollingInterval.Year:
                    return new Tuple<string, string>("yyyy", "\\d{4}");
                case RollingInterval.Month:
                    return new Tuple<string, string>("yyyy-MM", "\\d{4}-\\d{2}");
                case RollingInterval.Day:
                    return new Tuple<string, string>("yyyy-MM-dd", "\\d{4}-\\d{2}-\\d{2}");
                case RollingInterval.Hour:
                    return new Tuple<string, string>("yyyy-MM-dd-HH", "\\d{4}-\\d{2}-\\d{2}-\\d{2}");
                case RollingInterval.Minute:
                    return new Tuple<string, string>("yyyy-MM-dd-HH-mm", "\\d{4}-\\d{2}-\\d{2}-\\d{2}-\\d{2}");
                default:
                    throw new ArgumentException("Invalid rolling interval");
            }
        }

        public static DateTime? GetCurrentCheckpoint(this RollingInterval interval, DateTime instant)
        {
            switch (interval)
            {
                case RollingInterval.Infinite:
                    return null;
                case RollingInterval.Year:
                    return new DateTime(instant.Year, 1, 1, 0, 0, 0, instant.Kind);
                case RollingInterval.Month:
                    return new DateTime(instant.Year, instant.Month, 1, 0, 0, 0, instant.Kind);
                case RollingInterval.Day:
                    return new DateTime(instant.Year, instant.Month, instant.Day, 0, 0, 0, instant.Kind);
                case RollingInterval.Hour:
                    return new DateTime(instant.Year, instant.Month, instant.Day, instant.Hour, 0, 0, instant.Kind);
                case RollingInterval.Minute:
                    return new DateTime(instant.Year, instant.Month, instant.Day, instant.Hour, instant.Minute, 0,
                        instant.Kind);
                default:
                    throw new ArgumentException("Invalid rolling interval");
            }
        }

        public static DateTime? GetNextCheckpoint(this RollingInterval interval, DateTime instant)
        {
            var current = GetCurrentCheckpoint(interval, instant);
            if (current == null)
                return null;

            switch (interval)
            {
                case RollingInterval.Year:
                    return current.Value.AddYears(1);
                case RollingInterval.Month:
                    return current.Value.AddMonths(1);
                case RollingInterval.Day:
                    return current.Value.AddDays(1);
                case RollingInterval.Hour:
                    return current.Value.AddHours(1);
                case RollingInterval.Minute:
                    return current.Value.AddMinutes(1);
                default:
                    throw new ArgumentException("Invalid rolling interval");
            }
        }
    }
}
