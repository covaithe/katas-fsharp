
== Greg's katas in F# ==

This repo is to store various kata implementations in F#.

This branch is for working on the word search kata from here:
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

This branch I started on 2021-03-31. My goal on this run through the
kata is I guess two main things: first to move more fluidly through, with
somewhat less googling than last time, and second to have a slightly
more functional approach. I think I need smaller functions composed more. Maybe.

Reflections after finishing:

I certainly like this better than my first attempt. I think I did better
at making small functions with good names and gluing them together to
get things done. Making a functional-friendly `split` function early was
definitely good. Partial function application for the win!

There's a concept from... some book; maybe Clean Code?
...that suggests that all the lines in a function should be at the same level
of abstraction. I think there might be a similar principle to be discovered
in functional pipelines, e.g. it may be bad style to combine lambdas and
method calls. Or maybe it's just that lambdas are a bit sus? IDK. I do know
that in this run, I leaned away from lambdas and more towards named functions,
and I liked the outcome. Maybe it's just that a good name is very helpful at
revealing intention? Something to keep an eye on.

I also liked the infinite sequence generator for cellVector. IDK if that's a
good name, but I love the idea that a start and a direction is enough to
specify a sequence of cells of arbitrary length, and then we just `Seq.take word.Length`. That feels way better than mapping over the chars in the word.