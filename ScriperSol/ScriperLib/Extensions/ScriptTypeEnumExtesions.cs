using ScriperLib.Enums;
using System;
using System.Linq;

namespace ScriperLib.Extensions
{
    public static class ScriptTypeEnumExtesions
    {
        public static ScriptType GetScriptType(this string fileExtension)
        {
            var type = typeof(ScriptType);
            var fields = type.GetFields();
            foreach(var field in fields)
            {
                var attribute = (FileExtensionAttribute)field.GetCustomAttributes(typeof(FileExtensionAttribute), false)[0];
                if(attribute.FileExtensionts.Contains(fileExtension))
                {
                    return (ScriptType)Enum.Parse(type, field.Name);
                }
            }

            throw new ArgumentException($"Unknown FileExtension: {fileExtension}");
        }

        public static string[] GetFileExtensionAttributes(this ScriptType enumValue)
        {
            var type = enumValue.GetType();
            var attribute = type.GetMember(enumValue.ToString()).First().GetCustomAttributes(typeof(FileExtensionAttribute),false);
            return ((FileExtensionAttribute)attribute[0]).FileExtensionts;
        }
    }
}
