# Game

Workflow:

Updating your repo from master:
  git checkout master
  git pull
  git checkout your-branch-name
  git merge master
  
When you're committing something (still on your own branch):
  git add -u .
  git commit -m description-of-commit-here
  git push origin your-branch-name
  
Then go to github and create a pull request between your branch and master. Before pushing, you may want to use <code>git status</code> to make sure that the right files were added to your commit
  
