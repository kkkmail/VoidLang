open System.IO
open VoidLang.Commands

[<EntryPoint>]
let main argv =
    try
        let processFile (f : string) =
            if f.EndsWith brainFuckExtension
            then
                f
                |> getContent
                |> fromBrainFuck
                |> saveContent (getVoidFileName f false)
            else if f.EndsWith voidExtension || f.EndsWith voidAltExtension
            then
                f
                |> getContent
                |> toBrainFuck
                |> saveContent (getBrainFuckFileName f)                
            else raise (InvalidDataException($"Invalid file name: '{f}'."))

        argv |> Array.map processFile |> ignore
        0
    with
    | exn ->
        printfn $"%s{exn.Message}"
        -1        
