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

![image](https://github.com/shuebner/dev_puzzles/assets/1770684/e3f789bf-70c9-4990-9868-6766d005ad7b)