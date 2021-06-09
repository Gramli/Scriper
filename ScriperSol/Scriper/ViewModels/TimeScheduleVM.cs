using Microsoft.Scripting.Utils;
using ReactiveUI;
using ScriperLib.Configuration;
using ScriperLib.Enums;
using System;
using System.Collections.Generic;

namespace Scriper.ViewModels
{
    public class TriggerChangedEventArgs : EventArgs
    {
        public ScriptTriggerType ScriptTriggerType { get; }

        public TriggerChangedEventArgs(ScriptTriggerType scriptTriggerType)
        {
            ScriptTriggerType = scriptTriggerType;
        }
    }

    public class TimeScheduleVM : ViewModelBase
    {
        public event EventHandler<TriggerChangedEventArgs> OnTriggerChanged;

        public IEnumerable<string> TriggerTypes { get; } =  Enum.GetValues(typeof(ScriptTriggerType)).Select(item => item.ToString());

        private string _selectedTriggerType = ScriptTriggerType.Time.ToString();
        public string SelectedTriggerType
        {
            get => _selectedTriggerType;
            set
            {
                var actual = (ScriptTriggerType)Enum.Parse(typeof(ScriptTriggerType), value);
                OnTriggerChanged?.Invoke(this, new TriggerChangedEventArgs(actual));
                this.RaiseAndSetIfChanged(ref _selectedTriggerType, value);
            }
        }

        private readonly ITimeTriggerConfiguration _timeTriggerConfiguration;

        public TimeScheduleVM(ITimeTriggerConfiguration timeTriggerConfiguration)
        {
            _timeTriggerConfiguration = timeTriggerConfiguration;
        }
    }
}
