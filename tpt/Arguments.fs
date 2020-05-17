module Arguments

open Argu
open System
open Common

let parse argv =
    let errorHandler = ProcessExiter(colorizer = function ErrorCode.HelpText -> None | _ -> Some ConsoleColor.Red)
    let parser = ArgumentParser.Create<Argument>(programName = "tpt.exe", errorHandler = errorHandler)
    let result = parser.ParseCommandLine argv
    { source = result.GetResult(Source); transform = result.GetResult(Transform); destination = result.GetResult(Destination, None) }
