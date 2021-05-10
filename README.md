
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

I had a thought last time to make find return the answer more in the form of the expected output. Probably not much different but worth at try?

### Reflections

Kata as a whole is going nicely. Feels smooth and pretty clean. I'd like it to be a little faster but probably not worth pushing too hard. I'm down to about 30 minutes now, and going much faster will probably involve cutting some corners.

Definitely need more practice at carefully refactoring towards directions. I think it's a good approach, but it's a bit challenging to do cleanly.

Not sure about including the word in the output from find. It does clean up the solve method at the end, but it clutters up the tests. Maybe an intermediate function that wraps find with a formatter? but then I'd be tempted to inline that back into the solve function....