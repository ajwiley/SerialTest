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

        public BoundProperties() {
            _Response = "";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}