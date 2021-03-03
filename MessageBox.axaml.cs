using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;
using Avalonia.Interactivity;

namespace SerialTest {
    public class MessageBox : Window {
        public enum MessageBoxButtons {
            Ok,
            OkCancel,
            YesNo,
            YesNoCancel
        }

        public enum MessageBoxResult {
            Ok,
            Cancel,
            Yes,
            No
        }

        public MessageBox() {
            AvaloniaXamlLoader.Load(this);
        }

        public static Task<MessageBoxResult> Show(Window parent, string text, string title, MessageBoxButtons buttons) {
            var Msgbox = new MessageBox() {
                Title = title
            };
            
            Msgbox.FindControl<TextBlock>("Text").Text = "\n    " + text + "    \n";
            var buttonPanel = Msgbox.FindControl<StackPanel>("Buttons");

            var res = MessageBoxResult.Ok;

            void AddButton(string caption, MessageBoxResult r, bool def = false) {
                var btn = new Button {Content = caption};
                
                btn.Click += (_, __) => {
                    res = r;
                    Msgbox.Close();
                };
                
                buttonPanel.Children.Add(btn);
                
                if (def) {
                    res = r;
                }
            }

            if (buttons == MessageBoxButtons.Ok || buttons == MessageBoxButtons.OkCancel) {
                AddButton("Ok", MessageBoxResult.Ok, true);
            }

            if (buttons == MessageBoxButtons.YesNo || buttons == MessageBoxButtons.YesNoCancel) {
                AddButton("Yes", MessageBoxResult.Yes);
                AddButton("No", MessageBoxResult.No, true);
            }

            if (buttons == MessageBoxButtons.OkCancel || buttons == MessageBoxButtons.YesNoCancel) {
                AddButton("Cancel", MessageBoxResult.Cancel, true);
            }

            var tcs = new TaskCompletionSource<MessageBoxResult>();
            Msgbox.Closed += delegate { tcs.TrySetResult(res); };
            
            if (parent != null) {
                Msgbox.ShowDialog(parent);
            }
            else {
                Msgbox.Show();
            }
            
            return tcs.Task;
        }
    }
}