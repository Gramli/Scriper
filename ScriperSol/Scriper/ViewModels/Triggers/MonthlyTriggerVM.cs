using Microsoft.Scripting.Utils;
using Microsoft.Win32.TaskScheduler;
using ScriperLib.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Scriper.ViewModels.Triggers
{
    public class MonthlyTriggerVM : TriggerVM
    {
        public IEnumerable<int> DaysOfMonth { get; } = new int[31].Select((item, index) => index);
        public ObservableCollection<int> SelectedDaysOfMonth { get; } = new ObservableCollection<int>();

        public IEnumerable<string> MonthsOfYear { get; } = Enum.GetValues(typeof(MonthsOfTheYear)).Select(item => item.ToString());
        public ObservableCollection<string> SelectedMonthsOfYear { get; } = new ObservableCollection<string>();

        public MonthlyTriggerVM(ITimeTriggerConfiguration configuration) 
            : base(configuration)
        {

        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
