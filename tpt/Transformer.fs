module Transformer

open System.Xml
open Microsoft.Web.XmlTransform

let apply (source:string) (transform:string) = 
    let document = new XmlDocument(PreserveWhitespace = true)
    document.Load(source)

    let transform = new XmlTransformation(transform)

    match transform.Apply(document) with
    | true -> document.OuterXml
    | _ -> "Error applying transform"
