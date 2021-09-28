using ScriperLib.Enums;
using System;
using System.Linq;

namespace ScriperLib.Extensions
{
    public static class ScriptTypeEnumExtesions
    {
        public static ScriptType GetScriptType(this string fileExtension)
        {
            if(fileExtension.TryGetScriptType(out var scriptType))
            {
                return scriptType.Value;
            }

            throw new ArgumentException($"Unknown FileExtension: {fileExtension}");
        }

        public static bool TryGetScriptType(this string fileExtension, out ScriptType? scriptType)
        {
            var type = typeof(ScriptType);
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                var attribute = (FileExtensionAttribute)field.GetCustomAttributes(typeof(FileExtensionAttribute), false).FirstOrDefault();
                if (attribute != null && attribute.FileExtensionts.Contains(fileExtension))
                {
                    scriptType = (ScriptType)Enum.Parse(type, field.Name);
                    return true;
                }
            }

            scriptType = null;
            return false;
        }

        public static string[] GetFileExtensionAttributes(this ScriptType enumValue)
        {
            var type = enumValue.GetType();
            var attributes = type.GetMember(enumValue.ToString()).First().GetCustomAttributes(typeof(FileExtensionAttribute),false);
            return ((FileExtensionAttribute)attributes.First()).FileExtensionts;
        }
    }
}
