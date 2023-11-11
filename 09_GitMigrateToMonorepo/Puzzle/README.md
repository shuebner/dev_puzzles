# Setting

Until now, you had had a mixed approach with regards to monorepo vs multirepo.
A decision was made to go full monorepo.

There are two exemplary source repositories.
Each has a single library in it.
Both repositories have a rich commit history with merged feature branches and tags.

The repositories are:
* https://github.com/shuebner/dev_puzzle_lib1
* https://github.com/shuebner/dev_puzzle_lib2

# Objective

Write a script that creates a new repository "Monorepo" that contains both Lib1 and Lib2.
The following acceptance criteria need to be fulfilled:

1. Each source repo MUST get its own subfolder in Monorepo, i. e. subfolders "Lib1" and "Lib2".
1. Commit histories MUST be migrated for both subfolder and individual files. Commit SHAs MAY change.
1. Tags MUST be migrated. To avoid duplicate tags, rename them. Lib1's tags should be prefixed with `"lib1_"`, Lib2's tags should be prefixed with `"lib2_"`.
1. There MUST NOT be any changes to the original repositories.

# Hint

The script can leverage [git filter-repo](https://github.com/newren/git-filter-repo) (introduced in puzzle 06 GitBatching).

If you get stuck or don't know where even to begin, have a look at the [Hints folder](../Hints).