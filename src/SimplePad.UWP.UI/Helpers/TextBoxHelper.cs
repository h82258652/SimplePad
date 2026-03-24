using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Helpers;

public static class TextBoxHelper
{
    // https://www.blakepell.com/blog/getting-the-cursor-row-and-column-position-in-a-uwp-textbox
    public static CursorPosition GetCursorPosition(TextBox textBox)
    {
        int endMarker = textBox.SelectionStart + textBox.SelectionLength;

        if (endMarker == 0)
        {
            return new CursorPosition(1, 1);
        }

        int i = 0;
        int col = 1;
        int row = 1;

        foreach (char c in textBox.Text)
        {
            i++;
            col++;

            if (c == '\r')
            {
                row++;
                col = 1;
            }

            if (i == endMarker)
            {
                return new CursorPosition(row, col);
            }
        }

        return new CursorPosition(row, col);
    }
}
