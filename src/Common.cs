﻿namespace ARVTech.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    public static class Common
    {
        //  public const string UriBaseApiString = @"https://localhost:7104/api";

        //  public const string UriBaseApiString = @"http://api.arvtech.com.br/api";

        public static string ApplyDefaultFilterQueryString(string uri, Dictionary<string, string> filters)
        {
            string requestUri = uri;

            if (filters.Count > 0)
            {
                requestUri = string.Concat(requestUri, "?");

                //var url = string.Format(
                //    CultureInfo.InvariantCulture, 
                //    requestUri,
                //    HttpUtility.UrlEncode(
                //        string.Join(
                //            "&",
                //            filters.Select(kvp =>
                //                string.Format(
                //                    CultureInfo.InvariantCulture,
                //                    "{0}={1}",
                //                    kvp.Key,
                //                    kvp.Value)))));

                foreach (var item in filters)
                {
                    requestUri = string.Concat(
                        requestUri,
                        "$",
                        item.Key,
                        "=",
                        item.Value);

                    if (!filters[item.Key].Equals(filters.Last().Value))
                    {
                        requestUri = string.Concat(
                            requestUri, "&");
                    }
                }
            }

            return requestUri;
        }

        public static string ApplySpecificFilterQueryString(string uri, Dictionary<string, string> filters)
        {
            string requestUri = uri;

            if (filters.Count > 0)
            {
                requestUri = string.Concat(requestUri, "&");

                requestUri = string.Concat(requestUri, "$filter=");

                foreach (var item in filters)
                {
                    requestUri = string.Concat(
                        requestUri,
                        item.Key,
                        " eq ",
                        "'",
                        item.Value,
                        "'");

                    if (!filters[item.Key].Equals(filters.Last().Value))
                    {
                        requestUri = string.Concat(
                            requestUri, "&");
                    }
                }
            }

            return requestUri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameParts"></param>
        /// <returns></returns>
        public static string GetFirstName(string fullName)
        {
            if (fullName == null ||
                string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(
                    nameof(
                        fullName));
            }

            if (!fullName.Contains(' '))
            {
                return fullName;
            }
            else
            {
                var nameParts = fullName.Split(' ');

                return nameParts[0];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IPAddress? GetIP()
        {
            /*
             * https://ipinfo.io/ip/
             * https://api.ipify.org/
             * https://icanhazip.com/
             * https://checkip.amazonaws.com/
             * https://wtfismyip.com/text
            */

            var services = new List<string>()
               {
                "https://ipv4.icanhazip.com",
                "https://api.ipify.org",
                "https://ipinfo.io/ip",
                "https://checkip.amazonaws.com",
                "https://wtfismyip.com/text",
                "http://icanhazip.com"
               };

            using (var httpClient = new HttpClient())
            {
                foreach (var service in services)
                {
                    try
                    {
                        return IPAddress.Parse(httpClient.GetStringAsync(service).Result);
                    }
                    catch { }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetLastName(string fullName)
        {
            if (fullName == null ||
                string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(
                    nameof(
                        fullName));
            }

            if (!fullName.Contains(' '))
            {
                return fullName;
            }
            else
            {
                var nameParts = fullName.Split(' ');

                return nameParts[nameParts.Length - 1];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetTokenBase64Encode(string username, string password)
        {
            string textEncode = $"{username}:{password}";

            byte[] textBytes = Encoding.UTF8.GetBytes(
                textEncode);

            return Convert.ToBase64String(
                textBytes);
        }
    }
}