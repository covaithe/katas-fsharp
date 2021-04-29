namespace Kata

open System

module WordSearch =

    type Cell = {x:int; y:int; letter:char}
    type Direction = {x:int; y:int}

    let split (delim:string) (s:string) = s.Split(delim)

    let parseLine y line =
        line
        |> split ","
        |> Seq.mapi (fun x c ->
            {x=x; y=y; letter = c |> Seq.head })


    let parse input : Cell seq =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat

    let locationOf (cell:Cell) =
        sprintf "(%i,%i)" cell.x cell.y

    let cellContains c cell = cell.letter = c

    let cellVector (cells:Cell seq) (start:Cell) dir =
        Seq.initInfinite (fun i ->
            cells |> Seq.tryFind (fun c ->
                c.x = start.x + i*dir.x
                    && c.y = start.y + i*dir.y
            ))

    let toWord cells =
        cells |> Seq.map (fun c -> c.letter) |> String.Concat

    let matches word cells = word = (toWord cells)

    let directions = [
        {x = -1; y = -1}
        {x =  0; y = -1}
        {x =  1; y = -1}
        {x = -1; y =  0}
        {x =  1; y =  0}
        {x = -1; y =  1}
        {x =  0; y =  1}
        {x =  1; y =  1}
    ]

    let find cells (word:string) =
        let starts = cells |> Seq.filter (cellContains word.[0])

        let candidateFrom start direction =
            cellVector cells start direction
            |> Seq.take word.Length
            |> Seq.choose id

        let searchFrom start =
            directions
            |> Seq.map (candidateFrom start)

        let candidates =
            starts
            |> Seq.map searchFrom
            |> Seq.concat

        let answer = candidates |> Seq.find (matches word)

        answer
        |> Seq.map locationOf
        |> String.concat ","

    let solve (input: string) =
        let lines = input |> split "\n" |> Seq.ofArray
        let words = lines |> Seq.head |> split ","
        let cells = lines |> Seq.tail |> String.concat "\n" |> parse

        let spot word =
            let answer = find cells word
            sprintf "%s: %s" word answer

        words
        |> Seq.map spot
        |> String.concat "\n"



