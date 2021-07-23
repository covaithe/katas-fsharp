namespace Kata

module WordSearch =

    type Cell = { letter:char; x:int; y:int}
    type Direction = { x:int; y:int}

    let split (sep:string) (input:string) = input.Split(sep)
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
    let toLocation (cell:Cell) = sprintf "(%i,%i)" cell.x cell.y
    let toPath cells = cells |> Seq.map toLocation |> String.concat ","
    let toAnswer cells = sprintf "%s: %s" (toWord cells) (toPath cells)
    let matches word cells = word = toWord cells

    let find cells word =
        let firstChar = Seq.head word
        let starts = cells |> Seq.filter (fun c -> c.letter = firstChar)

        let findNthCellFrom (start:Cell) direction n =
            cells
            |> Seq.tryFind (fun c -> c.x = start.x+n*direction.x && c.y = start.y+n*direction.y)

        let makeCandidate start direction =
            Seq.initInfinite (findNthCellFrom start direction)
            |> Seq.take (Seq.length word)
            |> Seq.choose id

        let directions = [
            { x = -1; y= -1 }
            { x =  0; y= -1 }
            { x =  1; y= -1 }
            { x = -1; y=  0 }
            { x =  1; y=  0 }
            { x = -1; y=  1 }
            { x =  0; y=  1 }
            { x =  1; y=  1 }
        ]
        let findCandidatesAt start = directions |> Seq.map (makeCandidate start)
        let candidates = starts |> Seq.collect findCandidatesAt
        candidates |> Seq.find (matches word)

    let solve (input: string) =
        let lines = split "\n" input
        let words = lines |> Seq.head |> split ","
        let puzzle = lines |> Seq.tail |> String.concat "\n" |> parse
        words
        |> Seq.map ((fun word -> find puzzle word) >> toAnswer)
        |> String.concat "\n"


