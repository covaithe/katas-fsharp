namespace Kata

module WordSearch =

    type Cell = {letter:char; x:int; y:int;}
    type Direction = {x:int; y:int;}

    let split (separator:string) (input:string) = input.Split(separator)

    let parseLine y line =
        line
        |> split ","
        |> Seq.mapi (fun x c -> {letter=c.[0]; x=x; y=y;})

    let parse input : Cell seq =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat

    let toWord path = path |> Seq.map (fun cell -> cell.letter) |> System.String.Concat
    let toLocation (cell:Cell) = sprintf "(%i,%i)" cell.x cell.y
    let toPath path = path |> Seq.map toLocation |> String.concat ","
    let toAnswer path = sprintf "%s: %s" (toWord path) (toPath path)
    let matches word path = toWord path = word

    let find cells word : Cell seq =
        let firstChar = word |> Seq.head
        let starts = cells |> Seq.filter (fun c -> c.letter = firstChar)

        let findNthCharFrom (start:Cell) (direction:Direction) n =
            cells |> Seq.tryFind (fun c ->
                c.x = start.x + direction.x*n && c.y = start.y + direction.y*n)
        let makeCandidate start direction =
            Seq.initInfinite (findNthCharFrom start direction)
            |> Seq.take (Seq.length word)
            |> Seq.choose id

        let directions = [
            {x = -1; y = -1;}
            {x =  0; y = -1;}
            {x =  1; y = -1;}
            {x = -1; y =  0;}
            {x =  1; y =  0;}
            {x = -1; y =  1;}
            {x =  0; y =  1;}
            {x =  1; y =  1;}
        ]

        let candidatesAt start = directions |> Seq.map (makeCandidate start)
        let candidates = starts |> Seq.map candidatesAt |> Seq.concat

        candidates |> Seq.find (matches word)

    let solve (input: string) =
        let lines = input |> split "\n"
        let words = lines |> Seq.head |> split ","
        let cells = lines |> Seq.tail |> String.concat "\n" |> parse
        words
        |> Seq.map (find cells)
        |> Seq.map toAnswer
        |> String.concat "\n"

