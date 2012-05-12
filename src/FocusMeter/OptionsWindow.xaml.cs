using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FocusMeter.Helper;
using FocusMeter.Infrastructure;
using FocusMeter.Model;
using FocusMeter.ViewModels;

namespace FocusMeter
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private Configuration _configuration;
        public Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    return null;
                }

                var optionsViewModel = (OptionsViewModel) DataContext;

                _configuration.ShortcutKey = optionsViewModel.ActionKey;
                _configuration.ShortcutModifierKeys = optionsViewModel.ModifierKey1 | optionsViewModel.ModifierKey2;

                return _configuration;
            }
            set
            {
                _configuration = value;

                var optionsViewModel = (OptionsViewModel) DataContext;

                var modifierKeys = _configuration.ShortcutModifierKeys.GetIndividualValues<ModifierKeys>().ToArray();
                optionsViewModel.ModifierKey1 = modifierKeys.Length > 0 ? modifierKeys[0] : ModifierKeys.None;
                optionsViewModel.ModifierKey2 = modifierKeys.Length > 1 ? modifierKeys[1] : ModifierKeys.None;

                optionsViewModel.ActionKey = _configuration.ShortcutKey;
            }
        }

        public OptionsWindow()
        {
            InitializeComponent();

            DataContext = new OptionsViewModel();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
