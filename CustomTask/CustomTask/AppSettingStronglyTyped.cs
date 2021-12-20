using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomTask
{
    public class AppSettingStronglyTyped : Task
    {
        [Required]
        public string SettingClassName { get; set; }

        [Required]
        public string SettingNamespaceName { get; set; }

        [Required]
        public ITaskItem[] SettingFiles { get; set; }

        [Output]
        public string ClassNameFile { get; set; }

        public override bool Execute()
        {
            var settings = ReadProjectSettingFiles();
            return CreateSettingClass(settings);
        }

        private IDictionary<string, object> ReadProjectSettingFiles()
        {
            foreach (var item in SettingFiles)
            {
                var filename = item.GetMetadata("Filename");
                Log.LogMessage(MessageImportance.High, filename);
                // so far only reading the the files names, and let us know which files are identified
                // Next, we neeed to move the properties to a dictionary. 
                // Input, text files with name:type:value
                //return the dictionary
            }
            return new Dictionary<string, object>
            {
                ["Prop1"] = "Hello",
                ["Prop2"] = 2
            };
        }

        private bool CreateSettingClass(IDictionary<string, object> settings)
        {
            try
            {
                ClassNameFile = $"{SettingClassName}.generated.cs";
                StringBuilder settingsClass = new StringBuilder(1024);
                // open namespace  
                settingsClass.Append($"namespace {SettingNamespaceName} {{ \r\n");
                // open class
                settingsClass.Append($"public class {SettingClassName} {{  \r\n");
                //For each element in the dictionary create a static property
                foreach (var keyValuePair in settings)
                {
                    //TODO take care the type
                    settingsClass.Append($"public const string  {keyValuePair.Key} =  \"{keyValuePair.Value}\";\r\n");
                }
                // close namespace and class
                settingsClass.Append(" } \r\n} \r\n");
                File.WriteAllText(ClassNameFile, settingsClass.ToString());

            }
            catch (Exception Ex)
            {
                Log.LogError(Ex.ToString());
                return false;
            }
            return true;
        }
    }
}
