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

Next:

1. Complete code generation
1. Investigate Pre/Post/Clean Steps (More generic, stay away from built-in)
1. Include custom task on nuget to be used
