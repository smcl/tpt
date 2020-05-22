module Arguments

open Argu
open System
open Common
open System.Reflection

let parse argv =
    let errorHandler = ProcessExiter(colorizer = function ErrorCode.HelpText -> None | _ -> Some ConsoleColor.Red)
    let assemblyName = Assembly.GetExecutingAssembly().GetName()
    let parser = ArgumentParser.Create<Argument>(programName = assemblyName.Name, errorHandler = errorHandler)
    let result = parser.ParseCommandLine argv

    if result.Contains(Version)
    then PrintVersion
    else Default { 
            source = result.GetResult(Source)
            transform = result.GetResult(Transform)
            destination = result.GetResult(Destination, None)
            squashWhitespace = result.Contains(Squash_Whitespace)
        }
