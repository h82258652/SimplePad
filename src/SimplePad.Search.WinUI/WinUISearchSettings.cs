using SimplePad.Core.Settings;
using System;
using System.Threading.Tasks;

namespace SimplePad.Search;

internal sealed class WinUISearchSettings : AppSettingsBase, ISearchSettings
{
    public event EventHandler<bool>? IsMatchCaseChanged;

    public event EventHandler<bool>? IsWrapAroundChanged;

    public bool IsMatchCase { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
   
    public bool IsWrapAround { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override Task LoadAsync()
    {
        throw new NotImplementedException();
    }

    public override Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}