using System;

namespace FocusMeter
{
    public class StateChangedEvent
    {
        public DateTimeOffset Date { get; set; }
        public TimerState OldState { get; set; }
        public TimerState NewState { get; set; }
    }
}