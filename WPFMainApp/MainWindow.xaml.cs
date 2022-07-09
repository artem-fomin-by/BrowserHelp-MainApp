using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Logic;

namespace WPFMainApp;


public partial class MainWindow : Window
{
    private readonly Application ParentApp;
    private string Link;

    private const double Indent = 2.0;
    private const double LeftIndent = 13.0;
    private const double TopIndent = 36.0;

    public MainWindow(Application app, string appName, Browser[] browsers, string? link)
    {
        ParentApp = app;
        Link = link ?? "";

        InitializeComponent();

        Title = appName;
        FontSize = 12.0;

        var sizes = new[] {new[] {link ?? ""}, browsers.Select(x => x.Name)}
            .SelectMany(x => x)
            .Select(x => MeasureString(x, FontFamily, FontStyle, FontWeight, FontStretch, FontSize))
            .ToList();
        var maxWidth = sizes.Select(x => x.Width).Max() + (Indent * 2);
        var maxHeight = sizes.Select(x => x.Height).Max() + (Indent * 2);

        (LinkTextBox.Width, LinkTextBox.Height) = (maxWidth, maxHeight);
        LinkTextBox.Text = Link;
        LinkTextBox.TextChanged += LinkTextBoxTextChanged;

        InitButtons(browsers, maxWidth, maxHeight);

        Width = LinkTextBox.Width + LeftIndent;
        Height = ButtonsStackPanel.Height + LinkTextBox.Height + TopIndent;

        AdjustInitPosition(maxHeight);
    }

    private static Point ConvertCords(Point cords, Window window, int defaultXDPI = 96, int defaultYDPI = 96)
    {
        var helper = new WindowInteropHelper(window);
        var (xDPI, yDPI) = Interop.GetDpi(helper.Handle);
        var x = cords.X * ((double)defaultXDPI / xDPI);
        var y = cords.Y * ((double)defaultYDPI / yDPI);
        return new Point(x, y);
    }

    private void AdjustInitPosition(double h)
    {
        var mousePosition = new Interop.Win32Point();
        Interop.GetCursorPos(ref mousePosition);
        var mousePositionPoint = new Point(mousePosition.X, mousePosition.Y);
        var actualMousePosition = ConvertCords(mousePositionPoint, this);
        
        Left = actualMousePosition.X - (Width / 2);
        Top = actualMousePosition.Y - (h * 1.5 + TopIndent);
    }

    private void InitButtons(Browser[] browsers, double maxWidth, double maxHeight)
    {
        ButtonsStackPanel.Width = maxWidth;
        ButtonsStackPanel.Height = browsers.Length * maxHeight;

        foreach(var browser in browsers)
        {
            var button = new Button
            {
                Content = browser.Name,
                Width = maxWidth,
                Height = maxHeight
            };

            button.Click += (_, _) =>
            {
                browser.Launch(Link);
                ParentApp.Shutdown();
            };

            ButtonsStackPanel.Children.Add(button);
        }
    }

    private static Size MeasureString(
        string candidate, 
        FontFamily fontFamily, 
        FontStyle fontStyle, 
        FontWeight fontWeight, 
        FontStretch fontStretch,
        double fontSize)
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

    private void LinkTextBoxTextChanged(object? sender, TextChangedEventArgs e)
    {
        Link = LinkTextBox.Text;
    }
}
