open Printf

type command = 
    | Set of string * string 
    | Get of string 
    | Help 
    | Exit
    | Invalid

let read_cmd (cmd:string): command = 
    let info = String.split_on_char ' ' cmd in match info with
    | ["set"; key; value] -> Set(key, value)
    | ["get"; key] -> Get(key)
    | ["help"] | ["get_all"] -> Help
    | ["exit"] | ["quit"] | ["exit()"] | ["quit()"] | ["q!"] -> Exit
    | _ -> Invalid

let print_help () =
    print_string "this an in memory key value store\n";
    List.iter (
        fun str -> printf "> available command %s\n" str
    ) ["exit|quit|q!"; "help|get_all"; "set"; "get"];
    ()

let rec loop (ht:Hashtbl.t (string, string)) =
    printf ">>> ";
    let cmd = read_cmd (read_line ()) in match cmd with
    | Set(k,v) -> Hashtbl.add ht k v; loop ht
    | Get(k) -> (
        let maybe_value = Hashtbl.find_opt ht k in match maybe_value with 
        | Some(value) -> printf "> %s\n" value
        | None -> ()
    ); loop ht
    | Help -> print_help (); loop ht
    | Invalid -> print_string "invalid cmd\n"; loop ht
    | Exit -> ()
