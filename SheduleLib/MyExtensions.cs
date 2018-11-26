using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheduleLib
{
    public static class MyExtensions
    {
        /// <summary>
        /// Получить часы и минуты из целого числа
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string ToTime(this int a)
        {
            int m = a % 60; // получаем число минут
            int h = (a / 60) % 24;  // получаем число часов

            return string.Format("{0}:{1}",h.ToString(), m.ToString());
        }
    }
}
