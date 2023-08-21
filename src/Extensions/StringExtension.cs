namespace ARVTech.Shared.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class StringExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TreatStringWithAccent(this string value)
        {
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;

            byte[] utfBytes = utf8.GetBytes(
                value);

            byte[] isoBytes = Encoding.Convert(
                utf8,
                iso,
                utfBytes);

            return iso.GetString(
                isoBytes);
        }
    }
}