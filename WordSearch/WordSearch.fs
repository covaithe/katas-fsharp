namespace Kata

module WordSearch =
    type Cell = {letter: string; x: int; y: int }
    type Direction = {x:int; y:int}

    let parseLine (rownum : int) (line : string) : Cell list =
        line
        |> (fun s -> s.Split(","))
        |> Seq.mapi (fun foo c -> {letter=c; x=foo; y=rownum })
        |> Seq.toList

    let parse (input : string) : Cell list =
        input
        |> (fun s -> s.Split("\n"))
        |> Seq.mapi parseLine
        |> Seq.concat
        |> List.ofSeq

    let find cells (word: string) =
        let starts =
            cells
            |> List.filter (fun c -> c.letter = word.[0..0])

        let cellAt x y = cells |> List.tryFind (fun c -> c.x = x && c.y = y)

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

        let look (start : Cell) (direction : Direction) =
            word
            |> Seq.mapi (fun i char ->
                cellAt (start.x + i*direction.x) (start.y + i*direction.y))
            |> Seq.choose id
            |> Seq.toList

        let candidates = seq {
            for dir in directions do
                for start in starts do
                    yield look start dir
        }

        let hasTheRightWord candidate =
            let candidateWord = candidate |> Seq.map (fun cell -> cell.letter) |> String.concat ""
            word = candidateWord

        let print (cells : Cell list) =
            cells
            |> List.map (fun cell -> sprintf "(%i,%i)" cell.x cell.y)
            |> String.concat ","

        candidates |> Seq.find hasTheRightWord |> print

    let solve (input:string) =
        let lines = List.ofArray (input.Split("\n"))
        let words = lines.Head.Split(",")
        let body = lines.Tail |> String.concat "\n"
        let cells = parse body
        words
        |> Seq.map (fun word ->
            sprintf "%s: %s" word (find cells word)
        )
        |> String.concat "\n"
