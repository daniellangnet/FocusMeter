using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using FocusMeter.Infrastructure;

namespace FocusMeter.ViewModels
{
    public class OptionsViewModel : INotifyPropertyChanged
    {
        public IEnumerable<ModifierKeys> ModifierKeys
        {
            get
            {
                return Enum.GetValues(typeof(ModifierKeys)).Cast<ModifierKeys>();
            }
        }

        public IEnumerable<Keys> ActionKeys
        {
            get
            {
                return Enum.GetValues(typeof(Keys)).Cast<Keys>();
            }
        }

        private ModifierKeys _modifierKey1;
        public ModifierKeys ModifierKey1
        {
            get { return _modifierKey1; }
            set
            {
                if (_modifierKey1 == value)
                {
                    return;
                }
                _modifierKey1 = value;
                OnPropertyChanged("ModifierKey1");
            }
        }
        
        private ModifierKeys _modifierKey2;
        public ModifierKeys ModifierKey2
        {
            get { return _modifierKey2; }
            set
            {
                if (_modifierKey2 == value)
                {
                    return;
                }
                _modifierKey2 = value;
                OnPropertyChanged("ModifierKey2");
            }
        }

        private Keys _actionKey;
        public Keys ActionKey
        {
            get { return _actionKey; }
            set
            {
                if (_actionKey == value)
                {
                    return;
                }
                _actionKey = value;
                OnPropertyChanged("ActionKey");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}