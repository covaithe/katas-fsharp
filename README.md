
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

Going for smoothness and easy flow from step to step in this version. Ideally I'd like to get it done without any struggling.

### Reflections

Not bad. I got through the whole thing in about 45 minutes, not rushing, while half paying attention to a meeting. I did make one small mistake, concating the cell sequences at the wrong time. In candidatesAt after line 59; it ought to be at the end of line 61. This is a mistake I've made several times in the past, and in the past it took some difficult debugging. Today I remembered that this is one of the things that can go wrong at this step, and was able to find it by thinking about the type signatures of the inner functions. I wonder if F# has type aliases? if `seq<Cell>` had a friendly name, e.g. Candidate, it might be easier to understand this.

Also, I badly need some editor templates to build the tests for me. They're tricky and annoying to build by hand.
