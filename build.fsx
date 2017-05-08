// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#I "packages/FAKE/tools"
#r "FakeLib.dll"
open System
open System.Diagnostics
open System.IO
open Fake
open Fake.NpmHelper
open Fake.Git
open Fake.ProcessHelper
open Fake.ReleaseNotesHelper
open Fake.ZipHelper

Target "NpmInstall" (fun _ ->
  Npm (fun p ->
        { p with
            Command = Install Standard
            WorkingDirectory = "./"
        })
 )

Target "DotNetRestore" (fun _ ->
  DotNetCli.Restore (fun p -> 
         { p with 
              NoCache = true })
)

Target "DotNetBuild" (fun _ ->
  DotNetCli.RunCommand
    (fun p -> 
         { p with 
              TimeOut = TimeSpan.FromMinutes 10. })
    "fable npm-run build -v d"
)

"NpmInstall"
==> "DotNetRestore"
==> "DotNetBuild"

RunTargetOrDefault "DotNetBuild"
