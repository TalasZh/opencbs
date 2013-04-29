namespace Octopus.Enums
{
    public static class OGender
    {
        public const char Male = 'M';
        public const char Female = 'F';

        public static bool CheckGender(char genderToCheck)
        {
            return (genderToCheck == Male) || (genderToCheck == Female);
        }
    }
}
