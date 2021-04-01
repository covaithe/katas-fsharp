namespace Kata

module WordSearch =

    type Cell = {x:int; y:int; letter: string}
    type Direction = {x:int; y:int;}
    let buildCell x y c = {x=x; y=y; letter=c}

    let split (delim: string) (s:string) = s.Split(delim)

    let parseLine y line =
        line
        |> split ","
        |> Seq.mapi (fun x char -> buildCell x y char)

    let parse (input: string) : Cell list =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat
        |> List.ofSeq

    let locationOf (cell:Cell) =
        sprintf "(%i,%i)" cell.x cell.y

    let pathOf (candidate: Cell seq) =
        candidate
        |> Seq.map locationOf
        |> String.concat ","

    let toWord path =
        path
        |> Seq.map (fun c -> c.letter)
        |> String.concat ""

    let directions = [
        {x = -1; y = -1}
        {x =  0; y = -1}
        {x =  1; y = -1}
        {x = -1; y =  0}
        {x =  1; y =  0}
        {x = -1; y =  1}
        {x =  0; y =  1}
        {x =  1; y =  1}
    ]

    let cellVector (cells:Cell list) (start:Cell) direction =
        Seq.initInfinite (fun i ->
            cells |> Seq.tryFind (fun cell ->
                cell.x = start.x + i*direction.x && cell.y = start.y + i*direction.y
            )
        )

    let matches word path = word = toWord path

    let find cells (word:string) : string =
        let firstChar = word.[0..0]
        let starts = cells |> Seq.filter (fun c -> c.letter = firstChar)

        let candidateFrom start direction =
            cellVector cells start direction
            |> Seq.take word.Length
            |> Seq.choose id

        let lookInAllDirections start =
            directions
            |> Seq.map (fun direction -> candidateFrom start direction)

        let answer =
            starts
            |> Seq.map lookInAllDirections
            |> Seq.concat
            |> Seq.find (matches word)

        pathOf answer

    let solve (input: string) =
        let lines = split "\n" input |> List.ofArray
        let words = lines.Head |> split ","
        let body = lines.Tail |> String.concat "\n"
        let cells = parse body

        words
        |> Seq.map (fun w -> sprintf "%s: %s" w (find cells w))
        |> String.concat "\n"



