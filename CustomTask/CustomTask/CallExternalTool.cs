using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace CustomTask
{
    public class CallExternalTool : ToolTask
    {
        [Required]
        public string InputOpenApi { get; set; }
        protected override string ToolName => "TestFred";

        protected override string GenerateCommandLineCommands()
        {
            //Parametros??
            return $"openapi2csclient /input:{InputOpenApi}  /classname:PetStoreClient /namespace:PetStoreClient /output:PetStoreClient.cs";
        }

        protected override string GenerateFullPathToTool()
        {
            return "C:\\Users\\far\\Downloads\\Win\\NSwag.exe";
        }

        protected override bool ValidateParameters()
        {
            //absolute path is not allowed
            LogEventsFromTextOutput($"Test Fred {InputOpenApi} {InputOpenApi.StartsWith("C:\\")}", MessageImportance.High);
            if (InputOpenApi.StartsWith("C:\\")) 
                Log.LogError("absolute path is not allowed");
            return !InputOpenApi.StartsWith("C:\\");
        }
    }
}
