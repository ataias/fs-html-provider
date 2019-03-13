// Learn more about F# at http://fsharp.org
// Code here was written following book Expert F# 4.0

open System
open FSharp.Data
open Suave
open Suave.Http
open Suave.Web

type Species = HtmlProvider<"https://en.wikipedia.org/wiki/The_world's_100_most_threatened_species">

let printTupleArray t_array =
    for t, n in t_array do
      printfn "(%s, %A)" t n

[<EntryPoint>]
let main argv =
    let species = [ for x in Species.GetSample().Tables.``Species list``.Rows -> x.Type, x.``Common name`` ]
    let speciesSorted =
      species
        |> List.countBy fst
        |> List.sortByDescending snd
    printTupleArray speciesSorted
    let html =
      String.concat "\n" [
        yield "<html><body><ul>"
        for (category, count) in speciesSorted do
          yield sprintf "<li>Category <b>%s</b>: <b>%d</b></li>" category count
        yield "</ul></body></html>"
      ]
    startWebServer defaultConfig (Successful.OK html)
    0 // return an integer exit code
