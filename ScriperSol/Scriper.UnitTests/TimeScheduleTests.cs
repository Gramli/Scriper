using NUnit.Framework;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.ScriptScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scriper.UnitTests
{
    public class TimeScheduleTests : TestsBase
    {
        [Test]
        public void Add()
        {
            var scriperLibContainer = new ScriperLibContainer(filePath);
            var scriptConfig = scriperLibContainer.GetInstance<IScriptConfiguration>();
            var timeScheduleConfig = scriperLibContainer.GetInstance<ITimeTriggerConfiguration>();

            timeScheduleConfig.Time = DateTime.Now.AddMinutes(10);
            timeScheduleConfig.ScriptTriggerType = ScriperLib.Enums.ScriptTriggerType.Time;

            scriptConfig.TimeScheduleConfigurations.Add(timeScheduleConfig);

            var scriptScheduleManager = scriperLibContainer.GetInstance<IScriptSchedulerManager>();
            scriptScheduleManager.Add("","", scriptConfig);

            //Check Added

            //Remove at end
        }

        [Test]
        public void Get()
        {
            var scriperLibContainer = new ScriperLibContainer(filePath);
            var scriptConfig = scriperLibContainer.GetInstance<IScriptConfiguration>();
            var timeScheduleConfig = scriperLibContainer.GetInstance<ITimeTriggerConfiguration>();

            timeScheduleConfig.Time = DateTime.Now.AddMinutes(10);
            timeScheduleConfig.ScriptTriggerType = ScriperLib.Enums.ScriptTriggerType.Time;

            scriptConfig.TimeScheduleConfigurations.Add(timeScheduleConfig);

            var scriptScheduleManager = scriperLibContainer.GetInstance<IScriptSchedulerManager>();
            scriptScheduleManager.Add("", "", scriptConfig);


            //var timeScheduleConfigLoaded = scriptScheduleManager.Get(scriptConfig.Name);

            //Assert.Equals(timeScheduleConfig.Time, timeScheduleConfigLoaded.Time);
            //Assert.Equals(timeScheduleConfig.ScriptTriggerType, timeScheduleConfigLoaded.ScriptTriggerType);

            //Remove at end
        }

        [Test]
        public void Remove()
        {
            var scriperLibContainer = new ScriperLibContainer(filePath);
            var scriptConfig = scriperLibContainer.GetInstance<IScriptConfiguration>();
            var timeScheduleConfig = scriperLibContainer.GetInstance<ITimeTriggerConfiguration>();

            timeScheduleConfig.Time = DateTime.Now.AddMinutes(10);
            timeScheduleConfig.ScriptTriggerType = ScriperLib.Enums.ScriptTriggerType.Time;

            scriptConfig.TimeScheduleConfigurations.Add(timeScheduleConfig);

            var scriptScheduleManager = scriperLibContainer.GetInstance<IScriptSchedulerManager>();
            scriptScheduleManager.Add("", "", scriptConfig);

            //Check Added

            scriptScheduleManager.Remove(scriptConfig.Name);

            //Check Removed
            //Remove at end
        }
    }
}
