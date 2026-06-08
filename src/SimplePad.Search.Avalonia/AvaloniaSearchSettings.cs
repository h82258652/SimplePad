using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimplePad.Search;

internal sealed class AvaloniaSearchSettings : ISearchSettings
{
    public bool IsMatchCase { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsWrapAround { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event EventHandler<bool>? IsMatchCaseChanged;
    public event EventHandler<bool>? IsWrapAroundChanged;

    public Task LoadAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
