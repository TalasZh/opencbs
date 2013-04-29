using System;

namespace Octopus.Enums
{
    [Serializable]
    public enum OClientTypes
    {
        Person = 1,
        Group = 2,
        All = 3,
        Corporate = 4,
        Village = 5
    }

    public static class OClientTypeExtensions
    {
        public static OClientTypes ConvertToClientType(this char toConvert)
        {
            switch (toConvert)
            {
                case 'C' :
                    return OClientTypes.Corporate;
                case 'G':
                    return OClientTypes.Group;
                case 'I':
                    return OClientTypes.Person;
                case 'V':
                    return OClientTypes.Village;
                default:
                    return OClientTypes.All;
            }
        }

        public static OClientTypes ConvertToClientType(this string toConvert)
        {
            if(toConvert == null) throw new ArgumentNullException("toConvert");
            if(toConvert.Length != 1)  throw new ArgumentException("Lenth of toConvert argument should be 1");

            return toConvert[0].ConvertToClientType();
        }
    }
}
