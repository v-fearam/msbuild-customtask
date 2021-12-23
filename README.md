# msbuild-customtask

1. Build CustomTask project (AppSettingStronglyTyped)
1. Build BuildConsoleExample project (Console app, which use AppSettingStronglyTyped and generate a MySetting.generated.cs)
1. It can be executed by

```dotnetcli
# On BuildConsoleExample\BuildConsoleExample (-bl binary log)
dotnet build -bl
# or
msbuild -bl
#it will generate a log  msbuild.binlog, and it can be open with https://msbuildlog.com/
```

You can delete MySetting.generated.cs, it will be regenerated

The files is using incremental build, it is only regenerated only if

1. The document doesn't exist
2. The timestamp of the input file is newer than the output

The rebuild regenerates, it is because the file is deleted after CoreClean (before the build). The clean delete also the generated file.

Next:

1. Unit Test
1. Include custom task on nuget to be used
1. define scenario:
   1. What about generate a api rest client during build? using https://github.com/Azure/autorest. It is a Microsoft tool.
   2. We can create a small api and generate the console app to access it during build....

To be include in the narrative:

* different between “full” MSBuild (the one that powers Visual Studio) and “portable” MSBuild, or the one bundled in the .NET Core Command Line. (MSBuild.exe runs on .NET Framework)
* clear example on how to register a custom task based on UsingTask element
* declaimer to see basic concepts on other articles
* Custom task on netstandar2.0 and not netstandar2.1 and why
* Input and output parameters on the custom task. Simple input and list (items) input. 
* The custom task should be on a different solution to the project which use the task, and the custom task dll must e generated before hand
* how to package an distribute custom task on nugent package
* how to see what is going on base on binary log -bl
* Incremental build, MSBuild can compare the timestamps of the input files with the timestamps of the output files and determine whether to skip, build, or partially rebuild a target.
* To force rebuild we need to clean generated file after CoreClean
