using System;
using System.IO;

namespace Lix.Extensions
{
    public static class Extensions
    {
        private static readonly string[] SizeSuffixes = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB" };

        public static string GetBetween(this string input, string left, string right = null)
        {
            int i0 = input.IndexOf(left) + (left.Length + 1);
            if (string.IsNullOrEmpty(right))
            {
                return input.Substring(i0);
            }
            int i1 = input.IndexOf(right, i0);
            if (i1 < i0)
            {
                return input.Substring(i0);
            }
            else
            {
                return input.Substring(i0, i1 - i0);
            }
        }

        public static string GetBetweenN(this string input, string left, string right = null)
        {
            int i0 = input.IndexOf(left) + (left.Length);
            if (string.IsNullOrEmpty(right))
            {
                return input.Substring(i0);
            }
            int i1 = input.IndexOf(right, i0);
            if (i1 < i0)
            {
                return input.Substring(i0);
            }
            else
            {
                return input.Substring(i0, i1 - i0);
            }
        }

        public static string GetLast(this string input, string left, string right = null)
        {
            int i0 = input.LastIndexOf(left) + (left.Length);
            if (string.IsNullOrEmpty(right))
            {
                return input.Substring(i0);
            }
            int i1 = input.IndexOf(right, i0);
            if (i1 < i0)
            {
                return input.Substring(i0);
            }
            else
            {
                return input.Substring(i0, i1 - i0);
            }
        }

        public static string RemoveInvalidFileChars(this string input, string replaceto = "")
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                input = input.Replace(new string(c, 1), replaceto);
            }
            return input;
        }

        public static string RemoveInvalidPathChars(this string input, string replaceto = "")
        {
            foreach (char c in Path.GetInvalidPathChars())
            {
                input = input.Replace(new string(c, 1), replaceto);
            }
            return input;
        }

        public static string SingleDuration(this TimeSpan now)
        {
            if ((int)now.TotalDays >= 30)
            {
                int m = (int)(now.TotalDays / 30);
                return m + (m > 1 ? " Months" : " Month");
            }
            if ((int)now.TotalDays > 0)
            {
                return (int)now.TotalDays + ((int)now.TotalDays > 1 ? " Days" : " Day");
            }
            if ((int)now.TotalHours > 0)
            {
                return (int)now.TotalHours + ((int)now.TotalHours > 1 ? " Hours" : " Hour");
            }
            if ((int)now.TotalMinutes > 0)
            {
                return (int)now.TotalMinutes + ((int)now.TotalMinutes > 1 ? " Mins" : " Min");
            }
            if ((int)now.TotalSeconds > 0)
            {
                return (int)now.TotalSeconds + ((int)now.TotalSeconds > 1 ? " Mins" : " Min");
            }
            return "0 Mins";
        }

        public static string SingleMag(this int m, string form)
        {
            return m + (m > 1 ? form + "s" : form);
        }

        public static string SizeMag(this int value, System.Globalization.CultureInfo culture, int decimalPlaces = 3, string format = "N")
        {
            if (value < 0) { return "-" + SizeMag(-value, culture, decimalPlaces, format); }
            if (value == 0) { return value.ToString(format + decimalPlaces, culture); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1000);

            // 1L << (mag * 10) == 2 ^ (10 * mag)
            // [i.e. the number of bytes in the unit corresponding to mag]
            double adjustedSize = (double)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1000;
            }

            if (Math.Abs(adjustedSize % 1) <= (double.Epsilon * 100))
            {
                decimalPlaces = 0;
            }

            return adjustedSize.ToString(format + decimalPlaces, culture) + SizeSuffixes[mag];
        }

        public static string SizeSuffix(this long value, int decimalPlaces = 3)
        {
            return SizeSuffix((double)value, decimalPlaces);
            //if (value < 0) { return "-" + SizeSuffix(-value); }
            //if (value == 0) { return "0.0 bytes"; }

            //// mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            //int mag = (int)Math.Log(value, 1024);

            //// 1L << (mag * 10) == 2 ^ (10 * mag)
            //// [i.e. the number of bytes in the unit corresponding to mag]
            //decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            //// make adjustment when the value is large enough that
            //// it would round up to 1000 or more
            //if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            //{
            //    mag += 1;
            //    adjustedSize /= 1024;
            //}

            //return string.Format("{0:n" + decimalPlaces + "} {1}",
            //    adjustedSize,
            //    SizeSuffixes[mag]);
        }

        public static string SizeSuffix(this int value, int decimalPlaces = 3)
        {
            return SizeSuffix((double)value, decimalPlaces);
            //if (value < 0) { return "-" + SizeSuffix(-value); }
            //if (value == 0) { return "0.0 bytes"; }

            //// mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            //int mag = (int)Math.Log(value, 1024);

            //// 1L << (mag * 10) == 2 ^ (10 * mag)
            //// [i.e. the number of bytes in the unit corresponding to mag]
            //decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            //// make adjustment when the value is large enough that
            //// it would round up to 1000 or more
            //if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            //{
            //    mag += 1;
            //    adjustedSize /= 1024;
            //}

            //return string.Format("{0:n" + decimalPlaces + "} {1}",
            //    adjustedSize,
            //    SizeSuffixes[mag]);
        }

        public static string SizeSuffix(this float value, int decimalPlaces = 3)
        {
            return SizeSuffix((double)value, decimalPlaces);
            //if (value < 0) { return "-" + SizeSuffix(-value); }
            //if (value == 0) { return "0.0 bytes"; }

            //// mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            //int mag = (int)Math.Log(value, 1024);

            //// 1L << (mag * 10) == 2 ^ (10 * mag)
            //// [i.e. the number of bytes in the unit corresponding to mag]
            //decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            //// make adjustment when the value is large enough that
            //// it would round up to 1000 or more
            //if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            //{
            //    mag += 1;
            //    adjustedSize /= 1024;
            //}

            //return string.Format("{0:n" + decimalPlaces + "} {1}",
            //    adjustedSize,
            //    SizeSuffixes[mag]);
        }

        public static string SizeSuffix(this double value, int decimalPlaces = 3)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0B"; }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag)
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "}{1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }

        public static string RemoveInvalidFileChars(this string input)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                input = input.Replace(new string(c, 1), "");
            }
            return input;
        }

        public static string RemoveInvalidPathChars(this string input)
        {
            foreach (char c in Path.GetInvalidPathChars())
            {
                input = input.Replace(new string(c, 1), "");
            }
            return input;
        }
    }

    public static class LiXMath
    {
        public const int KIBIBYTE = 1024;
        public const int MIBIBYTE = 1048576;

        public static decimal Constrain(decimal x, decimal min, decimal max)
        {
            return Math.Max(min, Math.Max(max, x));
        }

        public static int Constrain(int x, int min, int max)
        {
            return Math.Max(min, Math.Max(max, x));
        }

        public static long Constrain(long x, long min, long max)
        {
            return Math.Max(min, Math.Max(max, x));
        }

        public static double Constrain(double x, double min, double max)
        {
            return Math.Max(min, Math.Max(max, x));
        }

        public static float Constrain(float x, float min, float max)
        {
            return Math.Max(min, Math.Max(max, x));
        }

        public static decimal Map(decimal x, decimal in_min, decimal in_max, decimal out_min, decimal out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        public static long Map(long x, long in_min, long in_max, long out_min, long out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        public static int Map(int x, int in_min, int in_max, int out_min, int out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        public static double Map(double x, double in_min, double in_max, double out_min, double out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        public static float Map(float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
    }
}