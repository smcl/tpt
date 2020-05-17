module Transformer

open Common
open System.IO
open System.Xml
open Microsoft.Web.XmlTransform

let openSourceXml (source:string) = 
    let document = new XmlDocument(PreserveWhitespace = true)
    try 
        document.Load(source)
        Success document
    with
        | :? XmlException as ex -> Failure (sprintf "Unable to parse %s, check it is valid XML" source)
        | :? FileNotFoundException as ex -> Failure (sprintf "Unable to open %s, check that the file exists and that you have permission to access it" source)
        | _ -> Failure (sprintf "Something went wrong when loading the source XML: %s" source)

let openTransformXml (transform:string) = 
    try 
        Success <| new XmlTransformation(transform)
    with
        (* TODO: don't repeat yourself *)
        | :? XmlException as ex -> Failure (sprintf "Unable to parse %s, check it is valid XML" transform)
        | :? FileNotFoundException as ex -> Failure (sprintf "Unable to open %s, check that the file exists and that you have permission to access it" transform)
        | _ -> Failure (sprintf "Something went wrong when loading the transform XML: %s" transform)

let apply (source:string) (transform:string) = 
    let document = openSourceXml source 
    let transformation = openTransformXml transform
    (* TODO: this match->match->if->else is hideous *)
    match document with
    | Failure e -> Failure e
    | Success d -> 
        match transformation with
        | Failure e -> Failure e
        | Success t -> if t.Apply(d) 
                       then Success d.OuterXml
                       else Failure "Error applying transformation"