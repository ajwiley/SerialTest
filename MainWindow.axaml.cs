using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

//Serial communication
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.CodeAnalysis.Emit;

namespace SerialTest {
    public class MainWindow : Window {
        #region Global Variables
        private static SerialPort _serialPort = new SerialPort();
        private static Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private readonly BoundProperties _boundProperties = new BoundProperties();
        #endregion
        
        public MainWindow() {
            InitializeComponent();
            DataContext = _boundProperties;
            
            #if DEBUG
            this.AttachDevTools();
            #endif
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }

        private void StartClick(object sender, RoutedEventArgs e) {
            _serialPort.Dispose();

            try {
                var temp = this.Find<ComboBox>("CmbParity");
                ComboBox CMB = temp;
                _serialPort = new SerialPort {
                    PortName = _boundProperties.PortName,
                    BaudRate = Int32.Parse(_boundProperties.BaudRate),
                    Parity = (Parity)Enum.Parse(typeof(Parity), CMB.SelectedItem.ToString())
                };
            }
            catch (Exception) {
                return;
            }
        }

        private void BtnKillConnection_OnClick(object? sender, RoutedEventArgs e) {
            var temp = this.Find<ComboBox>("CmbParity");
            ComboBox CMB = temp;
            ComboBoxItem CMBI = (ComboBoxItem)temp.SelectedItem;
            Console.WriteLine(CMBI.Content);
        }
    }
}