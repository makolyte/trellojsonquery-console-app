# Trello JSON query app

I wanted to get all of the names of cards in specific lists in my Trello board. So I exported the board as JSON (from the Trello UI), then wrote this utility that parses JSON to Lists and Cards. It outputs the Card names to a CSV file from a specific List.

This is v1. It's a simple starting point that solves a specific problem (show me card names for this list).

# Example input/output
Version 1 - Loads Trello JSON from hardcoded path, gets lists and cards, then displays card names for list you pick

Loading Trello JSON
Found 10 list(s). 2 active, 8 closed.

Here are the 2 active lists:
Todo
Done

Show cards for list (enter exact name): Todo
Output query results to: C:\temp\trello-query-133284861454721501.csv
