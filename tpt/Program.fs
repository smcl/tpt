open Arguments
open Transformer
open Writer

[<EntryPoint>]
let main argv = 
    let args = Parse argv
    Transform args
    |> Write args
