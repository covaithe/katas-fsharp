
## Greg's katas in F#

This repo is to store various kata implementations in F#.

This branch is the starting point for working on the word search kata from here:
https://github.com/PillarTechnology/kata-word-search

It has a failing acceptance test.

To run the tests, do this:

```
dotnet test
```

or, if you prefer to run the tests continuously:

```
dotnet watch test --project WordSearch.Tests
```

## 2021-05-10

### Plans

Not sure. Last time I did a thing with including the full problem output as the find fn's return value, e.g "SULU: (1,1),(2,2)" etc. I'm not sure I liked that. It made the tests more cumbersome to write, and made it feel like the solution was too artificially constrained to this problem. I wouldn't write a real-world software package like that; I'd return data and allow the presentation layer to format the data in the desired way.

So maybe this time I return data (Cell seq) from the thing, and look for ways to make the tests work nicely with that? Sure, why not.


### Reflections

Didn't finish, and several days have passed since I worked on it, but... I remember the approach working nicely. I built toWord, toPath, and toAnswer methods early, and used them freely in the tests. I think I liked the results.