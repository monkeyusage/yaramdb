module Next
open System

let print_help () =
    printfn "This an in memory key value store\n"
    let args = ["exit|quit|q!"; "help|get_all"; "set"; "get"]
    let _ = List.map (function str -> printf "> available command %s\n" str) args
    None

type Command = 
    | Set of key: string * value :string 
    | Get of key: string 
    | Help
    | Invalid

type MaybeAbort = 
    | Continue of cmd: Command 
    | Abort

let getCommand (cmd:string): MaybeAbort = 
    match cmd.Split ' ' with
    | [|"set"; key; value|] -> Continue(Set(key, value))
    | [|"get"; key|] -> Continue(Get(key))
    | [|"help"|] | [|"get_all"|] -> Continue(Help)
    | [|"exit"|] | [|"quit"|] | [|"exit()"|] | [|"quit()"|] | [|"q!"|] -> Abort
    | _ -> Continue(Invalid)

let exeCommand (cmd:Command, dict:Map<string,string>):Option<Map<string,string>> = 
    match cmd with
    | Set(k, v) -> Some (Map.add k v dict)
    | Get(k) -> 
        match Map.tryFind k dict with
        | Some(v) -> printfn "> value: %s" v
        | None -> printfn "> value not found"
        None
    | Invalid -> printfn "beep boop error fatal system"; None
    | Help -> print_help ()

let rec loop (dict:Map<string, string>) =
    printf ">>> ";
    let cmd = getCommand (Console.ReadLine ())
    match cmd with
    | Continue(cmd) ->
        let maybeNewMap = exeCommand(cmd, dict)
        match maybeNewMap with
        | Some(newMap) -> loop newMap
        | None -> loop dict
    | Abort -> ()
