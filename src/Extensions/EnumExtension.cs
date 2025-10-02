namespace ARVTech.Shared.Extensions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum value)
        {
            var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var attribute = member?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.GetName() ?? value.ToString();
        }
    }
}