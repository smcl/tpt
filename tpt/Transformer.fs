module Transformer

open Common
open System.IO
open System.Xml
open Microsoft.Web.XmlTransform

let openSource (squashWhitespace:bool) (source:string) = 
    let document = new XmlDocument(PreserveWhitespace = not squashWhitespace)
    document.Load(source)
    document

let openTransform (transform:string) = 
    new XmlTransformation(transform)

let tryOpen openFunc xmlFile = 
    try
        openFunc xmlFile
        |> Success
    with
        | :? XmlException as ex -> Failure (sprintf "Unable to parse %s, check it is valid XML" xmlFile)
        | :? FileNotFoundException as ex -> Failure (sprintf "Unable to open %s, check that the file exists and that you have permission to access it" xmlFile)
        | _ -> Failure (sprintf "Something went wrong when loading the XML: %s" xmlFile)
    
let apply (source:string) (transform:string) (squashWhitespace: bool) = 
    let document = (openSource squashWhitespace |> tryOpen) source
    let transformation = (openTransform |> tryOpen) transform
    (* TODO: this match->match->if->else is hideous *)
    match document with
    | Failure e -> Failure e
    | Success d -> 
        match transformation with
        | Failure e -> Failure e
        | Success t -> if t.Apply(d)
                       then Success d.OuterXml
                       else Failure "Error applying transformation"