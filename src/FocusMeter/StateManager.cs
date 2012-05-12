using System;
using System.Diagnostics;
using System.Net;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace FocusMeter
{
    public class StateManager
    {
        public TimerState CurrentState { get; private set; }

        public EmbeddableDocumentStore DocumentStore { get; set; }

        public Action<TimerState> StateChanged { get; set; }

        public bool CanShowDatabase { get; private set; }

        public StateManager(TimerState initialState)
        {
            CurrentState = initialState;
            
            DocumentStore = new EmbeddableDocumentStore
            {
                DataDirectory = "Data",
                UseEmbeddedHttpServer = true
            };

            try
            {
                DocumentStore.Initialize();
                CanShowDatabase = true;
            }
            catch (HttpListenerException)
            {
                DocumentStore = new EmbeddableDocumentStore
                {
                    DataDirectory = "Data"
                };
                DocumentStore.Initialize();
                CanShowDatabase = false;
            }
        }
        
        public void ChangeState(TimerState state)
        {
            using (var session = DocumentStore.OpenSession())
            {
                session.Store(new StateChangedEvent
                {
                    Date = DateTimeOffset.Now, 
                    OldState = CurrentState, 
                    NewState = state
                });
                session.SaveChanges();
            }

            CurrentState = state;

            if (StateChanged != null)
            {
                StateChanged(state);
            }
        }

        public void ShowDatabase()
        {
            Process.Start(DocumentStore.HttpServer.Configuration.ServerUrl);
        }

        public void ToggleDistraction()
        {
            switch (CurrentState)
            {
                case TimerState.Productive:
                    ChangeState(TimerState.Distracted);
                    break;
                case TimerState.Distracted:
                    ChangeState(TimerState.Productive);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}