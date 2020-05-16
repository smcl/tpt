module Transformer

open Types
open System.Xml
open Microsoft.Web.XmlTransform

let Transform (args:Arguments) = 

    let document = new XmlDocument(PreserveWhitespace = true)
    document.Load(args.source)

    let transform = new XmlTransformation(args.transform)

    match transform.Apply(document) with
    | true -> document.OuterXml
    | _ -> "Error applying transform"
