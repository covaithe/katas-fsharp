namespace Kata
open System

module WordSearch =

    type Cell = { letter:char; x:int; y:int; }
    type Direction = { x:int; y:int; }

    let split (separator:string) (s:string) = s.Split(separator)

    let parseLine y line =
        line
        |> split ","
        |> Seq.mapi (fun x c -> {letter=c.[0]; x=x; y=y;})

    let parse input =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat

    let formatAnswer word answer =
        let toLocation (cell:Cell) = sprintf "(%i,%i)" cell.x cell.y
        let path =
            answer
            |> Seq.map toLocation
            |> String.concat ","

        sprintf "%s: %s" word path

    let cellVector (cells:Cell seq) (start: Cell) direction =
        let isNextCell i (cell:Cell) =
            cell.x = start.x + i*direction.x &&
                cell.y = start.y + i*direction.y

        Seq.initInfinite (fun i -> cells |> Seq.tryFind (isNextCell i))

    let toWord candidate = candidate |> Seq.map (fun c -> c.letter) |> String.Concat
    let matches word candidate = toWord candidate = word

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

    let find cells word =
        let firstChar = word |> Seq.head
        let starts = cells |> Seq.filter (fun c -> c.letter = firstChar)

        let makeCandidate start direction =
            cellVector cells start direction
            |> Seq.take (Seq.length word)
            |> Seq.choose id

        let candidatesAt start = directions |> Seq.map (makeCandidate start)

        let candidates = starts |> Seq.map candidatesAt |> Seq.concat
        let answer = candidates |> Seq.find (matches word)

        formatAnswer word answer

    let solve (input: string) =
        let lines = input |> split "\n"
        let words = lines |> Seq.head |> split ","
        let cells = lines |> Seq.tail |> String.concat "\n" |> parse
        words |> Seq.map (find cells) |> String.concat "\n"



