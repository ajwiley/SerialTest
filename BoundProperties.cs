using System;
using System.ComponentModel;
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
                OnPropertyChanged(nameof(Response));
            }
        }
        
        private string _BaudRate;
        public string BaudRate {
            get => _BaudRate;
            set {
                _BaudRate = value;
                OnPropertyChanged(nameof(Response));
            }
        }

        public BoundProperties() {
            _Response = "";
            _PortName = "COM1";
            _BaudRate = "9600";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}