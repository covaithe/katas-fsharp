namespace Kata

open System
module WordSearch =

    type Cell = {letter:char; x:int; y:int;}
    type Direction = {x:int; y:int;}

    let split (separator:string) (input:string) = input.Split(separator)

    let parseLine y line =
        line
        |> split ","
        |> Seq.mapi (fun x c -> {letter=c.[0]; x=x; y=y;})

    let parse input =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat

    let locationOf (cell:Cell) =
        sprintf "(%i,%i)" cell.x cell.y

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

    let cellVector (cells: Cell seq) (start: Cell) direction =
        let findIthCell i =
            cells |> Seq.tryFind (fun c ->
                c.x = start.x + i*direction.x &&
                c.y = start.y + i*direction.y
            )
        Seq.initInfinite findIthCell

    let toWord candidate = candidate |> Seq.map (fun c -> c.letter) |> String.Concat
    let matches word candidate = toWord candidate = word

    let find cells word =
        let firstChar = Seq.head word
        let starts = cells |> Seq.filter (fun c -> c.letter = firstChar)

        let makeCandidate start direction =
            cellVector cells start direction
            |> Seq.take (Seq.length word)
            |> Seq.choose id

        let candidatesAt start =
            directions
            |> Seq.map (makeCandidate start)


        let candidates = starts |> Seq.map candidatesAt |> Seq.concat
        let answer = candidates |> Seq.find (matches word)

        answer
        |> Seq.map locationOf
        |> String.concat ","

    let solve (input: string) =
        let lines = input |> split "\n"
        let words = lines |> Seq.head |> split ","
        let cells = lines |> Seq.tail |> String.concat "\n" |> parse

        let findAnswer word = sprintf "%s: %s" word (find cells word)

        words
        |> Seq.map findAnswer
        |> String.concat "\n"

