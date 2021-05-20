
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

## kata start date goes here

### Plans

Gonna try returning data from the find function again, and using various projection functions, e.g. toWord, to convert the data to some more convenient format for assertion or printing or whatever. I think this is what I did last time and I liked it, so it's worth another go.

### Reflections

Idk... it seemed okay, but the toWord and all that seemed less relevant than I remembered. I mostly used toPath in the tests. I think overall it's worth doing, but maybe not as important to the solution as I first thought. The top-level solve method did come out neater and more easily though.