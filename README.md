
## Greg's katas in F#

This repo is to store various kata implementations in F#.
I'm pretty new to it (as of early 2021) but have some experience
with functional programming in other languages, so we'll see
how this goes.

Most of the actual implementations live on branches, which won't
ever be merged into main.

## This Branch

This branch holds my first attempt at the word search kata
in F#. I've done it many times in other languages, mostly
javascript, so the algorithm wasn't new to me, but these
are literally the first lines of F# I have ever written. It
took quite a while, mostly wrestling with the compiler to get
it to understand what I wanted.

The result is... not my favorite thing ever. I don't think
it reads particularly well, compared with previous implementations. Some of that may be rust on my part, but some of
it has to be my lack of familiarity with how to put together
F# programs. For example, I noticed that the compiler did
not like it when I tried to call functions defined later
in the same module; it will only call functions previously
defined. I don't really have to hide my top-level
functions at the bottom of my module, do I? There must
be patterns to make sense of this, but I don't know them yet.
