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
    1. What about generate a api rest client during build? using https://github.com/Azure/autorest
    2. We can create a small api and generate the console app to access it during build....
