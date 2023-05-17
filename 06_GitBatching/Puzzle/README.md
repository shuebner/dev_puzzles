# Setting

You have implemented a feature on a feature branch of [this GIT repository](https://github.com/shuebner/dev_puzzle_06_source).
You made granular atomic commits and thus have ~20 commits on your feature branch.
You were so focused on your work that you forgot to follow your team's git commit message policy.

The policy demands that each commit message be prefixed with a feature id.
The feature id of the feature that you implemented is "ABC-123".
A commit message of
```
heyho

did this and that
```

is invalid and should be:
```
ABC-123: heyho

did this and that
```

# Objective
Change all commit messages on your feature branch `feature_1_1` so that they are valid against the policy.
This means applying the prefix `"ABC-123: "` (without the quotes) to each one.
Mind multi-line commit messages.

# Hint

With the right command line git tool this can be done with a single line and a small config file.
The tool is not installed by default.

For trying things out, you can start with the `get_started.sh` script and add the renaming.