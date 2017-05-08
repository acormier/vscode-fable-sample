module Extension

open Fable
open Fable.Import
open Fable.Import.vscode
open Fable.Core.JsInterop

let activate(context: ExtensionContext) =
  let sb = window.createStatusBarItem(StatusBarAlignment.Left)

  let wordCount (d: TextDocument) =
    System.Text.RegularExpressions.Regex(@"\w+").Matches(d.getText()).Count

  let showIfMdElseHide(ev) =
    match window.activeTextEditor with
    | Some e ->
        let d = e.document
        if d.languageId = "markdown"
          then 
            sb.text <- wordCount d |> sprintf "word count: %d"
            sb.show()
          else
            sb.hide()
    | _ -> printf "Active text editor is undefined"

  showIfMdElseHide()

  let disposables: Disposable[] = [||]
  window.onDidChangeActiveTextEditor $ (showIfMdElseHide, (), disposables) |> ignore
  window.onDidChangeTextEditorSelection $ (showIfMdElseHide, (), disposables) |> ignore