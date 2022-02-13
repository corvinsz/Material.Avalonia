using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;
using System;
using Avalonia.Controls.Presenters;
using Avalonia.Threading;

namespace Material.Dialog.Views
{
    public class TextFieldDialog : Window, IDialogWindowResult<TextFieldDialogResult>, IHasNegativeResult
    {
        public TextFieldDialogResult Result { get; set; }

        public TextFieldDialog()
        {
            Result = new TextFieldDialogResult();

            InitializeComponent();

            this.Closed += TextFieldDialog_Closed;
            this.Opened += TextFieldDialog_Opened;
        }

        private void TextFieldDialog_Closed(object sender, EventArgs e)
        {
            this.Opened -= TextFieldDialog_Opened;
            this.Closed -= TextFieldDialog_Closed;
        }

        private void TextFieldDialog_Opened(object sender, EventArgs e)
        {
            if (!(DataContext is TextFieldDialogViewModel vm))
                return;
            
            //vm.ButtonClick.RaiseCanExecute();

            var fields = this.Find<ItemsControl>("PART_Fields");

            Dispatcher.UIThread.InvokeAsync(delegate
            {
                if (fields is null)
                    return;
                        
                int index = 0;
                foreach (var item in fields.ItemContainerGenerator.Containers)
                {
                    var fieldViewModel = vm.TextFields[index];

                    if (item.ContainerControl is ContentPresenter presenter)
                    {
                        if (presenter.Child is TextBox field)
                        {
                            var classes = fieldViewModel.Classes;
                            if (classes != null)
                            {
                                foreach (var @class in classes.Split(' '))
                                {
                                    if (@class != "")
                                        field.Classes.Add(@class);
                                }
                            }
                        }
                    }

                    index++;
                }
            });
            vm.IsReady = true;
        }

        public TextFieldDialogResult GetResult() => Result;

        public void SetNegativeResult(DialogResult result) => Result.result = result.GetResult;

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
