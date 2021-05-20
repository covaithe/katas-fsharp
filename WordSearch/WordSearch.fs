namespace Kata
open System

module WordSearch =

    type Cell = { letter: char; x: int; y: int;}
    type Direction = { x: int; y: int;}

    let split (sep:string) (s:string) = s.Split(sep)

    let parseLine y line =
        line
        |> split ","
        |> Seq.mapi (fun x c -> {letter=c.[0]; x=x; y=y;})

    let parse input: Cell seq =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat

    let toWord path =
        path
        |> Seq.map (fun cell -> cell.letter)
        |> String.Concat

    let toLocation (cell:Cell) = sprintf "(%i,%i)" cell.x cell.y
    let toPathLocation path = path |> Seq.map toLocation |> String.concat ","

    let toAnswer path =
        let word = toWord path
        let location = path |> toPathLocation
        sprintf "%s: %s" word location

    let matches word path = toWord path = word

    let find cells word =
        let firstChar = word |> Seq.head
        let starts = cells |> Seq.filter (fun c -> c.letter = firstChar)

        let findNthChar (start:Cell) dir n = cells |> Seq.tryFind (fun c ->
            c.x = start.x + n*dir.x && c.y = start.y + n*dir.y)

        let makeCandidate start direction =
            Seq.initInfinite (findNthChar start direction)
            |> Seq.take (Seq.length word)
            |> Seq.choose id

        let directions = [
            { x = -1; y = -1; }
            { x =  0; y = -1; }
            { x =  1; y = -1; }
            { x = -1; y =  0; }
            { x =  1; y =  0; }
            { x = -1; y =  1; }
            { x =  0; y =  1; }
            { x =  1; y =  1; }
        ]

        let candidatesStartingAt start =
            directions
            |> Seq.map (makeCandidate start)

        let candidates =
            starts
            |> Seq.map candidatesStartingAt
            |> Seq.concat

        candidates |> Seq.find (matches word)


    let solve (input: string) = ""

