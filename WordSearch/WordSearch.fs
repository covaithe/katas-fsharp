namespace Kata

module WordSearch =

    type Cell = { letter:char; x:int; y:int }
    type Direction = { x:int; y: int}

    let split (separator:string) (input:string) = input.Split(separator)
    let parseLine y line =
        line
        |> split ","
        |> Seq.mapi (fun x c -> {letter=c.[0]; x=x; y=y})

    let parse input =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat

    let toWord cells = cells |> Seq.map (fun c -> c.letter) |> System.String.Concat
    let toCoords (cell:Cell) = sprintf "(%i,%i)" cell.x cell.y
    let toPath cells = cells |> Seq.map toCoords |> String.concat ","
    let toAnswer cells = sprintf "%s: %s" (toWord cells) (toPath cells)
    let matches word cells = word = toWord cells

    let find cells word =
        let firstChar = word |> Seq.head
        let starts = cells |> Seq.filter (fun c -> c.letter = firstChar)

        let nextChar (start:Cell) direction i = cells |> Seq.tryFind (fun c ->
            c.x = start.x+i*direction.x && c.y = start.y + i*direction.y)
        let cellVector start direction =
            Seq.initInfinite (nextChar start direction)
            |> Seq.take (Seq.length word)
            |> Seq.choose id

        let directions = [
            { x = -1; y = -1 }
            { x =  0; y = -1 }
            { x =  1; y = -1 }
            { x = -1; y =  0 }
            { x =  1; y =  0 }
            { x = -1; y =  1 }
            { x =  0; y =  1 }
            { x =  1; y =  1 }
        ]
        let findCandidatesAt start = directions |> Seq.map (cellVector start)
        let candidates = starts |> Seq.collect findCandidatesAt

        candidates |> Seq.find (matches word)

    let solve (input: string) =
        let lines = input |> split "\n"
        let words = lines |> Seq.head |> split ","
        let puzzle = lines |> Seq.tail |> String.concat "\n"
        let cells = parse puzzle

        words
        |> Seq.map ((find cells) >> toAnswer)
        |> String.concat "\n"

