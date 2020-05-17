module Writer

open System.IO

let writeToFile destination result =
    File.WriteAllText(destination, result)

let getWriter destination =
    match destination with 
    | Some dest -> writeToFile dest
    | None -> printfn "%s"

let write (destination:string option) (xml:string) = 
    xml |> getWriter destination
    0