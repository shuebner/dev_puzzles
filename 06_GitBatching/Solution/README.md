# Solution

The tool to use is [git filter-repo](https://github.com/newren/git-filter-repo).
Although git comes with a similar tool [git filter-branch](https://git-scm.com/docs/git-filter-branch), the GIT people themselves advise against using it and recommend using git filter-repo instead.

## git filter-repo

Git filter-repo works directly on the git database.
It does not need to check out anything and is thus blazingly fast, even on big repos and lots of rewrites.
It will complete in seconds or in this particular puzzle, in a fraction of a second.
It is also less error-prone than filter-branch and gives better error messages in case of failure.

There are different approaches to the solution.

### Replace Message Regex

The suggested solution uses the most specific tool for the job: `--replace-message` along with a regex.
This has the advantage that unwanted side-effects are impossible.
The only thing you need to know is the [python regex syntax](https://docs.python.org/3/library/re.html#regular-expression-syntax).

### Generic Callback

Another solution uses a more generic approach that relies on a callback:
```bash
git filter-repo --commit-callback 'commit.message = ("ABC-123: " + commit.message.decode("utf-8")).encode("utf-8")' --refs origin/main..HEAD 
```
However, with great power comes great responsibility.
Unwanted side-effects could happen.
You need to take great care when writing the callback, and also need to know about the available properties and quirks of python.

## (NON-ADVISED) git filter-branch

I will admit that for this particular use-case there is not much of a difference between filter-branch and filter-repo, depending on how you approach the solution.
That is why I will document a filter-branch solution here as well.

A (non-advised) filter-branch solution could be written like this:
```bash
git filter-branch -f --msg-filter 'printf "ABC-123: " && cat' origin/main..HEAD
```

Note that this simple operation will already take 4 seconds.
The reason is that filter-branch needs to check out each commit to rewrite it.
If you have a bigger repo and a lot of commits to rewrite, the command will easily take minutes to complete.

filter-branch is error-prone and will not help you when things go wrong.