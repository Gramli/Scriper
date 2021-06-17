using Microsoft.Scripting.Utils;
using ReactiveUI;
using ScriperLib.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Scriper.ViewModels.Triggers
{
    public class DailyTriggerVM : TriggerVM
    {
        private DateTime _time;
        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                this.RaiseAndSetIfChanged(ref _time, value);
            }
        }

        private short _interval;
        public short Interval
        {
            get => _interval;
            set
            {
                _interval = value;
                this.RaiseAndSetIfChanged(ref _interval, value);
            }
        }

        public DailyTriggerVM(ITimeTriggerConfiguration configuration) 
            : base(configuration)
        {
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.Time = new DateTime(0,0,0, Time.Hour, Time.Minute, Time.Second);
            _configuration.DaysOfTheWeek = SelectedDaysOfWeek.ToList();
            return _configuration;
        }
    }
}
