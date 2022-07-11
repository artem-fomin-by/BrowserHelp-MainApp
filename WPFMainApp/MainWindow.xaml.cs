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
    private readonly App _parentApp;
    private readonly string _link;

    private const double HorizontalIndent = 8.0;
    private const double VerticalIndent = 2.0;
    private const double InternalIndent = 2.0;
    private const double MinimumWidth = 80.0;

    internal Browser? _selectedBrowser = null;
    internal int? _selectedBrowserIndex = null;

    public MainWindow(App app, string appName, Browser[] browsers, string? link)
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

        InitControls(browsers, width, maxHeight);

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

        var selectionPanel = (StackPanel)BrowsersStackPanel.Children[0];
        var button = (Button)selectionPanel.Children[1];

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

    private void InitControls(Browser[] browsers, double maxWidth, double maxHeight)
    {
        for(var index = 0; index < browsers.Length; index++)
        {
            var selectionPanel =
                CreateBrowserSelectionPanel(browsers[index], index, index == 0, maxWidth, maxHeight);

            BrowsersStackPanel.Children.Add(selectionPanel);
        }
    }

    private StackPanel CreateBrowserSelectionPanel
        (Browser browser, int index, bool isDefault, double width, double height)
    {
        var res = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, VerticalIndent, 0, 0)
    };

        var checkBox = new BrowserSelectionCheckBox(this, browser, index);
        checkBox.VerticalAlignment = VerticalAlignment.Center;
        checkBox.Margin = new Thickness(0, 0, InternalIndent, 0);

        var button = new Button
        {
            Content = browser.Name,
            Width = width,
            Height = height,
            IsDefault = isDefault,
        };

        button.Click += (_, _) =>
        {
            browser.Launch(_link);
            _parentApp.End(_selectedBrowser);
        };

        res.Height = height;
        res.Children.Add(checkBox);
        res.Children.Add(button);
        res.Width = checkBox.Width + InternalIndent + button.Width;

        return res;
    }

    private static Size MeasureString(
        string candidate, 
        FontFamily fontFamily, 
        FontStyle fontStyle, 
        FontWeight fontWeight, 
        FontStretch fontStretch, 
        double fontSize
        )
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

class BrowserSelectionCheckBox : CheckBox
{
    private readonly MainWindow _parent;
    private readonly Browser Browser;
    private readonly int Index;

    public BrowserSelectionCheckBox(MainWindow parent, Browser browser, int index)
    {
        Width = 16;
        Height = 16;

        _parent = parent;
        Browser = browser;
        Index = index;

        Checked += OnChecked_EventHandler;
        Unchecked += OnUnchecked_EventHandler;
    }

    private void OnChecked_EventHandler(object sender, RoutedEventArgs e)
    {
        if(_parent._selectedBrowserIndex != null)
        {
            var selectionPanel = (StackPanel)
                _parent.BrowsersStackPanel.Children[(int)_parent._selectedBrowserIndex];
            var previousCheckedBox = (BrowserSelectionCheckBox)selectionPanel.Children[0];
            previousCheckedBox.IsChecked = false;
        }

        _parent._selectedBrowser = Browser;
        _parent._selectedBrowserIndex = Index;
    }

    private void OnUnchecked_EventHandler(object sender, RoutedEventArgs e)
    {
        _parent._selectedBrowser = null;
        _parent._selectedBrowserIndex = null;
    }
}