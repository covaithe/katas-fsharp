namespace Kata

module WordSearch =

    type Cell = {x:int; y:int; letter:char}
    type Direction = {x:int; y:int;}

    let split (s:string) (input:string) = input.Split(s)
    let parseLine y line =
        line
        |> split ","
        |> Seq.mapi (fun x c -> {x=x; y=y; letter=c.[0]})

    let parse input =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat

    let toWord cells = cells |> Seq.map (fun cell -> cell.letter) |> System.String.Concat
    let toLocation (cell:Cell) = sprintf "(%i,%i)" cell.x cell.y
    let toPath cells = cells |> Seq.map toLocation |> String.concat ","
    let toAnswer cells = sprintf "%s: %s" (toWord cells) (toPath cells)
    let matches word candidate = word = toWord candidate

    let find cells word =
        let firstChar = word |> Seq.head
        let starts = cells |> Seq.filter (fun c -> c.letter = firstChar)

        let directions = [
            { x = -1; y = -1;}
            { x =  0; y = -1;}
            { x =  1; y = -1;}
            { x = -1; y =  0;}
            { x =  1; y =  0;}
            { x = -1; y =  1;}
            { x =  0; y =  1;}
            { x =  1; y =  1;}
        ]

        let nextCellInWord (start:Cell) direction n = cells |> Seq.tryFind (fun c -> c.x = start.x + n*direction.x && c.y = start.y + n*direction.y)
        let candidateInDirection start direction =
            Seq.initInfinite (nextCellInWord start direction)
            |> Seq.take (Seq.length word)
            |> Seq.choose id

        let candidatesFromStart start = directions |> Seq.map (candidateInDirection start)

        starts |> Seq.collect candidatesFromStart |> Seq.find (matches word)

    let solve (input: string) =
        let lines = input |> split "\n"
        let words = lines |> Seq.head |> split ","
        let puzzle = lines |> Seq.tail |> String.concat "\n" |> parse

        let findAnswer word = find puzzle word |> toAnswer

        words
        |> Seq.map findAnswer
        |> String.concat "\n"



