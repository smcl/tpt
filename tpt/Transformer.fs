module Transformer

open Common
open System.IO
open System.Xml
open Microsoft.Web.XmlTransform

let tryOpen openFunc xmlFile = 
    try
        openFunc xmlFile
        |> Success
    with
        | :? XmlException as ex -> Failure (sprintf "Unable to parse %s, check it is valid XML" xmlFile)
        | :? FileNotFoundException as ex -> Failure (sprintf "Unable to open %s, check that the file exists and that you have permission to access it" xmlFile)
        | _ -> Failure (sprintf "Something went wrong when loading the XML: %s" xmlFile)

let getDocument source squashWhitespace =
    let openSource (squashWhitespace:bool) (source:string) =
        let document = new XmlDocument(PreserveWhitespace = not squashWhitespace)
        document.Load(source)
        document
    tryOpen
        (openSource squashWhitespace)
        source

let getTransformation transform =
    let openTransformFile path = new XmlTransformation(path)
    tryOpen
        openTransformFile
        transform

let applyTransform (x:XmlTransformation) (d:XmlDocument) =
    if x.Apply(d)
    then Success d
    else Failure "Error applying transform"
    
let apply (source:string) (transform:string) (squashWhitespace: bool) =
    let document = getDocument source squashWhitespace
    let transformation = getTransformation transform
    match document, transformation with
    | Success d, Success t ->
        applyTransform t d
        >>= (fun (q:XmlDocument) -> Success q.OuterXml)
    | Success _, Failure e -> Failure e
    | Failure e, Failure _ -> Failure e
    | Failure e, Success _ -> Failure e