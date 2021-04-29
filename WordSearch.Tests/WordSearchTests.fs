module Tests

    open System
    open Xunit
    open Kata.WordSearch

    let dedent (s : string) =
        s
        |> (fun s -> s.Split("\n"))
        |> Array.map (fun s -> s.Trim())
        |> Array.filter (fun s -> not (String.IsNullOrEmpty(s)))
        |> (fun strings -> String.Join('\n', strings))

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
    let ``parse should return a sequence of cells``() =
        let input = dedent """
            a,b
            c,d
        """
        let expectedCells = [
            {x=0; y=0; letter='a'}
            {x=1; y=0; letter='b'}
            {x=0; y=1; letter='c'}
            {x=1; y=1; letter='d'}
        ]
        Assert.Equal(expectedCells, parse input)

    [<Fact>]
    let ``find should show the locations of the cells``() =
        let cells = parse "a,b,c,d"
        Assert.Equal("(1,0)", find cells "b")

    [<Fact>]
    let ``find should locate longer words``() =
        let cells = parse "a,b,c,d"
        Assert.Equal("(1,0),(2,0)", find cells "bc")

    [<Fact>]
    let ``find should work on any row``() =
        let cells = parse "a,b\nc,d"
        Assert.Equal("(0,1)", find cells "c")

    [<Fact>]
    let ``find should locate words from any matching cell``() =
        let cells = parse "a,b,b,b,b,c,d"
        Assert.Equal("(4,0),(5,0)", find cells "bc")

    [<Theory>]
    [<InlineData("c1", "(1,1),(0,0)")>]
    [<InlineData("c2", "(1,1),(1,0)")>]
    [<InlineData("c3", "(1,1),(2,0)")>]
    [<InlineData("c4", "(1,1),(0,1)")>]
    [<InlineData("c5", "(1,1),(2,1)")>]
    [<InlineData("c6", "(1,1),(0,2)")>]
    [<InlineData("c7", "(1,1),(1,2)")>]
    [<InlineData("c8", "(1,1),(2,2)")>]
    let ``find should work in all directions`` word answer =
        let input = dedent """
            1,2,3
            4,c,5
            6,7,8
        """
        let cells = parse input
        Assert.Equal(answer, find cells word)



