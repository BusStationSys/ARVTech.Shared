namespace ARVTech.Shared.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class EnumExtension
    {
        //public static string GetDisplayName(this Enum value)
        //{
        //    var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        //    var attribute = member?.GetCustomAttribute<DisplayAttribute>();
        //    return attribute?.GetName() ?? value.ToString();
        //}

        private static readonly Dictionary<Enum, string> Cache = new();

        /// <summary>
        /// Gets the display name of an enum value. It first checks for a <see cref="DisplayAttribute"/> and then a <see cref="DescriptionAttribute"/>. If neither is found, it returns the enum value's name.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The display name of the enum value.</returns>
        public static string GetDisplayName(this Enum value)
        {
            if (Cache.TryGetValue(value, out var cached))
                return cached;

            var type = value.GetType();
            var name = value.ToString();

            var member = type.GetMember(name).FirstOrDefault();

            if (member is null)
                return name;

            var displayAttr = member.GetCustomAttribute<DisplayAttribute>();
            if (displayAttr is not null)
                return Cache[value] = displayAttr.GetName();

            var descriptionAttr = member.GetCustomAttribute<DescriptionAttribute>();
            if (descriptionAttr is not null)
                return Cache[value] = descriptionAttr.Description;

            return Cache[value] = name;
        }
    }
}