![prizegame](https://user-images.githubusercontent.com/54157038/205478600-09fc26b0-a816-40db-9a7e-8b30c9913bac.PNG)

# PrizeGame

Encountered this game recently - the game had a large grid/board and four players (agents) along with "prizes" randomly placed around the board. The prizes each had their own int value between 1-10 and would tally their value to the collecting agent's total score once they moved into the same tile. Three agents were set up with their own algorithms to navigate the board and collect prizes as they have been assigned. The challenge set was to write a custom algorithm for your player (agent D) that will collect the highest score and win most of the time.

Here, I have rewritten the game entirely from what I could remember in that encounter. The challenge remains the same as when I first played it. 

## How to Play

- Clone the repo to your machine
- Apply the logic you see fit to `MyAgent.cs`
- Rebuild the solution (`Ctrl+Shift+B` in Visual Studio)
- Run the .exe to see the results (example in the header)

## Guidelines 

The main rule I can remember was not to outright disable the other agents and instead to compete "fairly" with them (within reason) - i.e., racing them  to prizes more effectively than their methods permitted rather than just trapping them somewhere. But, alas, agents can only occupy a single tile at a time.

Have fun and let me know what you think. Thanks!
