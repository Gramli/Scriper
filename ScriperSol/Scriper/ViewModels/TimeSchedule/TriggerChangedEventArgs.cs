using Scriper.ViewModels.TimeSchedule.Triggers;
using ScriperLib.Enums;
using System;

namespace Scriper.ViewModels.TimeSchedule
{
    public class TriggerChangedEventArgs : EventArgs
    {
        public ScriptTriggerType ScriptTriggerType { get; }

        public TriggerVM TriggerVM { get; }

        public TriggerChangedEventArgs(ScriptTriggerType scriptTriggerType, TriggerVM triggerVM)
        {
            ScriptTriggerType = scriptTriggerType;
            TriggerVM = triggerVM;
        }
    }
}
