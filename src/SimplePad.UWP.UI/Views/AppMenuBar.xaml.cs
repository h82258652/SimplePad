using System;
using Windows.Graphics.Printing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Printing;

namespace SimplePad.UWP.UI.Views;

public sealed partial class AppMenuBar : UserControl
{
    private PrintDocument? _printDocument;
    private IPrintDocumentSource? _printDocumentSource;

    public AppMenuBar()
    {
        InitializeComponent();
        PrintMenuItem.Visibility = PrintManager.IsSupported()
            ? Visibility.Visible
            : Visibility.Collapsed;
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
        _printDocument.AddPage(new TextBox());
        _printDocument.AddPagesComplete();
    }

    private void OnPrintDocumentGetPreviewPage(object sender, GetPreviewPageEventArgs e)
    {
        _printDocument.SetPreviewPage(e.PageNumber, new TextBox());
    }

    private void OnPrintDocumentPaginate(object sender, PaginateEventArgs e) { }

    private void OnPrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs args)
    {
        args.Request.CreatePrintTask(
            "SimplePad",
            printTaskSourceRequested =>
            {
                printTaskSourceRequested.SetSource(_printDocumentSource);
            }
        );
    }
}
