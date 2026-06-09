using System;
using System.Collections.Generic;
using SimplePad.Tabs;
using Windows.Graphics.Printing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Printing;

namespace SimplePad.Menu;

public sealed partial class PrintMenuItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TabProperty = DependencyProperty.Register(
        nameof(Tab),
        typeof(Tab),
        typeof(PrintMenuItem),
        null);

    private PrintDocument? _printDocument;
    private IPrintDocumentSource? _printDocumentSource;
    private readonly List<UIElement> _pages = new();
    private string _contentToPrint = string.Empty;
    private string _printTaskTitle = "SimplePad";

    public PrintMenuItem()
    {
        InitializeComponent();
    }

    public Tab? Tab
    {
        get => (Tab?)GetValue(TabProperty);
        set => SetValue(TabProperty, value);
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        if (Tab is not { } tab || !PrintManager.IsSupported())
        {
            return;
        }

        _contentToPrint = tab.Content;
        _printTaskTitle = tab.Title;

        _printDocument = new PrintDocument();
        _printDocumentSource = _printDocument.DocumentSource;

        var printManager = PrintManager.GetForCurrentView();
        printManager.PrintTaskRequested += OnPrintTaskRequested;

        _printDocument.Paginate += OnPaginate;
        _printDocument.GetPreviewPage += OnGetPreviewPage;
        _printDocument.AddPages += OnAddPages;

        try
        {
            await PrintManager.ShowPrintUIAsync();
        }
        catch (Exception)
        {
            // Print UI could not be shown
        }
        finally
        {
            printManager.PrintTaskRequested -= OnPrintTaskRequested;

            if (_printDocument is { } printDocument)
            {
                printDocument.Paginate -= OnPaginate;
                printDocument.GetPreviewPage -= OnGetPreviewPage;
                printDocument.AddPages -= OnAddPages;
            }

            _printDocument = null;
            _printDocumentSource = null;
            _pages.Clear();
        }
    }

    private void OnPrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs args)
    {
        args.Request.CreatePrintTask(
            $"SimplePad - {_printTaskTitle}",
            source => source.SetSource(_printDocumentSource));
    }

    private void OnPaginate(object sender, PaginateEventArgs e)
    {
        _pages.Clear();

        var printDoc = (PrintDocument)sender;
        var pageDescription = e.PrintTaskOptions.GetPageDescription(0);

        double pageWidth = pageDescription.PageSize.Width;
        double pageHeight = pageDescription.PageSize.Height;
        double marginLeft = pageDescription.ImageableRect.X;
        double marginTop = pageDescription.ImageableRect.Y;
        double printableWidth = pageDescription.ImageableRect.Width;
        double printableHeight = pageDescription.ImageableRect.Height;

        // First page uses a RichTextBlock with all content
        var richTextBlock = new RichTextBlock
        {
            FontFamily = new FontFamily("Consolas"),
            FontSize = 11,
            TextWrapping = TextWrapping.Wrap
        };

        var paragraph = new Paragraph();
        paragraph.Inlines.Add(new Run { Text = _contentToPrint });
        richTextBlock.Blocks.Add(paragraph);

        AddPage(richTextBlock, pageWidth, pageHeight, marginLeft, marginTop, printableWidth, printableHeight);

        // Overflow pages handle content that didn't fit on the first page
        RichTextBlockOverflow? lastOverflow = null;
        bool hasOverflow = richTextBlock.HasOverflowContent;
        const int maxPages = 10000;

        while (hasOverflow && _pages.Count < maxPages)
        {
            var overflowBlock = new RichTextBlockOverflow();

            if (lastOverflow is null)
            {
                richTextBlock.OverflowContentTarget = overflowBlock;
            }
            else
            {
                lastOverflow.OverflowContentTarget = overflowBlock;
            }

            AddPage(overflowBlock, pageWidth, pageHeight, marginLeft, marginTop, printableWidth, printableHeight);

            hasOverflow = overflowBlock.HasOverflowContent;
            lastOverflow = overflowBlock;
        }

        printDoc.SetPreviewPageCount(_pages.Count, PreviewPageCountType.Final);
    }

    private void AddPage(
        FrameworkElement content,
        double pageWidth,
        double pageHeight,
        double marginLeft,
        double marginTop,
        double printableWidth,
        double printableHeight)
    {
        content.Width = printableWidth;
        content.Height = printableHeight;

        var page = new Canvas
        {
            Width = pageWidth,
            Height = pageHeight
        };

        Canvas.SetLeft(content, marginLeft);
        Canvas.SetTop(content, marginTop);
        page.Children.Add(content);

        _pages.Add(page);

        page.InvalidateMeasure();
        page.UpdateLayout();
    }

    private void OnGetPreviewPage(object sender, GetPreviewPageEventArgs e)
    {
        var printDoc = (PrintDocument)sender;
        int index = e.PageNumber - 1;
        if (index >= 0 && index < _pages.Count)
        {
            printDoc.SetPreviewPage(e.PageNumber, _pages[index]);
        }
    }

    private void OnAddPages(object sender, AddPagesEventArgs e)
    {
        var printDoc = (PrintDocument)sender;
        foreach (var page in _pages)
        {
            printDoc.AddPage(page);
        }

        printDoc.AddPagesComplete();
    }
}