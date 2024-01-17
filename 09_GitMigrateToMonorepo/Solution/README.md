# Solution

As mentioned in the hints, the solution involves `git filter-repo`.
Be sure to check out the source repositories with either `--mirror` or `--bare` option to tell `filter-repo` that it is working on a copy and can thus safely rewrite history.

The important command to use on each source repo is then
```sh
git filter-repo --to-subdirectory-filter Lib1 --tag-rename v:lib1_v
```
to move everything through the entire repo history into the `Lib1` folder and prefix all all tags starting with `v` to `lib1_v`.

After that, merge the source repos into the new monorepo with
```sh
git merge --allow-unrelated-histories RepoLib1/main
```
which leads you to the target structure:

![image](https://github.com/shuebner/dev_puzzles/assets/1770684/f2169940-5fa8-4c52-9048-3253e6a0f547)

# Git Subtree? Not quite a Solution

At first glance, using `git subtree` also fits the bill, i. e. in the fresh monorepo directory:
```sh
git subtree add --prefix Lib1 https://github.com/shuebner/dev_puzzle_lib1 main
git subtree add --prefix Lib2 https://github.com/shuebner/dev_puzzle_lib2 main
```
This will lead to the correct folder structure in the file system.
Even the history of the monorepo directory will show everything we want.
The caveat is that there is no history of subfolders or files.

In contrast, `git filter-repo` preserves file history, which is why it is the preferred solution.