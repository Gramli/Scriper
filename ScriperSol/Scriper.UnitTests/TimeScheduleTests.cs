using NUnit.Framework;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.ScriptScheduler;
using System;
using Microsoft.Win32.TaskScheduler;

namespace Scriper.UnitTests
{
    public class TimeScheduleTests : TestsBase
    {
        [Test]
        public void AddAndRemove()
        {
            var scriperLibContainer = new ScriperLibContainer(filePath);
            var scriptConfig = scriperLibContainer.GetInstance<IScriptConfiguration>();
            scriptConfig.Name = "myScript";
            var timeScheduleConfig = scriperLibContainer.GetInstance<ITimeTriggerConfiguration>();
            timeScheduleConfig.Time = DateTime.Now.AddMinutes(10);
            timeScheduleConfig.ScriptTriggerType = ScriperLib.Enums.ScriptTriggerType.Time;

            scriptConfig.TimeScheduleConfigurations.Add(timeScheduleConfig);

            var scriptScheduleManager = scriperLibContainer.GetInstance<IScriptSchedulerManager>();
            scriptScheduleManager.Add("something","something", scriptConfig);

            TaskService.Instance.RootFolder.DeleteTask(scriptConfig.Name);
        }
    }
}
