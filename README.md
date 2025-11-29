# Unity + Git Collaboration Guide (VS Code Workflow)
## HOW TO PULL FROM GITHUB (Update Unity Project)
**ALWAYS do this before opening Unity.**

### In VS Code:
1. Open your Unity project folder  
2. Open the terminal inside VS Code (**Ctrl + `**)  
3. Run:

```bash
git pull
```

If it says `"Already up to date"` → good.  
Now open Unity.

Unity will refresh assets and load all updates.

---

## HOW TO PUSH YOUR UNITY CHANGES TO GITHUB
**Do this after you finish working in Unity.**

### Step 1 — Save everything in Unity
- Save Scenes (**Ctrl + S**)  
- Save Project (**File → Save Project**)  

### Step 2 — Go back to VS Code terminal and run:

#### 1️ Stage changes:
```bash
git add .
```

#### 2️ Commit:
```bash
git commit -m "Describe your changes here"
```

Example commit message:  
`"Added player movement script"`

#### 3️ Push:
```bash
git push
```

You just uploaded your Unity changes and teammates can now pull them  

---

# THE OFFICIAL UNITY + GIT WORKFLOW WITH VS CODE 

## Before you open Unity:
```bash
git pull
```

## After you finish working in Unity:
```bash
git add .
git commit -m "Your message"
git push
```

---
### 🔹 Always pull BEFORE opening Unity  
Prevents merge conflicts + corrupted meta files.

### 🔹 Always commit + push when done  
Keeps the team in sync.

### 🔹 Don’t edit the same scene at the same time  
Use **prefabs** or **separate scenes** so multiple people can work at once.

---
