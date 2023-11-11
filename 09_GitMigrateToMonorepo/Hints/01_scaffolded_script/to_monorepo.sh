#! /usr/bin/bash

### TODOs are marked with three hashes ###

rm -rf RepoLib1_temp
git clone https://github.com/shuebner/dev_puzzle_lib1 RepoLib1_temp --bare
cd RepoLib1_temp
### TODO ######################################
# use git filter-repo to rewrite the history such that
# 1. all files are moved one folder-level deeper into a "Lib1" directory
# 2. the tags are renamed with the required "lib1_" prefix
###############################################
cd ..

rm -rf RepoLib2_temp
git clone https://github.com/shuebner/dev_puzzle_lib2 RepoLib2_temp --bare
cd RepoLib2_temp
### TODO ######################################
# use git filter-repo to rewrite the history such that
# 1. all files are moved one folder-level deeper into a "Lib2" directory
# 2. the tags are renamed with the required "lib2_" prefix
###############################################
cd ..

rm -rf Monorepo
mkdir Monorepo

cd Monorepo
git init .

git remote add RepoLib1 ../RepoLib1_temp
git fetch RepoLib1
### TODO ######################################
# merge the main branch of the remote "RepoLib1" that was just added
# into the (currently checked-out) main branch of this repo (the Monorepo)
###############################################
git remote remove RepoLib1

git remote add RepoLib2 ../RepoLib2_temp
git fetch RepoLib2
### TODO ######################################
# merge the main branch of the remote "RepoLib2" that was just added
# into the (currently checked-out) main branch of this repo (the Monorepo)
###############################################
git remote remove RepoLib2