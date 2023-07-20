namespace ARVTech.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Common
    {
        public const string UriBaseApiString = @"https://localhost:7104/api";

        public const string MediaTypes = @"application/json";

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