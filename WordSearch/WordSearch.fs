namespace Kata

module WordSearch =

    type Cell = { letter: char; x:int; y:int; }

    let split (sep:string) (input:string) = input.Split(sep)

    let parseLine y line =
        line
        |> split ","
        |> Seq.mapi (fun x c -> {letter=c.[0]; x=x; y=y;})

    let parse input =
        input
        |> split "\n"
        |> Seq.mapi parseLine
        |> Seq.concat

    let toWord cells = cells |> Seq.map (fun c -> c.letter) |> System.String.Concat
    let toLocation cell = sprintf "(%i,%i)" cell.x cell.y
    let toPath cells = cells |> Seq.map toLocation |> String.concat ","
    let toAnswer cells = sprintf "%s: %s" (toWord cells) (toPath cells)
    let matches word cells = word = toWord cells

    let find cells word =
        let firstChar = Seq.head word
        let starts = cells |> Seq.filter (fun c -> c.letter = firstChar)

        let findNthCell start i = cells |> Seq.find (fun c -> c.x = start.x + i && c.y = start.y)

        let makeCandidate start =
            Seq.initInfinite (findNthCell start)
            |> Seq.take (Seq.length word)

        let candidates = starts |> Seq.map makeCandidate

        candidates |> Seq.find (matches word)


    let solve (input: string) = ""

