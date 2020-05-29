module Writer

open Common
open System.IO
open System

let writeToFile destination result =
    try 
        File.WriteAllText(destination, result) |> Success
    with 
        | :? DirectoryNotFoundException as ex -> Failure (sprintf "Couldn't write output to %s - check the directory exists" destination)
        | :? UnauthorizedAccessException as ex -> Failure (sprintf "Couldn't write output to %s - check that you have permission to write to it" destination)
        | _ -> Failure (sprintf "Something went wrong writing to %s" destination)

let writeToConsole result =
    printfn "%s" result |> Success

let getWriter = function
    | Some dest -> writeToFile dest
    | None -> writeToConsole

let write (destination:string option) (output:string) = 
    let writer = getWriter destination
    writer output
    