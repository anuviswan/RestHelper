@echo off
echo Would you like to push the packages to NuGet when finished?
set /p choice="Enter y/n: "

del *.nupkg
@echo on
nuget pack RestHelper.Net45.csproj -Exclude *.tt
if /i %choice% equ y (
    nuget push EcSolvo.RestHelper.1.0.1.140.nupkg -Source https://www.nuget.org/api/v2/package
)
pause