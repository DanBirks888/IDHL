namespace DeveloperAssessment.Web.Extensions;

public static class StringExtensions
{
    public static string ToImageAvatarName(this string name)
    {
        return string.IsNullOrEmpty(name)
            ? string.Empty
            : name.Replace(" ", "+").Trim();
    }
}