using System;
using SimplePad.Core.Settings;

namespace SimplePad.Search;

public interface ISearchSettings : IAppSettings
{
    event EventHandler<bool>? IsMatchCaseChanged;

    event EventHandler<bool>? IsWrapAroundChanged;

    bool IsMatchCase { get; set; }

    bool IsWrapAround { get; set; }
}
