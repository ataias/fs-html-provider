// Learn more about F# at http://fsharp.org

open System
open FSharp.Data


type Species = HtmlProvider<"https://en.wikipedia.org/wiki/The_world's_100_most_threatened_species">

let printTupleArray t_array =
    for t, n in t_array do
      printfn "(%s, %A)" t n

[<EntryPoint>]
let main argv =
    let species = [ for x in Species.GetSample().Tables.``Species list``.Rows -> x.Type, x.``Common name`` ]
    let processed =
      species
        |> List.countBy fst
        |> List.sortByDescending snd
    printTupleArray processed
    0 // return an integer exit code
