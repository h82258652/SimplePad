namespace SimplePad.Fonts;

internal sealed class AvaloniaFontSettings : IFontSettings
{
    public string FontFamily
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public int FontSize
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public AppFontStyle FontStyle
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public event EventHandler<string>? FontFamilyChanged;
    public event EventHandler<int>? FontSizeChanged;
    public event EventHandler<AppFontStyle>? FontStyleChanged;

    public Task LoadAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
