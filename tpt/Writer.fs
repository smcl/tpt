module Writer

open System.IO
open Types

let writeToFile destination result =
    File.WriteAllText(destination, result)

let getWriter args =
    match args.destination with 
    | Some dest -> writeToFile dest
    | None -> printfn "%s"

let Write (args:Arguments) result = 
    result |> getWriter args
    0