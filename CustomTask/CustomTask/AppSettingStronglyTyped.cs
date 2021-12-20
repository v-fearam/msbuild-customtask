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
            var (success, settings) = ReadProjectSettingFiles();
            return CreateSettingClass(settings);
        }

        private (bool, IDictionary<string, object>) ReadProjectSettingFiles()
        {
            var values = new Dictionary<string, object>();
            foreach (var item in SettingFiles)
            {
                var identity = item.GetMetadata("Identity");
                 foreach (string line in File.ReadLines(identity))
                {
                    var lineParse = line.Split(':');
                    if (lineParse.Length != 3)
                    {
                        return (false, null);
                    }
                    var value = GetValue(lineParse[1], lineParse[2]);
                    if (!value.Item1) {
                        return (value.Item1, null);
                    }

                    values[lineParse[0]] = value.Item2;
                }
            }
            return (true, values);
        }

        private (bool, object) GetValue(string type, string value)
        {
            try
            {
                // So far only srting and int are supported values.
                if ("string".Equals(type))
                {
                    return (true, value);
                }
                if ("int".Equals(type))
                {
                    return (true, int.Parse(value));
                }
                return (false, null);
            }
            catch
            {
                return (false, null);
            }
        }

        private bool CreateSettingClass(IDictionary<string, object> settings)
        {
            try
            {
                ClassNameFile = $"{SettingClassName}.generated.cs";
                File.Delete(ClassNameFile);
                StringBuilder settingsClass = new StringBuilder(1024);
                // open namespace  
                settingsClass.Append($@" using System; 
 namespace {SettingNamespaceName} {{ 

  public class {SettingClassName} {{
");
                //For each element in the dictionary create a static property
                foreach (var keyValuePair in settings)
                {
                    string typeName = keyValuePair.Value.GetType().Name;
                    settingsClass.Append($"    public const {typeName}  {keyValuePair.Key} =  {("String".Equals(typeName)? "\"":"")}{keyValuePair.Value}{("String".Equals(typeName) ? "\"" : "")};\r\n");
                }
                // close namespace and class
                settingsClass.Append(@"  }

}");
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
