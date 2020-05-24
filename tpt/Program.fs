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
    sprintf "%s %d.%d.%d" assemblyName.Name version.Major version.Minor version.Build
    |> Success

let getAction = function
    | PrintVersion -> printVersion
    | Default args -> apply args.source args.transform args.squashWhitespace

let getWriter = function 
    | PrintVersion -> write None
    | Default args -> write args.destination

[<EntryPoint>]
let main argv = 
    let args = parse argv
    getAction args
    >>= getWriter args
    |> returnCode
