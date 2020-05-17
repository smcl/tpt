module Common

open Argu

type Argument =
    | [<AltCommandLine("-s")>]Source of string
    | [<AltCommandLine("-t")>]Transform of string
    | [<AltCommandLine("-d")>]Destination of string option
    interface IArgParserTemplate with 
        member arg.Usage =
            match arg with
            | Source _ -> "Location of the App.config or Web.config file to transform"
            | Transform _ -> "Location of the transform to apply"
            | Destination _ -> "Location of file to write transformed file to"

type Arguments = { source: string; transform: string; destination: string option }

type Result<'TSuccess, 'TFailure> =
    | Success of 'TSuccess
    | Failure of 'TFailure

let bind switchFunc = function
    | Success s -> switchFunc s
    | Failure f -> Failure f
    
let (>>=) twoTrackInput switchFunc = 
    bind switchFunc twoTrackInput