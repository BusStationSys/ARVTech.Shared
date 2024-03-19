namespace ARVTech.Shared.Extensions
{
    using System;
    using System.Text;
    using Newtonsoft.Json.Linq;

    public static partial class StringExtension
    {
        private readonly static Dictionary<string, string> foreign_characters = new()
        {
            { "äæǽ", "ae" },
            { "öœ", "oe" },
            { "ü", "ue" },
            { "Ä", "Ae" },
            { "Ü", "Ue" },
            { "Ö", "Oe" },
            { "ÀÁÂÃÄÅǺĀĂĄǍΑΆẢẠẦẪẨẬẰẮẴẲẶА", "A" },
            { "àáâãåǻāăąǎªαάảạầấẫẩậằắẵẳặа", "a" },
            { "Б", "B" },
            { "б", "b" },
            { "ÇĆĈĊČ", "C" },
            { "çćĉċč", "c" },
            { "Д", "D" },
            { "д", "d" },
            { "ÐĎĐΔ", "Dj" },
            { "ðďđδ", "dj" },
            { "ÈÉÊËĒĔĖĘĚΕΈẼẺẸỀẾỄỂỆЕЭ", "E" },
            { "èéêëēĕėęěέεẽẻẹềếễểệеэ", "e" },
            { "Ф", "F" },
            { "ф", "f" },
            { "ĜĞĠĢΓГҐ", "G" },
            { "ĝğġģγгґ", "g" },
            { "ĤĦ", "H" },
            { "ĥħ", "h" },
            { "ÌÍÎÏĨĪĬǏĮİΗΉΊΙΪỈỊИЫ", "I" },
            { "ìíîïĩīĭǐįıηήίιϊỉịиыї", "i" },
            { "Ĵ", "J" },
            { "ĵ", "j" },
            { "ĶΚК", "K" },
            { "ķκк", "k" },
            { "ĹĻĽĿŁΛЛ", "L" },
            { "ĺļľŀłλл", "l" },
            { "М", "M" },
            { "м", "m" },
            { "ÑŃŅŇΝН", "N" },
            { "ñńņňŉνн", "n" },
            { "ÒÓÔÕŌŎǑŐƠØǾΟΌΩΏỎỌỒỐỖỔỘỜỚỠỞỢО", "O" },
            { "òóôõōŏǒőơøǿºοόωώỏọồốỗổộờớỡởợо", "o" },
            { "П", "P" },
            { "п", "p" },
            { "ŔŖŘΡР", "R" },
            { "ŕŗřρр", "r" },
            { "ŚŜŞȘŠΣС", "S" },
            { "śŝşșšſσςс", "s" },
            { "ȚŢŤŦτТ", "T" },
            { "țţťŧт", "t" },
            { "ÙÚÛŨŪŬŮŰŲƯǓǕǗǙǛŨỦỤỪỨỮỬỰУ", "U" },
            { "ùúûũūŭůűųưǔǖǘǚǜυύϋủụừứữửựу", "u" },
            { "ÝŸŶΥΎΫỲỸỶỴЙ", "Y" },
            { "ýÿŷỳỹỷỵй", "y" },
            { "В", "V" },
            { "в", "v" },
            { "Ŵ", "W" },
            { "ŵ", "w" },
            { "ŹŻŽΖЗ", "Z" },
            { "źżžζз", "z" },
            { "ÆǼ", "AE" },
            { "ß", "ss" },
            { "Ĳ", "IJ" },
            { "ĳ", "ij" },
            { "Œ", "OE" },
            { "ƒ", "f" },
            { "ξ", "ks" },
            { "π", "p" },
            { "β", "v" },
            { "μ", "m" },
            { "ψ", "ps" },
            { "Ё", "Yo" },
            { "ё", "yo" },
            { "Є", "Ye" },
            { "є", "ye" },
            { "Ї", "Yi" },
            { "Ж", "Zh" },
            { "ж", "zh" },
            { "Х", "Kh" },
            { "х", "kh" },
            { "Ц", "Ts" },
            { "ц", "ts" },
            { "Ч", "Ch" },
            { "ч", "ch" },
            { "Ш", "Sh" },
            { "ш", "sh" },
            { "Щ", "Shch" },
            { "щ", "shch" },
            { "ЪъЬь", "" },
            { "Ю", "Yu" },
            { "ю", "yu" },
            { "Я", "Ya" },
            { "я", "ya" },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char RemoveDiacritics(this char c)
        {
            foreach (KeyValuePair<string, string> entry in foreign_characters)
            {
                if (entry.Key.Contains(c))
                    return entry.Value[0];
            }

            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string s)
        {
            var sbText = new StringBuilder(); 

            foreach (char c in s)
            {
                int len = sbText.Length;

                foreach (KeyValuePair<string, string> entry in foreign_characters)
                {
                    if (entry.Key.Contains(c))
                    {
                        sbText.Append(
                            entry.Value);
                        break;
                    }
                }

                if (len == sbText.Length)
                    sbText.Append(
                        c);
            }

            return sbText.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static bool IsValidJson(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return false;

            //  const string pattern = @"^[\],:{}\s]*$";
            //  const string jsonPattern = @"(?:[""'](?:\\.|[^""\\\r\n])*[""']|(?<o>\{)|(?<-o>\})|(?<a>\[)|(?<-a>\]))*?(?(o)(?!))(?(a)(?!))$";

            //        return Regex.IsMatch(jsonString, pattern) &&
            //            Regex.IsMatch(jsonString, jsonPattern);

            jsonString = jsonString.Trim();

            if ((jsonString.StartsWith('{') && jsonString.EndsWith('}')) ||
                (jsonString.StartsWith('[') && jsonString.EndsWith(']')))   //  For object or for array.
            {
                try
                {
                    JToken.Parse(
                        jsonString);

                    return true;
                }
                catch
                {
                    return false;
                }

                //catch (JsonReaderException ex1)
                //{
                //    //Exception in parsing json
                //    Console.WriteLine(jex.Message);
                //    return false;
                //}
                //catch (Exception ex) //some other exception
                //{
                //    Console.WriteLine(ex.ToString());
                //    return false;
                //}
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string FirstCharToUpper(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(
                    nameof(
                        input)),
                "" => throw new ArgumentException(
                    @$"{nameof(
                        input)} cannot be empty",
                    nameof(
                        input)),
                _ => string.Concat(
                    input[0].ToString().ToUpper(),
                    input.Substring(1)),
            };
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeSpan? ToTimeSpan(this string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 5)
                return null;

            var numberOfElements = value.Split(':').Length;

            var timeSpan = default(TimeSpan?);

            if (numberOfElements == 3)
            {
                timeSpan = new TimeSpan(
                    int.Parse(value.Split(':')[0]),                 // Hours
                    int.Parse(value.Split(':')[1]),                 // Minutes
                    int.Parse(value.Split(':')[2].Split('.')[0]));  // Seconds
            }
            else
            {
                timeSpan = new TimeSpan(
                    int.Parse(value.Split(':')[0]),                 // Hours
                    int.Parse(value.Split(':')[1]),
                    0);                                             // Minutes
            }

            return timeSpan;
        }
    }
}