﻿using Microsoft.Scripting.Utils;
using ReactiveUI;
using Scriper.Closing;
using Scriper.ViewModels.TimeSchedule.Triggers;
using ScriperLib.Configuration.TimeTrigger;
using ScriperLib.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace Scriper.ViewModels.TimeSchedule
{
    public class TimeScheduleVM : ViewModelBase, ITimeScheduleVM
    {
        public event EventHandler<TriggerChangedEventArgs> OnTriggerChanged;
        public event EventHandler OnTriggerApplied;
        public event CloseEventHandler<ITimeTriggerConfiguration> Close;

        public IEnumerable<string> TriggerTypes { get; } =  Enum.GetValues(typeof(ScriptTriggerType)).Select(item => item.ToString());

        private string _triggerName;
        public string TriggerName
        {
            get => _triggerName;
            set => this.RaiseAndSetIfChanged(ref _triggerName, value);
        }

        private string _selectedTriggerType;
        public string SelectedTriggerType
        {
            get => _selectedTriggerType;
            set
            {
                if (value is null)
                {
                    return;
                }
                var actual = (ScriptTriggerType)Enum.Parse(typeof(ScriptTriggerType), value);
                _actualTriggerVm = GeTriggerVM(actual);
                OnTriggerChanged?.Invoke(this, new TriggerChangedEventArgs(actual, _actualTriggerVm));
                this.RaiseAndSetIfChanged(ref _selectedTriggerType, value);
            }
        }

        private bool _editingVisisble;

        public bool EditingVisisble
        {
            get => _editingVisisble;
            set => this.RaiseAndSetIfChanged(ref _editingVisisble, value);
        }

        public ObservableCollection<ITimeTriggerConfiguration> TimeTriggerConfigurations { get; }

        private ITimeTriggerConfiguration _selectedTimeTriggerConfiguration;
        public ITimeTriggerConfiguration SelectedTimeTriggerConfiguration
        {
            get => _selectedTimeTriggerConfiguration;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTimeTriggerConfiguration, value);
                EditingVisisble = value != null;
                SelectedTriggerType = value?.ScriptTriggerType.ToString();
                TriggerName = value?.Name;
            }
        }

        public ReactiveCommand<Unit, Unit> CreateNewTriggerConfigurationCmd { get; }
        public ReactiveCommand<string, Unit> DeleteNonSelectedTriggerConfigurationCmd { get; }
        public ReactiveCommand<Unit, Unit> ApplyChangesCmd { get; }
        public ReactiveCommand<Unit, Unit> DeleteCmd { get; }

        private TriggerVM _actualTriggerVm;

        private readonly ITimeTriggerConfigurationFactory _timeTriggerConfigurationFactory;

        public TimeScheduleVM(ITimeTriggerConfigurationFactory timeTriggerConfigurationFactory,
            ICollection<ITimeTriggerConfiguration> timeTriggerConfigurations)
        {
            TimeTriggerConfigurations = new ObservableCollection<ITimeTriggerConfiguration>(timeTriggerConfigurations);
            SelectedTimeTriggerConfiguration = TimeTriggerConfigurations.FirstOrDefault();
            EditingVisisble = SelectedTimeTriggerConfiguration != null;
            CreateNewTriggerConfigurationCmd = ReactiveCommand.Create(CreateNewTriggerConfiguration);
            DeleteNonSelectedTriggerConfigurationCmd = ReactiveCommand.Create<string>(DeleteNonSelectedTriggerConfiguration);
            ApplyChangesCmd = ReactiveCommand.Create(ApplyChanges);
            DeleteCmd = ReactiveCommand.Create(Delete);
            _timeTriggerConfigurationFactory = timeTriggerConfigurationFactory;
        }

        private TriggerVM GeTriggerVM(ScriptTriggerType type)
        {
            var newTimeTriggerConfiguration = _timeTriggerConfigurationFactory.Create();
            CopySelectedTimeTriggerConfiguration(newTimeTriggerConfiguration);

            switch (type)
            {
                case ScriptTriggerType.Daily:
                    return new DailyTriggerVM(newTimeTriggerConfiguration);
                case ScriptTriggerType.Logon:
                    return new LogonTriggerVM(newTimeTriggerConfiguration);
                case ScriptTriggerType.Time:
                    return new TimeTriggerVM(newTimeTriggerConfiguration);
                case ScriptTriggerType.Weekly:
                    return new WeeklyTriggerVM(newTimeTriggerConfiguration);
                case ScriptTriggerType.Monthly:
                    return new MonthlyTriggerVM(newTimeTriggerConfiguration);
            }

            return null;
        }

        private void CopySelectedTimeTriggerConfiguration(ITimeTriggerConfiguration newTimeTriggerConfiguration)
        {
            newTimeTriggerConfiguration.Name = SelectedTimeTriggerConfiguration.Name;
            newTimeTriggerConfiguration.ScriptTriggerType = SelectedTimeTriggerConfiguration.ScriptTriggerType;
            newTimeTriggerConfiguration.Time = SelectedTimeTriggerConfiguration.Time;
            newTimeTriggerConfiguration.DelayInSeconds = SelectedTimeTriggerConfiguration.DelayInSeconds;
            newTimeTriggerConfiguration.Interval = SelectedTimeTriggerConfiguration.Interval;
            newTimeTriggerConfiguration.DaysOfTheWeek = SelectedTimeTriggerConfiguration.DaysOfTheWeek;
            newTimeTriggerConfiguration.DaysOfMonth = SelectedTimeTriggerConfiguration.DaysOfMonth;
            newTimeTriggerConfiguration.MonthsOfYear = SelectedTimeTriggerConfiguration.MonthsOfYear;
        }

        private void ApplyChanges()
        {
            var editedConfiguration = _actualTriggerVm.GetTriggerConfiguration();
            editedConfiguration.Name = TriggerName;
            editedConfiguration.ScriptTriggerType = (ScriptTriggerType)Enum.Parse(typeof(ScriptTriggerType), SelectedTriggerType);
            TimeTriggerConfigurations.Remove(TimeTriggerConfigurations.First(item => item.Name == SelectedTimeTriggerConfiguration.Name));
            TimeTriggerConfigurations.Add(editedConfiguration);
            this.SelectedTimeTriggerConfiguration = null;
            OnTriggerApplied?.Invoke(this, EventArgs.Empty);
        }

        private void Delete()
        {
            var editedConfiguration = _actualTriggerVm.GetTriggerConfiguration();
            TimeTriggerConfigurations.Remove(TimeTriggerConfigurations.First(item => item.Name == editedConfiguration.Name));
            this.SelectedTimeTriggerConfiguration = null;
            OnTriggerApplied?.Invoke(this, EventArgs.Empty);
        }

        private void CreateNewTriggerConfiguration()
        {
            var newTimeTriggerConfiguration = _timeTriggerConfigurationFactory.Create();
            newTimeTriggerConfiguration.Name = GenerateName("New Trigger");
            newTimeTriggerConfiguration.ScriptTriggerType = ScriptTriggerType.Time;
            TimeTriggerConfigurations.Add(newTimeTriggerConfiguration);
            SelectedTimeTriggerConfiguration = newTimeTriggerConfiguration;
        }

        private string GenerateName(string name)
        {
            var index = 0;
            var editableName = name;
            while (TimeTriggerConfigurations.Any(item=> item.Name == editableName))
            {
                editableName = $"{name}{index}";
                index++;
            }

            return editableName;
        }

        private void DeleteNonSelectedTriggerConfiguration(string name)
        {
            if (SelectedTimeTriggerConfiguration.Name == name)
            {
                Delete();
                return;
            }

            TimeTriggerConfigurations.Remove(TimeTriggerConfigurations.Single(item => item.Name == name));
        }

        public void InvokeSelection()
        {
            SelectedTriggerType = _selectedTriggerType;
            TriggerName = _triggerName;
        }
    }
}
