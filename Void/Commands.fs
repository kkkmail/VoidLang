namespace VoidLang

open System
open System.IO

module Commands =
    let brainFuckExtension = ".bf"
    let voidExtension = ".   "
    let voidAltExtension = ".void"

    let private brainFuckCommands = "><+-.,[]".ToCharArray()

    let fromBrainFuckMap =
        brainFuckCommands
        |> Array.mapi (fun i e -> e, new string(' ', i + 1))
        |> Map.ofArray


    let toBrainFuckMap =
        fromBrainFuckMap
        |> Map.toArray
        |> Array.map(fun (a, b) -> b, a)
        |> Map.ofArray

    let separators = "\r\n\t".ToCharArray()
    
    
    let toBrainFuck (s : string) =
        s.Split(separators, StringSplitOptions.RemoveEmptyEntries)
        |> Array.map (fun e -> toBrainFuckMap[e])
        |> String.Concat

    
    
    let fromBrainFuck (s : string) =
        s.ToCharArray()
        |> Array.filter fromBrainFuckMap.ContainsKey
        |> Array.map (fun e -> fromBrainFuckMap[e] + "\t")
        |> String.Concat


    let getFileFileName (f : string) ext =
        let folder = f |> Path.GetDirectoryName
        let name = f |> Path.GetFileNameWithoutExtension
        let f = Path.Combine(folder, name) + ext
        f

    let getBrainFuckFileName f = getFileFileName f brainFuckExtension
    let getVoidFileName f useAltExt = getFileFileName f (if useAltExt then voidAltExtension else voidExtension)
    
    
    let getContent (f : string) = File.ReadAllText f
    let saveContent f s = File.WriteAllText(f, s)
