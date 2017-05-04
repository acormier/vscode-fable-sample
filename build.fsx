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

Target "NpmBuild" (fun _ ->
  Npm (fun p ->
    {p with
      Command = (Run "build")
      WorkingDirectory = "./"
      })
)

"NpmInstall"
==> "NpmBuild"

RunTargetOrDefault "NpmBuild"
