using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Logic;

namespace MainApp;

public partial class MainWindow : Window
{
    private readonly Application _parentApp;
    private readonly string _link;

    private const double HorizontalIndent = 8.0;
    private const double VerticalIndent = 2.0;
    private const double MinimumWidth = 80.0;

    public MainWindow(Application app, string appName, Browser[] browsers, string? link)
    {
        _parentApp = app;
        _link = link ?? "";

        InitializeComponent();

        Title = appName;
        FontSize = 12.0;

        var sizes = browsers.Select(x => x.Name)
            .Select(x => MeasureString(x, FontFamily, FontStyle, FontWeight, FontStretch, FontSize))
            .ToList();
        var maxWidth = sizes.Select(x => x.Width).Max();
        var width = Math.Max(maxWidth, MinimumWidth) + (HorizontalIndent * 2);
        var maxHeight = sizes.Select(x => x.Height).Max() + (VerticalIndent * 2);

        InitButtons(browsers, width, maxHeight);

        LinkLabel.Content = link;
    }

    private static Point ConvertCords(Point cords, Window window, int defaultXDPI = 96, int defaultYDPI = 96)
    {
        var helper = new WindowInteropHelper(window);
        var (xDPI, yDPI) = Interop.GetDpi(helper.Handle);
        var x = cords.X * ((double)defaultXDPI / xDPI);
        var y = cords.Y * ((double)defaultYDPI / yDPI);
        return new Point(x, y);
    }

    private void AdjustInitPosition()
    {
        var actualMousePosition = GetMousePosition();

        Left = actualMousePosition.X - (ActualWidth / 2);

        var button = (Button)ButtonsStackPanel.Children[0];

        Top = actualMousePosition.Y - SystemParameters.WindowCaptionHeight - button.Height;
    }

    private Point GetMousePosition()
    {

        var mousePosition = new Interop.Win32Point();
        Interop.GetCursorPos(ref mousePosition);
        var mousePositionPoint = new Point(mousePosition.X, mousePosition.Y);
        var actualMousePosition = ConvertCords(mousePositionPoint, this);
        return actualMousePosition;
    }

    private void InitButtons(Browser[] browsers, double maxWidth, double maxHeight)
    {
        ButtonsStackPanel.Width = maxWidth;

        for (var index = 0; index < browsers.Length; index++)
        {
            var browser = browsers[index];
            var button = new Button
            {
                Content = browser.Name,
                Width = maxWidth,
                Height = maxHeight,
                IsDefault = index == 0,
                Margin = new Thickness(0, VerticalIndent, 0, 0)
            };

            button.Click += (_, _) =>
            {
                browser.Launch(_link);
                _parentApp.Shutdown();
            };

            ButtonsStackPanel.Children.Add(button);
        }
    }

    private static Size MeasureString(string candidate, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double fontSize)
    {
        var formattedText = new FormattedText(
            candidate,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
            fontSize,
            Brushes.Black,
            new NumberSubstitution(),
            1);

        return new Size(formattedText.Width, formattedText.Height);
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        AdjustInitPosition();
    }
}
