using System.Text.RegularExpressions;

namespace SimplePad.Search;

public static class SearchSettingsHelper
{
    public static RegexOptions GetRegexOptions(ISearchSettings searchSettings)
    {
        RegexOptions regexOptions = RegexOptions.None;
        if (!searchSettings.IsMatchCase)
        {
            regexOptions |= RegexOptions.IgnoreCase;
        }

        if (searchSettings.IsWrapAround)
        {
            regexOptions |= RegexOptions.Multiline;
        }

        return regexOptions;
    }
}
