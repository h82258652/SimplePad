using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.UWP.UI.ViewModels;
using Windows.Graphics.Printing;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Printing;

namespace SimplePad.UWP.UI.Views;

public sealed partial class AppMenuBar : UserControl
{
    public static readonly DependencyProperty ShellViewModelProperty = DependencyProperty.Register(
        nameof(ShellViewModel),
        typeof(ShellViewModel),
        typeof(AppMenuBar),
        null);

    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(TextBox),
        typeof(AppMenuBar),
        new PropertyMetadata(null, OnTextBoxChanged));

    private readonly IAppSettings _appSettings;
    private readonly AppState _appState;

    private PrintDocument? _printDocument;
    private IPrintDocumentSource? _printDocumentSource;

    public AppMenuBar()
    {
        _appSettings = ServiceLocator.Current.GetRequiredService<IAppSettings>();
        _appState = ServiceLocator.Current.GetRequiredService<AppState>();

        InitializeComponent();

        PrintMenuItem.Visibility = PrintManager.IsSupported() ? Visibility.Visible : Visibility.Collapsed;
    }

    public ShellViewModel? ShellViewModel
    {
        get => (ShellViewModel?)GetValue(ShellViewModelProperty);
        set => SetValue(ShellViewModelProperty, value);
    }

    public TextBox? TextBox
    {
        get => (TextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppMenuBar self = (AppMenuBar)d;
        TextBox? oldTextBox = (TextBox?)e.OldValue;
        if (oldTextBox is not null)
        {
            oldTextBox.TextChanged -= self.OnTextBoxTextChanged;
        }

        TextBox? newTextBox = (TextBox?)e.NewValue;
        if (newTextBox is not null)
        {
            newTextBox.TextChanged += self.OnTextBoxTextChanged;
        }

        self.UpdateUndoMenuFlyoutItem();
    }

    private void OnCloseTabClick(object sender, RoutedEventArgs e)
    {
    }

    private void OnCopyClick(object sender, RoutedEventArgs e)
    {
        TextBox?.CopySelectionToClipboard();
    }

    private void OnCutClick(object sender, RoutedEventArgs e)
    {
        TextBox?.CutSelectionToClipboard();
    }

    private void OnDeleteClick(object sender, RoutedEventArgs e)
    {
    }

    private void OnFindClick(object sender, RoutedEventArgs e)
    {
    }

    private void OnFontClick(object sender, RoutedEventArgs e)
    {
    }

    private void OnGoToClick(object sender, RoutedEventArgs e)
    {
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (PrintManager.IsSupported())
        {
            PrintManager.GetForCurrentView().PrintTaskRequested += OnPrintTaskRequested;
        }
    }

    private void OnNewTabClick(object sender, RoutedEventArgs e)
    {
        ShellViewModel?.AddEditor();
    }

    private void OnNewWindowClick(object sender, RoutedEventArgs e)
    {
    }

    private async void OnOpenClick(object sender, RoutedEventArgs e)
    {
        if (TextBox is not { } textBox)
        {
            return;
        }

        FileOpenPicker fileOpenPicker = new();
        fileOpenPicker.FileTypeFilter.Add(".txt");
        fileOpenPicker.FileTypeFilter.Add("*");
        StorageFile? file = await fileOpenPicker.PickSingleFileAsync();
        if (file is null)
        {
            return;
        }

        string text = await FileIO.ReadTextAsync(file);
        textBox.Text = text;
    }

    private void OnPasteClick(object sender, RoutedEventArgs e)
    {
        TextBox?.PasteFromClipboard();
    }

    private async void OnPrintClick(object sender, RoutedEventArgs e)
    {
        if (PrintManager.IsSupported())
        {
            _printDocument = new PrintDocument();
            _printDocumentSource = _printDocument.DocumentSource;
            _printDocument.Paginate += OnPrintDocumentPaginate;
            _printDocument.GetPreviewPage += OnPrintDocumentGetPreviewPage;
            _printDocument.AddPages += OnPrintDocumentAddPages;

            _ = await PrintManager.ShowPrintUIAsync();
        }
    }

    private void OnPrintDocumentAddPages(object sender, AddPagesEventArgs e)
    {
        _printDocument.AddPage(TextBox);
        _printDocument.AddPagesComplete();
    }

    private void OnPrintDocumentGetPreviewPage(object sender, GetPreviewPageEventArgs e)
    {
        _printDocument.SetPreviewPage(e.PageNumber, TextBox);
    }

    private void OnPrintDocumentPaginate(object sender, PaginateEventArgs e)
    {
    }

    private void OnPrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs args)
    {
        args.Request.CreatePrintTask("SimplePad", printTaskSourceRequested =>
        {
            printTaskSourceRequested.SetSource(_printDocumentSource);
        });
    }

    private void OnReplaceClick(object sender, RoutedEventArgs e)
    {
    }

    private void OnRestoreDefaultZoomClick(object sender, RoutedEventArgs e)
    {
        _appState.ResetZoomFactor();
    }

    private void OnSaveAllClick(object sender, RoutedEventArgs e)
    {
    }

    private async void OnSaveAsClick(object sender, RoutedEventArgs e)
    {
        if (TextBox is not { } textBox)
        {
            return;
        }

        FileSavePicker fileSavePicker = new();
        fileSavePicker.FileTypeChoices.Add("Text documents", new List<string>() { ".txt" });
        StorageFile? file = await fileSavePicker.PickSaveFileAsync();
        if (file is not null)
        {
            await FileIO.WriteTextAsync(file, textBox.Text);
        }
    }

    private void OnSaveClick(object sender, RoutedEventArgs e)
    {
    }

    private void OnSelectAllClick(object sender, RoutedEventArgs e)
    {
        TextBox?.SelectAll();
    }

    private void OnSettingsButtonClick(object sender, RoutedEventArgs e)
    {
        ShellViewModel?.IsSettingsViewVisible = true;
    }

    private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateUndoMenuFlyoutItem();
    }

    private void OnTimeDateClick(object sender, RoutedEventArgs e)
    {
        TextBox.SelectedText = DateTime.Now.ToString("hh:mm tt MM/dd/yyyy");
        TextBox.SelectionLength = 0;
    }

    private void OnUndoClick(object sender, RoutedEventArgs e)
    {
        if (TextBox is { CanUndo: true } textBox)
        {
            textBox.Undo();
        }
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (PrintManager.IsSupported())
        {
            PrintManager.GetForCurrentView().PrintTaskRequested -= OnPrintTaskRequested;
        }
    }

    private void OnZoomInClick(object sender, RoutedEventArgs e)
    {
        _appState.ZoomIn();
    }

    private void OnZoomOutClick(object sender, RoutedEventArgs e)
    {
        _appState.ZoomOut();
    }

    private void UpdateUndoMenuFlyoutItem()
    {
        UndoMenuFlyoutItem.IsEnabled = TextBox is { CanUndo: true };
    }
}
