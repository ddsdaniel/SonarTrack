namespace SonarTrack.Application.Extensions
{
    public static class StringExtensions
    {
        public static bool TryGetEnum<TEnum>(this string source, out TEnum result) where TEnum : struct
        {
            return Enum.TryParse(source, true, out result) && Enum.IsDefined(typeof(TEnum), result);
        }
    }
}
