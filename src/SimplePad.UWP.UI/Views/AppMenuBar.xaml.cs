using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Services.UWP;
using SimplePad.Settings;
using SimplePad.UWP.UI.Controls;
using SimplePad.UWP.UI.Dialogs;
using SimplePad.ViewModels;
using Windows.Graphics.Printing;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Printing;

namespace SimplePad.UWP.UI.Views;

public sealed partial class AppMenuBar : UserControl
{
    public static readonly DependencyProperty EditorViewModelProperty = DependencyProperty.Register(
        nameof(EditorViewModel),
        typeof(EditorViewModel),
        typeof(AppMenuBar),
        null);

    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(AppTextBox),
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

    public EditorViewModel? EditorViewModel
    {
        get => (EditorViewModel?)GetValue(EditorViewModelProperty);
        set => SetValue(EditorViewModelProperty, value);
    }

    public AppTextBox? TextBox
    {
        get => (AppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppMenuBar self = (AppMenuBar)d;
        TextBox? oldTextBox = (TextBox?)e.OldValue;
        if (oldTextBox is not null)
        {
            oldTextBox.TextChanged -= self.OnTextBoxTextChanged;
            oldTextBox.SelectionChanged -= self.OnTextBoxSelectionChanged;
        }

        TextBox? newTextBox = (TextBox?)e.NewValue;
        if (newTextBox is not null)
        {
            newTextBox.TextChanged += self.OnTextBoxTextChanged;
            newTextBox.SelectionChanged += self.OnTextBoxSelectionChanged;
        }

        self.UpdateUndoMenuFlyoutItem();
        self.UpdateCutMenuFlyoutItem();
        self.UpdateCopyMenuFlyoutItem();
        self.UpdateDeleteMenuFlyoutItem();
    }

    private async void OnCloseTabClick(object sender, RoutedEventArgs e)
    {
        if (EditorViewModel is { } editorViewModel)
        {
            await editorViewModel.ShellViewModel.CloseEditorAsync(EditorViewModel);
        }
    }

    private void OnCloseWindowClick(object sender, RoutedEventArgs e)
    {
        Window.Current.Close();
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
        if (TextBox is { SelectedText.Length: > 0 } textBox)
        {
            textBox.SelectedText = string.Empty;
        }
    }

    private void OnFindClick(object sender, RoutedEventArgs e)
    {
    }

    private void OnFontClick(object sender, RoutedEventArgs e)
    {
        if (EditorViewModel is { ShellViewModel: { } shellViewModel })
        {
            shellViewModel.IsSettingsViewVisible = true;
            shellViewModel.SettingsViewModel.IsFontSettingsExpanded = true;
        }
    }

    private async void OnGoToClick(object sender, RoutedEventArgs e)
    {
        if (TextBox is not { } textBox)
        {
            return;
        }

        string text = TextBox.Text;
        int totalLines = text.Split('\r').Length;

        GoToLineDialog goToLineDialog = new(textBox.CursorPosition.Row, totalLines);
        ContentDialogResult dialogResult = await goToLineDialog.ShowAsync();
        if (dialogResult == ContentDialogResult.Primary)
        {
            int i = 0;
            int row = 1;

            for (int index = 0; index < text.Length && row < goToLineDialog.LineNumber; index++)
            {
                char c = text[index];
                i++;

                if (c == '\r')
                {
                    row++;
                }
            }

            textBox.SelectionStart = i;
        }
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
        if (EditorViewModel is { ShellViewModel: { } shellViewModel })
        {
            shellViewModel.AddBlankEditor();
        }
    }

    private async void OnNewWindowClick(object sender, RoutedEventArgs e)
    {
        // TODO switch to legacy multiple window solution

        AppWindow appWindow = await AppWindow.TryCreateAsync();
        appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        ElementCompositionPreview.SetAppWindowContent(appWindow, new ShellView());
        _ = await appWindow.TryShowAsync();
    }

    private async void OnOpenClick(object sender, RoutedEventArgs e)
    {
        if (EditorViewModel is not { } editorViewModel)
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

        editorViewModel.ShellViewModel.AddEditorFromFile(new UWPFile(file));
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

    private async void OnSaveAllClick(object sender, RoutedEventArgs e)
    {
        if (EditorViewModel is { ShellViewModel: { } shellViewModel })
        {
            await shellViewModel.SaveAllAsync();
        }
    }

    private async void OnSaveAsClick(object sender, RoutedEventArgs e)
    {
        if (EditorViewModel is not { } editorViewModel)
        {
            return;
        }

        FileSavePicker fileSavePicker = new();
        fileSavePicker.FileTypeChoices.Add("Text documents", new List<string>() { ".txt" });
        StorageFile? file = await fileSavePicker.PickSaveFileAsync();
        if (file is not null)
        {
            editorViewModel.File = new UWPFile(file);
            await editorViewModel.SaveAsync();
        }
    }

    private async void OnSaveClick(object sender, RoutedEventArgs e)
    {
        if (EditorViewModel is { } editorViewModel)
        {
            await editorViewModel.SaveAsync();
        }
    }

    private void OnSelectAllClick(object sender, RoutedEventArgs e)
    {
        TextBox?.SelectAll();
    }

    private void OnSettingsButtonClick(object sender, RoutedEventArgs e)
    {
        if (EditorViewModel is { ShellViewModel: { } shellViewModel })
        {
            shellViewModel.IsSettingsViewVisible = true;
        }
    }

    private void OnTextBoxSelectionChanged(object sender, RoutedEventArgs e)
    {
        UpdateCutMenuFlyoutItem();
        UpdateCopyMenuFlyoutItem();
        UpdateDeleteMenuFlyoutItem();
    }

    private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateUndoMenuFlyoutItem();
    }

    private void OnTimeDateClick(object sender, RoutedEventArgs e)
    {
        if (TextBox is not { } textBox)
        {
            return;
        }

        string timeDateText = DateTime.Now.ToString("hh:mm tt MM/dd/yyyy");
        textBox.SelectedText = timeDateText;
        textBox.SelectionLength = 0;
        textBox.SelectionStart += timeDateText.Length;
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

    private void UpdateCopyMenuFlyoutItem()
    {
        DeleteMenuFlyoutItem.IsEnabled = TextBox is { SelectedText.Length: > 0 };
    }

    private void UpdateCutMenuFlyoutItem()
    {
        DeleteMenuFlyoutItem.IsEnabled = TextBox is { SelectedText.Length: > 0 };
    }

    private void UpdateDeleteMenuFlyoutItem()
    {
        DeleteMenuFlyoutItem.IsEnabled = TextBox is { SelectedText.Length: > 0 };
    }

    private void UpdateUndoMenuFlyoutItem()
    {
        UndoMenuFlyoutItem.IsEnabled = TextBox is { CanUndo: true };
    }

    private void OnFindNextClick(object sender, RoutedEventArgs e)
    {

    }

    private void OnFindPreviousClick(object sender, RoutedEventArgs e)
    {

    }
}
