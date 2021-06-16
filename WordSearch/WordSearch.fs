namespace Kata

module WordSearch =

    type Cell = {letter:char; x:int; y:int}
    type Direction = {x:int; y:int}

    let split (separator:string) (s:string) = s.Split(separator)

    let parseLine lineNumber line =
        line
        |> split ","
        |> Seq.mapi (fun i c -> {letter=c.[0]; y=lineNumber; x=i})

    let parse input =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat

    let toWord cells = cells |> Seq.map (fun c -> c.letter) |> System.String.Concat
    let toLocation (cell:Cell) = sprintf "(%i,%i)" cell.x cell.y
    let toPath cells = cells |> Seq.map toLocation |> String.concat ","
    let matches word candidate = word = toWord candidate

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

    let find cells word =
        let firstChar = Seq.head word
        let starts = cells |> Seq.filter (fun c -> c.letter = firstChar)

        let nthCellFrom (start:Cell) (direction:Direction) i =
            cells |> Seq.tryFind (fun c ->
                c.x = start.x + i * direction.x
                && c.y = start.y + i * direction.y)

        let makeCandidate start direction =
            Seq.initInfinite (nthCellFrom start direction)
            |> Seq.take (Seq.length word)
            |> Seq.choose id

        let candidatesAt start =
            directions
            |> Seq.map (makeCandidate start)

        let candidates =
            starts
            |> Seq.map candidatesAt
            |> Seq.concat

        candidates
        |> Seq.find (matches word)

    let findAnswer cells word =
        let path = find cells word |> toPath
        sprintf "%s: %s" word path

    let solve (input: string) =
        let lines = split "\n" input
        let words = lines |> Seq.head |> split ","
        let cells = lines |> Seq.tail |> String.concat "\n" |> parse
        words
        |> Seq.map (findAnswer cells)
        |> String.concat "\n"
