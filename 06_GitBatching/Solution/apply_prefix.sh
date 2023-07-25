#! /usr/bin/bash

git clone https://github.com/shuebner/dev_puzzle_06_source.git source
cd source

# apply commit message changes to the feature/1_1 branch
git filter-repo --replace-message ../message_changes.txt --refs origin/main..feature/1_1