using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace SerialTest {
    public class BoundProperties : INotifyPropertyChanged {
        private string _Response;
        [NotNull]
        public string Response {
            get => _Response;
            set {
                _Response = value;
                OnPropertyChanged(nameof(Response));
            }
        }

        private string _PortName;
        public string PortName {
            get => _PortName;
            set {
                _PortName = value;
                OnPropertyChanged(nameof(PortName));
            }
        }
        
        private string _Command;
        public string Command {
            get => _Command;
            set {
                _Command = value;
                OnPropertyChanged(nameof(Command));
            }
        }
        
        private string _BaudRate;
        public string BaudRate {
            get => _BaudRate;
            set {
                _BaudRate = value;
                OnPropertyChanged(nameof(BaudRate));
            }
        }

        private bool _Both;
        public bool Both {
            get => _Both;
            set {
                _Both = value;
                OnPropertyChanged(nameof(Both));
            }
        }
        private bool _CR;
        public bool CR {
            get => _CR;
            set {
                _CR = value;
                OnPropertyChanged(nameof(CR));
            }
        }

        private bool _LF;
        public bool LF {
            get => _LF;
            set {
                _LF = value;
                OnPropertyChanged(nameof(LF));
            }
        }

        private bool _Reset;
        public bool Reset {
            get => _Reset;
            set {
                _Reset = value;
                OnPropertyChanged(nameof(Reset));
            }
        }

        public BoundProperties() {
            _Response = "";
            _PortName = "COM1";
            _BaudRate = "9600";
            _Command = "";
            Both = true;
            Reset = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}