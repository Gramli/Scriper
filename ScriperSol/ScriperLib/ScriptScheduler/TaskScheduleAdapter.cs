using Microsoft.Scripting.Utils;
using Microsoft.Win32.TaskScheduler;
using ScriperLib.Configuration;
using ScriperLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriperLib.ScriptScheduler
{
    public class TaskScheduleAdapter : ITaskScheduleAdapter
    {
        //private running
        //run in some hidden mode, so pass arguments from Scriper
        public void Delete(string scriptName)
        {
            TaskService.Instance.RootFolder.DeleteTask(scriptName);
        }

        public void Register(string runnerExe, string arguments, IScriptConfiguration scriptConfiguration)
        {
            var taskDefinition = TaskService.Instance.NewTask();
            taskDefinition.RegistrationInfo.Description = scriptConfiguration.Description;

            var triggers = CreateTriggers(scriptConfiguration);
            taskDefinition.Triggers.AddRange(triggers);

            var exeAction = new ExecAction(runnerExe, arguments, null);
            taskDefinition.Actions.Add(exeAction);

            TaskService.Instance.RootFolder.RegisterTaskDefinition(scriptConfiguration.Name, taskDefinition, TaskCreation.CreateOrUpdate, Environment.UserName);
        }

        private IEnumerable<Trigger> CreateTriggers(IScriptConfiguration scriptConfiguration)
        {
            var timeScheduleConfigs = scriptConfiguration.TimeScheduleConfigurations;
            return timeScheduleConfigs.Select(timeScheduleConfig =>
            {
                var taskTriggerType = timeScheduleConfig.ScriptTriggerType.Map();
                var trigger = Trigger.CreateTrigger(taskTriggerType);
                trigger.StartBoundary = timeScheduleConfig.Time;

                switch (trigger)
                {
                    case DailyTrigger dailyTrigger:
                        dailyTrigger.DaysInterval = timeScheduleConfig.Interval;
                        break;
                    case TimeTrigger timeTrigger:
                        timeTrigger.EndBoundary = timeScheduleConfig.Time.AddDays(1);
                        break;
                    case WeeklyTrigger weeklyTrigger:
                        weeklyTrigger.WeeksInterval = timeScheduleConfig.Interval;
                        var daysOfTheWeek =
                            timeScheduleConfig.DaysOfTheWeek.Select(day => (DaysOfTheWeek) Enum.Parse(typeof(DaysOfTheWeek), day));
                        weeklyTrigger.DaysOfWeek = daysOfTheWeek.Aggregate((x,y) => x | y);
                        break;
                    case LogonTrigger logonTrigger:
                        logonTrigger.Delay = TimeSpan.FromSeconds(timeScheduleConfig.DelayInSeconds);
                        break;
                    default:
                        break;
                }

                return trigger;
            });
        }
    }
}
