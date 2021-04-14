open Next

[<EntryPoint>]
let main argv =
    let dict = Map.empty
    Next.loop dict
    0 // return an integer exit code