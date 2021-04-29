
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

## 2021-04-06

### Plans

Last time through the kata felt like it was getting better.
I still fumbled with F# a fair amount, but less than before,
and the outcome was better, eventually. This time I think
more of the same: try to use F# idiomatically as far as I understand
what that means (not very far...), and just try to get smoother
and less error-prone at the kata.

### Reflections

Going well. I feel like F# is coming more easily with repetition.
Which is not surprising I suppose, but still gratifying.

I'm still struggling a little with type annotations, but I think
that might be partly because I have two types with very
similar signatures in Cell and Direction. Finding a way to
combine them might help, or just accepting that this kata might
need more type signature specifications than most problems.

The other thing on my mind is where to put the functions. I think
I'm getting better at (or at least more willing to attempt) lots
of small, simple functions, but when do I leave them as local "child"
functions and when do I "promote" them to exist outside? I suppose in
a sense local functions are "private" in that they aren't visible
from outside the fn they are declared in, which

