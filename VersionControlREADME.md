 Layout

master   0---------0----------0
          \   \   /           |
hotfix     \   -0-            |
            \    \            |
release      \    \        0--0
              \    \      /    \
dev            -0-0-0----0------0
                |  \    /
feature1        |   0--0 
                |
feature2        0-0-0-0


master
The master branch is the version of the application that is currently being used
in the experiment. There is only one master branch.

deployment (Deployment)
The deployment branch is the branch that will be pushed to the master branch
when there is a working version. There is only one deployment branch.

dev (Development)
The dev branch is the branch that all feature branches are pushed to. There is
only one dev branch.

feature
A feature branch is a branch that is used to work on a specific feature. There
can be multiple feature branches at a given time. 

hotfix
The hotfix branch is the branch that will be used if there is a bug or error in
the master branch that does not require a new feature branch. There is only one
hotfix branch.

Style Guide

