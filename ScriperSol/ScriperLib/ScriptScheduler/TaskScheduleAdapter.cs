using Microsoft.Win32.TaskScheduler;
using ScriperLib.Configuration;
using System;

namespace ScriperLib.ScriptScheduler
{
    public class TaskScheduleAdapter : ITaskScheduleAdapter
    {

        //private running
        //run in some hidden mode, so pass arguments from Scriper
        public void Delete(string scriptName)
        {
            throw new NotImplementedException();
        }

        public IScriptConfiguration Get(string scriptName)
        {
            throw new NotImplementedException();
        }

        public void Register(IScriptConfiguration scriptConfiguration)
        {
            var taskDefinition = TaskService.Instance.NewTask();
            taskDefinition.RegistrationInfo.Description = scriptConfiguration.Description;


            //create trigger

            //create action

            TaskService.Instance.RootFolder.RegisterTaskDefinition(scriptConfiguration.Name, taskDefinition, TaskCreation.CreateOrUpdate, Environment.UserName);
        }
    }
}
