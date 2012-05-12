using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using FocusMeter.Infrastructure;
using FocusMeter.Model;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace FocusMeter
{
    /// <summary>
    /// Interaction logic for HiddenWindow.xaml
    /// </summary>
    public partial class HiddenWindow : Window
    {
        private Icon notWorkingIcon;
        private Icon productiveIcon;
        private Icon distractedIcon;

        public HiddenWindow()
        {
            InitializeComponent();
            
            LoadIcons();

            if (!DocumentStoreContainer.CanShowDatabase)
            {
                MessageBox.Show("You need to run FocusMeter as Administrator if you want to show the embedded RavenDB database.\r\n\r\n" +
                                "(This is because RavenDB Management Studio is hosted by an embedded http server that needs to listen to an http port.)",
                                "Administrator privileges", MessageBoxButtons.OK, MessageBoxIcon.Information);
                menuShowDatabase.IsEnabled = false;
            }

            App.StateManager.StateChanged += StateChanged;
            StateChanged(TimerState.NotWorking);
        }

        private void LoadIcons()
        {
            notWorkingIcon = LoadIconFromResource("pack://application:,,,/FocusMeter;component/Assets/Icons/notworking.ico");
            productiveIcon = LoadIconFromResource("pack://application:,,,/FocusMeter;component/Assets/Icons/productive.ico");
            distractedIcon = LoadIconFromResource("pack://application:,,,/FocusMeter;component/Assets/Icons/distracted.ico");
        }

        private static Icon LoadIconFromResource(string uri)
        {
            var streamResourceInfo = Application.GetResourceStream(new Uri(uri));
            if (streamResourceInfo == null || streamResourceInfo.Stream == null)
            {
                throw new Exception("Icon resource not found");
            }

            return new Icon(streamResourceInfo.Stream);
        }

        private new void StateChanged(TimerState timerState)
        {
            switch (timerState)
            {
                case TimerState.NotWorking:
                    menuStart.Visibility = Visibility.Visible;
                    menuStop.Visibility = Visibility.Collapsed;
                    menuTrackDistraction.Visibility = Visibility.Collapsed;
                    menuContinueProductive.Visibility = Visibility.Collapsed;
                    NotifyIcon.Icon = notWorkingIcon;
                    break;
                case TimerState.Productive:
                    menuStart.Visibility = Visibility.Collapsed;
                    menuStop.Visibility = Visibility.Visible;
                    menuTrackDistraction.Visibility = Visibility.Visible;
                    menuContinueProductive.Visibility = Visibility.Collapsed;
                    NotifyIcon.Icon = productiveIcon;
                    break;
                case TimerState.Distracted:
                    menuStart.Visibility = Visibility.Collapsed;
                    menuStop.Visibility = Visibility.Visible;
                    menuTrackDistraction.Visibility = Visibility.Collapsed;
                    menuContinueProductive.Visibility = Visibility.Visible;
                    NotifyIcon.Icon = distractedIcon;
                    break;
            }
        }
        
        private void menuShowDatabase_Click(object sender, RoutedEventArgs e)
        {
            DocumentStoreContainer.ShowDatabase();
        }

        private void menuTrackDistraction_Click(object sender, RoutedEventArgs e)
        {
            App.StateManager.ChangeState(TimerState.Distracted);
        }
        
        private void menuContinueProductive_Click(object sender, RoutedEventArgs e)
        {
            App.StateManager.ChangeState(TimerState.Productive);
        }

        private void menuStart_Click(object sender, RoutedEventArgs e)
        {
            App.StateManager.ChangeState(TimerState.Productive);
        }

        private void menuStop_Click(object sender, RoutedEventArgs e)
        {
            App.StateManager.ChangeState(TimerState.NotWorking);
        }

        private void menuExitProgram_Click(object sender, RoutedEventArgs e)
        {
            // manually disposing is important here so that the icon immediately disappers from systray
            NotifyIcon.Dispose();
            Close();
        }

        private void menuOptions_Click(object sender, RoutedEventArgs e)
        {
            var optionsWindow = new OptionsWindow
            {
                Configuration = new Configuration
                {
                    ShortcutKey = Keys.Enter,
                    ShortcutModifierKeys = ModifierKeys.Control | ModifierKeys.Alt
                }
            };

            if (optionsWindow.ShowDialog() == true)
            {
                
            }
        }
    }
}
