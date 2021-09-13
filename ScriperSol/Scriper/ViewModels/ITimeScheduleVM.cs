using Scriper.Closing;
using ScriperLib.Configuration.TimeTrigger;
using System;
using System.Collections.ObjectModel;

namespace Scriper.ViewModels
{
    public interface ITimeScheduleVM : IClose<ITimeTriggerConfiguration>
    {
        event EventHandler<TriggerChangedEventArgs> OnTriggerChanged;
        event EventHandler OnTriggerApplied;
        ObservableCollection<ITimeTriggerConfiguration> TimeTriggerConfigurations { get; }
        void InvokeSelection();
    }
}
