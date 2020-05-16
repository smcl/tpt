module Types

open Argu

type Argument =
    | Source of string
    | Transform of string
    | Destination of string option
    interface IArgParserTemplate with 
        member arg.Usage =
            match arg with
            | Source _ -> "Location of the App.config or Web.config file to transform"
            | Transform _ -> "Location of the transform to apply"
            | Destination _ -> "Location of file to write transformed file to"

type Arguments = { source: string; transform: string; destination: string option }