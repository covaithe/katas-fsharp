module Tests

    open System
    open Xunit
    open Kata.WordSearch

    let dedent (s : string) =
        s
        |> (fun s -> s.Split("\n"))
        |> Array.map (fun s -> s.Trim())
        |> Array.filter (String.IsNullOrEmpty >> not)
        |> String.concat "\n"

    // As far as I can tell, the simplest way to run just a single
    // test in xunit is to add this attribute to the test, and then
    // run `dotnet run --filter Category=wtf`
    // [<Trait("Category", "wtf")>]

    [<Fact>]
    let ``solve should print the solution`` () =
        let input = dedent """
            BONES,KHAN,KIRK,SCOTTY,SPOCK,SULU,UHURA
            U,M,K,H,U,L,K,I,N,V,J,O,C,W,E
            L,L,S,H,K,Z,Z,W,Z,C,G,J,U,Y,G
            H,S,U,P,J,P,R,J,D,H,S,B,X,T,G
            B,R,J,S,O,E,Q,E,T,I,K,K,G,L,E
            A,Y,O,A,G,C,I,R,D,Q,H,R,T,C,D
            S,C,O,T,T,Y,K,Z,R,E,P,P,X,P,F
            B,L,Q,S,L,N,E,E,E,V,U,L,F,M,Z
            O,K,R,I,K,A,M,M,R,M,F,B,A,P,P
            N,U,I,I,Y,H,Q,M,E,M,Q,R,Y,F,S
            E,Y,Z,Y,G,K,Q,J,P,C,Q,W,Y,A,K
            S,J,F,Z,M,Q,I,B,D,B,E,M,K,W,D
            T,G,L,B,H,C,B,E,C,H,T,O,Y,I,K
            O,J,Y,E,U,L,N,C,C,L,Y,B,Z,U,H
            W,Z,M,I,S,U,K,U,R,B,I,D,U,X,S
            K,Y,L,B,Q,Q,P,M,D,F,C,K,E,A,B
        """

        let answer = solve input
        let expected = dedent """
            BONES: (0,6),(0,7),(0,8),(0,9),(0,10)
            KHAN: (5,9),(5,8),(5,7),(5,6)
            KIRK: (4,7),(3,7),(2,7),(1,7)
            SCOTTY: (0,5),(1,5),(2,5),(3,5),(4,5),(5,5)
            SPOCK: (2,1),(3,2),(4,3),(5,4),(6,5)
            SULU: (3,3),(2,2),(1,1),(0,0)
            UHURA: (4,0),(3,1),(2,2),(1,3),(0,4)
        """

        Assert.Equal(expected, answer)

    [<Fact>]
    let ``parse should return a seq of cells``() =
        let puzzle = dedent """
            a,b
            c,d
        """
        let expectedCells = [
            {letter='a'; x=0; y=0}
            {letter='b'; x=1; y=0}
            {letter='c'; x=0; y=1}
            {letter='d'; x=1; y=1}
        ]
        Assert.Equal<Cell seq>(expectedCells, parse puzzle)

    let sampleSequence = [
        {letter='a'; x=0; y=0}
        {letter='b'; x=1; y=0}
        {letter='c'; x=0; y=1}
    ]
    [<Fact>]
    let ``toWord should concat the letters of a cell sequence``() =
        Assert.Equal("abc", toWord sampleSequence)

    [<Fact>]
    let ``toPath should return the formatted coords of a sequence``() =
        Assert.Equal("(0,0),(1,0),(0,1)", toPath sampleSequence)

    [<Fact>]
    let ``toAnswer should format the word and the path of a sequence``() =
        Assert.Equal("abc: (0,0),(1,0),(0,1)", toAnswer sampleSequence)

    [<Fact>]
    let ``find should return the cell sequence of the located word``() =
        let cells = parse "a,b,c"
        let expectedCells = [
            {letter='b'; x=1; y=0}
            {letter='c'; x=2; y=0}
        ]
        Assert.Equal<Cell seq>(expectedCells, find cells "bc")

    [<Fact>]
    let ``find should locate words of any length``() =
        let cells = parse "a,b,c,d,e"
        Assert.Equal("bcde", find cells "bcde" |> toWord)

    [<Fact>]
    let ``find should locate words from any cell matching the first char of the word``() =
        let cells = parse "a,a,a,b"
        Assert.Equal("(2,0),(3,0)", find cells "ab" |> toPath)

    [<Fact>]
    let ``find should locate words starting on any line``() =
        let cells = parse "a,b\nc,d"
        Assert.Equal("(0,1),(1,1)", find cells "cd" |> toPath)

    [<Fact>]
    let ``find should not break at the edge of the grid``() =
        let cells = parse "a,a\nb,b\na,c"
        Assert.Equal("(0,2),(1,2)", find cells "ac" |> toPath)

    [<Theory>]
    [<InlineData("c1")>]
    [<InlineData("c2")>]
    [<InlineData("c3")>]
    [<InlineData("c4")>]
    [<InlineData("c5")>]
    [<InlineData("c6")>]
    [<InlineData("c7")>]
    [<InlineData("c8")>]
    let ``find should locate words in any direction`` word =
        let cells = parse (dedent """
            1,2,3
            4,c,5
            6,7,8
        """)
        Assert.Equal(word, find cells word |> toWord)
