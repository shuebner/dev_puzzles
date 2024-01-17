#! /usr/bin/bash

# create a fresh clone to rewrite history before merging
rm -rf RepoLib1_temp
git clone https://github.com/shuebner/dev_puzzle_lib1 RepoLib1_temp --bare
cd RepoLib1_temp
git filter-repo --to-subdirectory-filter Lib1 --tag-rename v:lib1_v
cd ..

# create a fresh clone to rewrite history before merging
rm -rf RepoLib2_temp
git clone https://github.com/shuebner/dev_puzzle_lib2 RepoLib2_temp --bare
cd RepoLib2_temp
git filter-repo --to-subdirectory-filter Lib2 --tag-rename v:lib2_v
cd ..

rm -rf Monorepo
mkdir Monorepo

cd Monorepo
git init .

git remote add RepoLib1 ../RepoLib1_temp
git fetch RepoLib1
git merge --allow-unrelated-histories RepoLib1/main
git remote remove RepoLib1

git remote add RepoLib2 ../RepoLib2_temp
git fetch RepoLib2
git merge --allow-unrelated-histories RepoLib2/main
git remote remove RepoLib2