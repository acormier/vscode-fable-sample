#r "../node_modules/fable-core/Fable.Core.dll"
#load "../node_modules/fable-import-vscode/Fable.Import.VSCode.fs"

open Fable
open Fable.Import
open Fable.Import.vscode
open Fable.Core.JsInterop

let activate(context: ExtensionContext) =
  let sb = window.createStatusBarItem(StatusBarAlignment.Left)

  let wordCount (d: TextDocument) =
    System.Text.RegularExpressions.Regex(@"\w+").Matches(d.getText()).Count

  let showIfMdElseHide() =
    let e = window.activeTextEditor
    let d = e.document
    if d.languageId = "markdown"
      then 
        sb.text <- wordCount d |> sprintf "word count: %d"
        sb.show()
      else 
        sb.hide()

  showIfMdElseHide()

  let disposables: Disposable[] = [||]
  window.onDidChangeActiveTextEditor $ (showIfMdElseHide, (), disposables) |> ignore
  window.onDidChangeTextEditorSelection $ (showIfMdElseHide, (), disposables) |> ignore