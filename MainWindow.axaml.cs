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
        
        //These are the items in the combo boxes
        private readonly string[] Parity = {"None", "Odd", "Even", "Mark", "Space"};
        private readonly string[] Handshake = {"None", "XOnXOff", "RequestToSend", "RequestToSendXOnXOff"};
        private readonly string[] RtsEnable = { "true", "false" };

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
            _serialPort.Dispose(); //Dispose of any serial connections that the user has made before.

            try {
                //Get the items
                var temp = this.Find<ComboBox>("CmbParity");
                ComboBox ParityCMB = temp;
                temp = this.Find<ComboBox>("CmbDataBits");
                ComboBox DataCMB = temp;
                temp = this.Find<ComboBox>("CmbHandshake");
                ComboBox HandCMB = temp;
                temp = this.Find<ComboBox>("CmbRtsEnable");
                ComboBox RtsEnableCMB = temp;
                
                //Create a serial Port with the users wanted information.
                _serialPort = new SerialPort {
                    PortName = _boundProperties.PortName,
                    BaudRate = Int32.Parse(_boundProperties.BaudRate),
                    Parity = (Parity)Enum.Parse(typeof(Parity), Parity[ParityCMB.SelectedIndex]),
                    DataBits = int.Parse(DataCMB.SelectedIndex.ToString()) + 5,
                    Handshake = (Handshake)Enum.Parse(typeof(Handshake), Handshake[HandCMB.SelectedIndex]),
                    RtsEnable = RtsEnable[RtsEnableCMB.SelectedIndex] == "true",
                    ReadTimeout = 2000,
                    WriteTimeout = 2000
                };
            }
            catch (Exception ex) {
                MessageBox.Show(this, "One or more provided values are bad:\r\n" + ex.Message, "Command Error",
                    MessageBox.MessageBoxButtons.Ok);
                return;
            }

            try {
                //When we get data update the text box
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(NewSerialData);

                _serialPort.Open(); //Open the connection
            }
            catch (Exception ex) {
                //Show the error message.
                MessageBox.Show(this, "Failed to open serial port:\r\n" + ex.Message, "Command Error",
                    MessageBox.MessageBoxButtons.Ok);
            }
        }

        private void NewSerialData(object sender, SerialDataReceivedEventArgs e) {
            Thread.Sleep(50);
            _boundProperties.Response = _serialPort.ReadExisting().Trim();
        }

        private void BtnKillConnection_OnClick(object? sender, RoutedEventArgs e) {
        }
    }
}