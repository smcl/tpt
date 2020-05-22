open Arguments
open Common
open Transformer
open Writer
open System.Reflection

let returnCode = function
    | Success _ -> 0
    | Failure ex -> 
        printfn "%s" ex
        1

let printVersion =
    let assembly = Assembly.GetExecutingAssembly()
    let assemblyName = assembly.GetName()
    let version = assemblyName.Version
    Success <| sprintf "%s %d.%d.%d" assemblyName.Name version.Major version.Minor version.Build

[<EntryPoint>]
let main argv = 
    parse argv |> function
        (* TODO: don't like this, rewrite *)
        | PrintVersion -> 
            printVersion
            >>= write None
        | Default args -> 
            apply args.source args.transform args.squashWhitespace
            >>= write args.destination
        |> returnCode
