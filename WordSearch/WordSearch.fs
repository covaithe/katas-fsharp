namespace Kata

module WordSearch =

    type Cell = { letter:char; x:int; y:int }
    type Direction = { x:int; y:int }

    let split (delimiter:string) (input:string) = input.Split(delimiter)
    let parseLine y line =
        line
        |> split ","
        |> Seq.mapi (fun x c -> { letter=c.[0]; x=x; y=y; })

    let parse input =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat


    let toWord cells = cells |> Seq.map (fun cell -> cell.letter) |> System.String.Concat
    let toLocation (cell:Cell) = sprintf "(%i,%i)" cell.x cell.y
    let toPath cells = cells |> Seq.map toLocation |> String.concat ","
    let matches word candidate = word = toWord candidate

    let find cells word =
        let firstChar = word |> Seq.head
        let starts = cells |> Seq.filter (fun cell -> cell.letter = firstChar)

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

        let nextCell (start:Cell) direction n = cells |> Seq.tryFind (fun cell -> cell.x = start.x + n*direction.x && cell.y = start.y + n*direction.y)
        let buildCandidateFrom start direction =
            Seq.initInfinite (nextCell start direction)
            |> Seq.take (Seq.length word)
            |> Seq.choose id

        let findAllCandidatesStartingHere start = directions |> Seq.map (buildCandidateFrom start)

        starts
        |> Seq.collect findAllCandidatesStartingHere
        |> Seq.find (matches word)


    let solve (input: string) =
        let lines = input |> split "\n"
        let words = lines |> Seq.head |> split ","
        let puzzle = lines |> Seq.tail |> String.concat "\n" |> parse

        let toAnswer word = sprintf "%s: %s" word (find puzzle word |> toPath)

        words
        |> Seq.map toAnswer
        |> String.concat "\n"
