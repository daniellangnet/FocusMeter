using System;
using System.Diagnostics;
using System.Net;
using FocusMeter.Model;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace FocusMeter
{
    public class StateManager
    {
        public TimerState CurrentState { get; private set; }

        public Action<TimerState> StateChanged { get; set; }

        public IDocumentStore DocumentStore { get; set; }

        public StateManager(IDocumentStore documentStore, TimerState initialState)
        {
            DocumentStore = documentStore;
            CurrentState = initialState;
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