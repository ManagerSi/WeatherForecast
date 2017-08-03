using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.Globalization;
using System.Reflection;
using WeatherLib.Model;
using System.Configuration;
namespace WeatherLib.Utility
{
    public static class ClassExtension
    {

        public static int GetRPCWeekNumber(this DateTime dt)
        {
            DayOfWeek firstDOW = System.DayOfWeek.Monday;
            return Configuration.RPCCalendar.GetWeekOfYear(dt, Configuration.RPCWeekRule, firstDOW);
        }
        public static DateTime UtcToRPC(this DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dt.ToUniversalTime(), Configuration.RPCTimeZone);
        }

        public static string ToDateTimeString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string ToShortDateTimeString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm");
        }
        public static string ToDateTimeDigitString(this DateTime dt)
        {
            return dt.ToString("yyyyMMddHHmmss");
        }

        public static string ToYearMonthString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM");
        }
        //532获取当前上报日期
        public static string GetVstYearMonthString()
        {
            var day = DateTime.Now.Day;
            var startDay = int.Parse(ConfigurationManager.AppSettings["VstStartDate"]);
            var endDay = int.Parse(ConfigurationManager.AppSettings["VstEndDate"]);
            //当前日期小于上报开始日期，上报为上上个月的数据
            if (day < startDay)
            {
                return ToYearMonthString(DateTime.Now.AddMonths(-2));
            }
            //当前日期大于上报开始日期，上报为上个月的数据
            return ToYearMonthString(DateTime.Now.AddMonths(-1));
        }
        public static string GetTBBSVstYearMonthString()
        {
            var day = DateTime.Now.Day;
            var startDay = int.Parse(ConfigurationManager.AppSettings["TBBSVstStartDate"]);
            var endDay = int.Parse(ConfigurationManager.AppSettings["TBBSVstEndDate"]);
            //当前日期小于上报开始日期，上报为上上个月的数据
            if (day < startDay)
            {
                return ToYearMonthString(DateTime.Now.AddMonths(-2));
            }
            //当前日期大于上报开始日期，上报为上个月的数据
            return ToYearMonthString(DateTime.Now.AddMonths(-1));//--正常
            //return ToYearMonthString(DateTime.Now.AddMonths(-2));//设置上报月份
        }
        public static string GetVstViewYearMonthString()
        {
            var day = DateTime.Now.Day;
            var startDay = int.Parse(ConfigurationManager.AppSettings["VstStartDate"]);
            var endDay = int.Parse(ConfigurationManager.AppSettings["VstEndDate"]);
            if (day <= endDay)
            {
                return ToYearMonthString(DateTime.Now.AddMonths(-2));
            }
            //当前日期大于上报开始日期，上报为上个月的数据
            return ToYearMonthString(DateTime.Now.AddMonths(-1));
        }

        public static DateTime YearMonthToDate(this string bizDate)
        {
            if (bizDate.Length != 7)
            {
                throw new ArgumentException("Year Month string's Length is not 7", "bizDate");
            }
            return DateTime.Parse(bizDate + "-01");
        }

        public static string YearMonthToBizDate(this string bizDate) { return bizDate.YearMonthToDate().ToDateString(); }

        public static string ToDateString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        public static string ToLocalDateLongString(this DateTime dt)
        {
            return dt.ToString("yyyy年MM月dd日 dddd");
        }
        public static string ToLocalYearMonthString(this DateTime dt)
        {
            return dt.ToString("yyyy年MM月");
        }
          public static string ToShortEnMonthName(this DateTime dt) {
            return dt.ToString("MMM",CultureInfo.GetCultureInfo("en-US"));
          }
    public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return dt.AddDays(1 - dt.Day);
        }
        public static DateTime FirstDayOfYear(this DateTime dt)
        {
            return dt.AddMonths(1 - dt.Month).FirstDayOfMonth();
        }
        /// <summary>
        /// 返回当天的最后分钟
        /// </summary>
        public static DateTime LastMinuteOfDay(this DateTime dt)
        {
            return dt.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        /// <summary>
        /// 本周1
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime MondayOfThisWeek(this DateTime dt)
        {
            return dt.LastDayOfWeek(DayOfWeek.Monday);
        }


        public static DateTime SundayOfThisWeek(this DateTime dt)
        {
            return dt.MondayOfThisWeek().AddDays(6);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1);
        }
        public static DateTime FirstDayOfLastMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(-1);
        }
        public static DateTime FirstDayOfQuarter(this DateTime dt)
        {
            var date = dt.FirstDayOfMonth();
            while (date.Month % 3 != 1)
            {
                date = date.AddMonths(-1);
            }
            return date;
        }
        /// <summary>
        /// 获得当前日期是当前月的第几个周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int WeekOfMonth(this DateTime dt)
        {
            int weekNo = 1;
            //每月个第一个星期天是几号
            var firstSunday = dt.FirstDayOfMonth().SundayOfThisWeek().Day;
            var currentDay = dt.Day;
            var day = currentDay - firstSunday;
            if (day > 0)
            {
                weekNo = (int)(currentDay / 7) + 1;
            }

            return weekNo;
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }
        public static DateTime LastDayOfWeek(this DateTime dt, DayOfWeek day)
        {
            var cur = dt;
            while (cur.DayOfWeek != day)
            {
                cur = cur.AddDays(-1);
            }
            return cur;
        }
        public static DateTime NextDayOfWeek(this DateTime dt, DayOfWeek day)
        {
            var cur = dt.AddDays(1);
            while (cur.DayOfWeek != day)
            {
                cur = cur.AddDays(1);
            }
            return cur;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static string ConvertDateTimeToTimeStamp(this DateTime dt) {
          System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970,1,1));
          return ((int)(dt - startTime).TotalSeconds).ToString();
        }
        /// <summary>
        /// Unix时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetTimeFromTimeStamp(this string timeStamp) {
          DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970,1,1));
          long lTime = long.Parse(timeStamp + "0000000");
          TimeSpan toNow = new TimeSpan(lTime);
          return dtStart.Add(toNow);
        }
        public static int MonthNumSince(this DateTime d1, DateTime d2)
        {
            return (d1.Year - d2.Year) * 12 + d1.Month - d2.Month;
        }

        public static void AddAll<T>(this ICollection<T> col, IEnumerable<T> listToAdd)
        {
            foreach (T obj in listToAdd)
            {
                col.Add(obj);
            }
        }
        public static void RemoveAll<T>(this ICollection<T> col, IEnumerable<T> listToAdd)
        {
            foreach (T obj in listToAdd)
            {
                col.Remove(obj);
            }
        }


        public static RouteValueDictionary Update(this RouteValueDictionary obj, object dict)
        {
            return obj.UpdateWithPrefix("", new RouteValueDictionary(dict));
        }

        public static RouteValueDictionary UpdateWith(this RouteValueDictionary obj, IDictionary<string, object> dict)
        {
            return obj.UpdateWithPrefix("", dict);
        }

        public static RouteValueDictionary UpdateWithPrefix(this RouteValueDictionary obj, string prefix, IDictionary<string, object> dict)
        {
            if (dict == null)
            {
                return obj;
            }
            foreach (var i in dict)
            {
                string key = prefix + i.Key;
                obj[key] = i.Value;
            }
            return obj;
        }

        /// <summary>
        /// 这个函数将便利form中 key + id键
        /// 并将 key, form[key + id]传给 fillFunc
        /// 如果 fillFunc返回的非空对象将添加到items 中
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="form"></param>
        /// <param name="key">key前缀</param>
        /// <param name="IDs">所有需要遍历的ID</param>
        /// <param name="items">用于填写的项目集合</param>
        /// <param name="fillFunc">用于填充的函数</param>
        /// <param name="skipNull">是否跳过form[key + id]为空的对象</param>

        public static void FillItems<T>(this FormCollection form, string keyPrefix,
          IEnumerable<decimal> IDs, ICollection<T> items,
          Func<decimal, string, T> fillFunc, bool skipNull = true)
        {
            foreach (var id in IDs)
            {
                string value = form[keyPrefix + id];
                if (skipNull && value == null)
                {
                    continue;
                }
                T obj = fillFunc(id, value);
                if (null != obj)
                {
                    items.Add(obj);
                }
            }
        }

        public static string ToPostString(this IDictionary<string, string> dict, Encoding encoding)
        {
            return String.Join("&", dict.ToArray()
              .Select(
                i => string.Format(
                  "{0}={1}",
                  i.Key, i.Value
                )
              )
            );
        }
        public static string ToPostString(this IDictionary<string, string> dict) { return dict.ToPostString(Encoding.Default); }
        public static string ToPostString(this RouteValueDictionary dict)
        {
            return dict.Where(i => i.Value != null).ToDictionary(i => i.Key, i => i.Value.ToString())
              .ToPostString();
        }
        public static string ToQueryString(this IDictionary<string, string> dict, Encoding encoding)
        {
            return String.Join("&", dict.ToArray()
              .Select(
                i => string.Format(
                  "{0}={1}",
                  i.Key, HttpUtility.UrlEncode(i.Value, encoding)
                )
              )
            );
        }

        public static string ToQueryString(this IDictionary<string, string> dict) { return dict == null ? string.Empty : dict.ToQueryString(Encoding.Default); }


        public static string ToQueryString(this RouteValueDictionary dict)
        {
            return dict.Where(i => i.Value != null).ToDictionary(i => i.Key, i => i.Value.ToString())
              .ToQueryString();
        }


        public static RouteValueDictionary ToRouteDict(this object obj)
        {
            var dict = new RouteValueDictionary(obj);
            if (obj is IRemoveNotQueryUrlParams)
            {
                var type = obj.GetType();
                foreach (var p in type.GetProperties(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance))
                {
                    if (p.HasAttribute<NonQueryUrlEncodeAttribute>())
                    {
                        dict.Remove(p.Name);
                    }
                }
            }
            return dict;
        }



        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> iter)
        {
            return new HashSet<T>(iter);
        }

        public static HashSet<O> ToHashSet<T, O>(this IEnumerable<T> iter, Func<T, O> selector)
        {
            return new HashSet<O>(iter.Select(selector));
        }

        public static List<int> ToIntList(this IEnumerable<decimal> iter) { return iter.Select(i => (int)i).ToList(); }
        public static List<int?> ToIntList(this IEnumerable<decimal?> iter) { return iter.Select(i => (int?)i).ToList(); }

        /// <summary>
        /// 以逗号分隔为Decimal List
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<decimal> ToDecimalList(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new List<decimal>();
            }
            return str.Split(',').Select(i => decimal.Parse(i)).ToList();
        }
        /// <summary>
        /// 以逗号分隔为Int List
        /// </summary>
        public static List<int> ToIntList(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new List<int>();
            }
            return str.Split(',').Select(i => int.Parse(i)).ToList();
        }


        public static string ToMoneyString(this decimal value)
        {
            return value.ToString("N", Configuration.MoneyFormat);
        }

        public static string ToIntMoneyString(this decimal value)
        {
            return value.ToString("N", Configuration.IntMoneyFormat);
        }


        public static string ToIntString(this decimal value)
        {
            return value.ToString("0");
        }


        public static string ToIntPercentageString(this decimal value)
        {
            return (value * 100).ToIntString() + "%";
        }

        public static string ToPercentageString(this decimal value)
        {
            return (value * 100).ToString("0.0") + "%";
        }
        public static T GetModel<T>(this ActionResult actionResult) where T : class
        {
            object model;
            if (actionResult.GetType() == typeof(ViewResult))
            {
                ViewResult viewResult = (ViewResult)actionResult;
                model = viewResult.Model;
            }
            else if (actionResult.GetType() == typeof(PartialViewResult))
            {
                PartialViewResult partialViewResult = (PartialViewResult)actionResult;
                model = partialViewResult.Model;
            }
            else
            {
                throw new InvalidOperationException(string.Format("Actionresult of type {0} is not supported by ModelFromResult extractor.", actionResult.GetType()));
            }
            T typedModel = model as T;
            return typedModel;
        }

        public static bool HasAttribute<T>(this ICustomAttributeProvider t, bool inherit = true) where T : Attribute
        {
            return t.GetCustomAttributes(typeof(T), inherit).Length > 0;
        }

        public static ParallelQuery<T> AsConfigedParallel<T>(this IEnumerable<T> list)
        {
            return list.AsParallel().AsOrdered();
        }

        public static T MaxBy<T, U>(this IEnumerable<T> source, Func<T, U> selector)
          where U : IComparable<U>
        {
            if (source == null) throw new ArgumentNullException("source");
            bool first = true;
            T maxObj = default(T);
            U maxKey = default(U);
            foreach (var item in source)
            {
                if (first)
                {
                    maxObj = item;
                    maxKey = selector(maxObj);
                    first = false;
                }
                else
                {
                    U currentKey = selector(item);
                    if (currentKey.CompareTo(maxKey) > 0)
                    {
                        maxKey = currentKey;
                        maxObj = item;
                    }
                }
            }
            if (first) throw new InvalidOperationException("Sequence is empty.");
            return maxObj;
        }

        public static T MinBy<T, U>(this IEnumerable<T> source, Func<T, U> selector)
          where U : IComparable<U>
        {
            if (source == null) throw new ArgumentNullException("source");
            bool first = true;
            T minObj = default(T);
            U minKey = default(U);
            foreach (var item in source)
            {
                if (first)
                {
                    minObj = item;
                    minKey = selector(minObj);
                    first = false;
                }
                else
                {
                    U currentKey = selector(item);
                    if (currentKey.CompareTo(minKey) < 0)
                    {
                        minKey = currentKey;
                        minObj = item;
                    }
                }
            }
            if (first) throw new InvalidOperationException("Sequence is empty.");
            return minObj;
        }

        public static int ColorToInt(this string color)
        {
            if (color.Length != 7 || !color.StartsWith("#"))
            {
                throw new ArgumentException("color not start with #");
            }
            var r = int.Parse(color.Substring(1, 2), NumberStyles.HexNumber);
            var g = int.Parse(color.Substring(3, 2), NumberStyles.HexNumber);
            var b = int.Parse(color.Substring(5, 2), NumberStyles.HexNumber);
            int rgb = ((r & 0x0ff) << 16) | ((g & 0x0ff) << 8) | (b & 0x0ff);
            return rgb;
        }

        //public static double DistaceFrom(this IGisData dist1, IGisData dist2)
        //{
        //    // 计算两个坐标点的距离
        //    if (!(dist1.Lat.HasValue && dist1.Lng.HasValue && dist2.Lng.HasValue && dist2.Lat.HasValue))
        //    {
        //        return -1;
        //    }
        //    const double DistanceLng = 102834.74258026089786013677476285;
        //    const double DistanceLat = 111712.69150641055729984301412873;
        //    double LngAbs = Math.Abs(((double)(dist1.Lng.Value - dist2.Lng.Value)) * DistanceLng);
        //    double LatAbs = Math.Abs(((double)(dist1.Lat.Value - dist2.Lat.Value)) * DistanceLat);
        //    return Math.Sqrt((LatAbs * LatAbs + LngAbs * LngAbs));
        //}
    }
}
