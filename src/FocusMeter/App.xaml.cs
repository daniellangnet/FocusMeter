using System;
using System.Windows.Forms;
using FocusMeter;
using Application = System.Windows.Application;

namespace FocusMeter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private KeyboardHook hook;

        public static StateManager StateManager { get; set; }
        
        public App()
        {
            DocumentStoreContainer.Initialize();

            StateManager = new StateManager(DocumentStoreContainer.DocumentStore, TimerState.NotWorking);

            RegisterKeyboardShortcuts();
        }

        private void RegisterKeyboardShortcuts()
        {
            hook = new KeyboardHook();

            hook.RegisterHotKey(ModifierKeys.Control, Keys.Enter);
            hook.KeyPressed += (sender, args) => StateManager.ToggleDistraction();
        }
    }
}
