using System;
using System.Collections.Generic;
using System.Text;
using SimplePad.Editor;
using SimplePad.UWP.UI.Controls;
using Windows.UI.Xaml;

namespace SimplePad.UWP.UI.Views
{
    public sealed partial class TextBoxAdapter : DependencyObject, IAppTextBox
    {
        public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
            nameof(TextBox),
            typeof(AppTextBox),
            typeof(TextBoxAdapter),
            new PropertyMetadata(null, OnTextBoxChanged));

        private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (TextBoxAdapter)d;
            var newTextBox = (AppTextBox?)e.NewValue;
            if (newTextBox != null)
            {
                newTextBox.TextChanged += self.NewTextBox_TextChanged;
                newTextBox.RegisterPropertyChangedCallback(AppTextBox.CursorPositionProperty, self.Xooooo);
                newTextBox.SelectionChanged += self.NewTextBox_SelectionChanged;

                self.Text = newTextBox.Text;
                self.SelectionLength = newTextBox.SelectionLength;
            }
        }

        private void NewTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SelectionLength = TextBox?.SelectionLength ?? 0;
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private   void Xooooo(DependencyObject sender, DependencyProperty dp)
        {
            this.CursorPosition = TextBox?.CursorPosition ?? new CursorPosition(1, 1);

            CursorPositionChanged?.Invoke(this, CursorPosition);
        }

        private void NewTextBox_TextChanged(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
        { 
            Text = TextBox?.Text ?? string.Empty;
            TextChanged?.Invoke(this, Text);
        }

        public AppTextBox? TextBox
        {
            get
            {
                return (AppTextBox?)GetValue(TextBoxProperty);
            }
            set
            {
                SetValue(TextBoxProperty, value);
            }
        }

        public int SelectionLength { get; private set; }

        public CursorPosition CursorPosition { get; private set; } = new CursorPosition(1, 1);

        public string Text { get; private set; } = string.Empty;

        public event EventHandler<CursorPosition>? CursorPositionChanged;
        public event EventHandler<string>? TextChanged;
        public event EventHandler? SelectionChanged;
    }
}
