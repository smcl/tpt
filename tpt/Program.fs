open Arguments
open Transformer
open Writer

[<EntryPoint>]
let main argv = 
    let args = parse argv
    apply args.source args.transform
    |> write args.destination
