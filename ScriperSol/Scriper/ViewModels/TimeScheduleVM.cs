using Microsoft.Scripting.Utils;
using ReactiveUI;
using ScriperLib.Configuration;
using ScriperLib.Enums;
using System;
using System.Collections.Generic;
using Scriper.ViewModels.Triggers;

namespace Scriper.ViewModels
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

    public class TimeScheduleVM : ViewModelBase
    {
        public event EventHandler<TriggerChangedEventArgs> OnTriggerChanged;

        public IEnumerable<string> TriggerTypes { get; } =  Enum.GetValues(typeof(ScriptTriggerType)).Select(item => item.ToString());

        private string _selectedTriggerType;
        public string SelectedTriggerType
        {
            get => _selectedTriggerType;
            set
            {
                var actual = (ScriptTriggerType)Enum.Parse(typeof(ScriptTriggerType), value);
                OnTriggerChanged?.Invoke(this, new TriggerChangedEventArgs(actual, GeTriggerVM(actual)));
                this.RaiseAndSetIfChanged(ref _selectedTriggerType, value);
            }
        }

        private readonly ITimeTriggerConfiguration _timeTriggerConfiguration;

        public TimeScheduleVM(ITimeTriggerConfiguration timeTriggerConfiguration)
        {
            _timeTriggerConfiguration = timeTriggerConfiguration;
            SelectedTriggerType = string.IsNullOrEmpty(_timeTriggerConfiguration.Name) ? 
                ScriptTriggerType.Time.ToString() : _timeTriggerConfiguration.ScriptTriggerType.ToString();
        }

        private TriggerVM GeTriggerVM(ScriptTriggerType type)
        {
            switch (type)
            {
                case ScriptTriggerType.Daily:
                    return new DailyTriggerVM(_timeTriggerConfiguration);
                case ScriptTriggerType.Logon:
                    return new LogonTriggerVM(_timeTriggerConfiguration);
                case ScriptTriggerType.Time:
                    return new TimeTriggerVM(_timeTriggerConfiguration);
                case ScriptTriggerType.Weekly:
                    return new WeeklyTriggerVM(_timeTriggerConfiguration);
                case ScriptTriggerType.Monthly:
                    return new MonthlyTriggerVM(_timeTriggerConfiguration);
            }

            return null;
        }

        public void InvokeSelection()
        {
            SelectedTriggerType = _selectedTriggerType;
        }
    }
}
