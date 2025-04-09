using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Material.Avalonia.Demo.ViewModels;

namespace Material.Avalonia.Demo.Pages;

public partial class FieldsLineUpDemo : UserControl {
    public FieldsLineUpDemo() {
        InitializeComponent();
    }

    private void HasClearButton_IsCheckedChanged(object? sender, RoutedEventArgs e) {
        bool shouldAddClearBtn = cbHasClearButton.IsChecked ?? false;

        foreach (var child in FieldsLineUpGrid.Children) {

            if (child.GetType() == typeof(TextBlock)) {
                continue;
            }

            if (child is not StyledElement styleable) {
                continue;
            }

            if (shouldAddClearBtn) {
                if (!styleable.Classes.Contains("clearButton")) {
                    styleable.Classes.Add("clearButton");
                }
            }
            else {
                if (styleable.Classes.Contains("clearButton")) {
                    styleable.Classes.Remove("clearButton");
                }
            }
        }
    }
}