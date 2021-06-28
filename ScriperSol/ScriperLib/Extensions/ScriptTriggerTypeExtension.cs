using System;
using Microsoft.Win32.TaskScheduler;
using ScriperLib.Enums;
using ScriperLib.Exceptions;

namespace ScriperLib.Extensions
{
    public static class ScriptTriggerTypeExtension
    {
        public static TaskTriggerType Map(this ScriptTriggerType scriptTriggerType)
        {
            var valuesTriggerTypes = Enum.GetValues(typeof(TaskTriggerType));

            foreach (var valuesTriggerType in valuesTriggerTypes)
            {
                if (valuesTriggerType.ToString() == scriptTriggerType.ToString())
                {
                    return (TaskTriggerType)valuesTriggerType;
                }
            }

            throw new ScriptException($"Can't map {scriptTriggerType} to TaskTriggerType");
        }
    }
}
