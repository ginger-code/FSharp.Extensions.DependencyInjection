open Expecto

[<EntryPoint>]
let main argv =
    Tests.runTestsInAssemblyWithCLIArgs [| CLIArguments.Colours 256; CLIArguments.Summary |] argv
