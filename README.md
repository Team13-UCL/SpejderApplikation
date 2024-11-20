# GettingReal - Jacobsens Bakery
## Accessing and working with the repo
### Clone the repo down to your desired folder
_Preferably do this while listening to Black Eyed Peas' "Let's Get It Started"._
1) Click the green "<> Code" button towards the top-right of this page
2) Copy the HTTPS link
3) On your local drive, access the folder in which you want to store the project
4) Right click blank space inside the new folder
5) Select 'Open Git Bash here' and type in the following commands...
6) Clone the remote repo to local:\
     git clone LINK
7) Access the project folder (replace GettingReal if the folder is called something else):\
     cd GettingReal

### Create your branch and access it
_In the following section, always replace BRANCH_NAME with the name you want for your own branch._
1) Create a new branch and name it appropriately (choose a brief, yet descriptive name):\
     git branch BRANCH_NAME
3) Switch to the folder to access it:\
     git switch BRANCH_NAME
4) Push branch to remote and set upstream, so it's easier for both yourself and everyone else:\
     git push -u origin BRANCH_NAME

### Commit and push changes to remote
_Congratulations, you've written some ~~great~~ code, and you're either done with it, need ~~help with~~ it review, haven't pushed in a few days, or you're going on vacation tomorrow.\
Commits should preferably be done every time you've hit some sort of breakpoint, such as a section being done.\
Pushes don't have to happen every commit, but should be often enough that others can keep reasonable track of your progress._
1) Add your changes to staging. There's a couple of ways to do this:\
     git add -A  _Adds everything_\
     git add .  _Adds everything in currently accessed folder and subfolders_\
     git add -u  _Adds all updated/changed files. This does_ **NOT** _add new files!!_\
     git add FILE_NAME.EXT  _Adds specifically FILE_NAME.EXT_
2) Commit changes. Make sure to give a descriptive message!\
     git commit -m "A perfect description of the changes made, without being too lo-"
3) Push changes\
     git push

### Accessing other branches from remote
_**Make sure your changes are committed before switching branch!**\
If you don't want your changes, use 'git stash' to store them in a corner, never to be touched again._
1) Right click blank space inside your project folder
2) Select 'Open Git Bash here' and type in the following commands...
3) First fetch the data\
     git fetch
4) Go to the desired branch (If you've accessed this branch before)\
     git switch BRANCH_NAME
5) Pull new data (If you've accessed this branch before)\
     git pull\
\
**IF YOU NEED A NEW BRANCH DOWN**\
   git checkout -b BRANCH_NAME remotes/origin/BRANCH_NAME\
(Reminder to replace BRANCH_NAME with actual the name of the actual branch)

### Merge stable branches onto master
_If A is for apple, and B is for banana, what is C for? Plastic explosive._
1) Good luck _- Martin_
