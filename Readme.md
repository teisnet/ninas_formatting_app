# App for formatting Nina's csv-formatted email subscription list
Converts the fullname field to a firstname and a lastname field.
See 'testdata_expected_output.csv' for examples.

SolutionDir/Data/ is set as the current working directory

## NOTE: Nina's original source data has been deleted from this repo
The files *nina_data_output.csv* and *nina_source_data.csv* has been removed from this repo using the following commands commands:
```
$ git filter-branch --force --index-filter "git rm --cached --ignore-unmatch Data/nina_source_data.csv"  --prune-empty --tag-name-filter cat -- --all

$ git for-each-ref --format="delete %(refname)" refs/original | git update-ref --stdin

$ git reflog expire --expire=now --all

$ git gc --prune=now
```
See https://help.github.com/en/articles/removing-sensitive-data-from-a-repository

---

Teis Draiby | teis@teis.net | 2019
