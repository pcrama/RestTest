// Learn more about F# at http://fsharp.org

open System

open RestSharp
open RestSharp.Deserializers

open FsCheck

// let urlIsNotLost (uri:string) = RestClient(uri).BaseUrl = new Uri(uri)

let accept2types((t1, t2):string * string) =
    if t1 = null || t2 = null || t1 = "" || t2 = "" || t1 = t2
    then
       true
    else
        let mutable add2ThenRemoveSecond = RestClient();
        let mutable addOnlyFirst = RestClient();
        add2ThenRemoveSecond.ClearHandlers();
        addOnlyFirst.ClearHandlers();
        add2ThenRemoveSecond.AddHandler(t1, JsonDeserializer());
        addOnlyFirst.AddHandler(t1, JsonDeserializer());
        add2ThenRemoveSecond.AddHandler(t2, XmlDeserializer());
        add2ThenRemoveSecond.RemoveHandler(t2);
        add2ThenRemoveSecond.DefaultParameters = addOnlyFirst.DefaultParameters

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!";
    Check.One({ Config.Quick with MaxTest = 10; QuietOnSuccess=false }, accept2types);
    0 // return an integer exit code
