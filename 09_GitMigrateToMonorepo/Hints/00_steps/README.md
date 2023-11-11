# Suggested Steps

## 1. Create a throw-away clone of the lib repositories
Create a fresh local clone of both source repositories. This avoids damaging the original repository.
Use the `--bare` or `--mirror` option, so that `git filter-repo` can see that it is a non-critical copy of the original.
Otherwise `git filter-repo` will give you a warning and (rightfully) refuse to touch the repo unless you `--force` it.

Look at the history.
Note the feature branches, merge commits and tags.
Keep the history open to compare with the rewritten history after the next step and with the monorepo's history at the end.

## 2. Move their history into a subfolder
Use `git filter-repo` to rewrite the repo history.

1. It should look like all repo contents have always been in a subdirectory instead of the repo root.
1. All tags must be renamed, i. e. prefixed as described in the objectives.

Fortunately, this is a one-liner.

Look at the history now.
Compare with the original history.

## 3. Create a new Monorepo
Just create an empty repository locally.
This will be the home of the new monorepo.
There need not be any commit

## 4. Add the local lib repositories as remotes
Use `git remote add` to add the local lib repos (with the rewritten history) as remotes.
Then `git fetch` them.

## 5. Merge the local lib repositories into the Monorepo
Use `git merge` to merge the `HEAD` (which should be `main`) of lib 1 into the `main` branch of the Monorepo.

Do the same for lib 2.

**CAVEAT**: the histories of lib2 and the monorepo are unrelated.
By default, merging will leave the history of lib2 in parallel with the one of the monorepo.
You can still see the tags, but the main branch of the monorepo will not show anything from lib2, not even the merge you just made. For the desired result, you thus need to tell git to merge the unrelated histories.

You can remove the local lib repos as remotes now.

## 6. Check the result

Look at the history of the resulting monorepo.
You should see both the lib's full histories, including their tags.
Compare with the histories of the source repos to be sure.