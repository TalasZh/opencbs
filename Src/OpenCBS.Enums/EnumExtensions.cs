using System;

namespace Octopus.Enums
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum enumaration)
        {
            var enumType = enumaration.GetType();
            return Enum.GetName(enumType, enumaration);
        }
    }
}
