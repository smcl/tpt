open Arguments
open Common
open Transformer
open Writer

let returnCode = function
    | Success _ -> 0
    | Failure ex -> 
        printfn "%s" ex
        1

[<EntryPoint>]
let main argv = 
    let args = parse argv
    apply args.source args.transform
    >>= write args.destination
    |> returnCode
