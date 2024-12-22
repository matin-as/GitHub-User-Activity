### README

# GitHub Activity CLI

A simple command-line interface (CLI) tool to fetch and display the recent activity of a GitHub user. This project uses the GitHub API to retrieve activity data and displays it in the terminal.

---

## Features
- Fetches recent GitHub activity for a specified user.
- Displays event details like:
  - Pushing commits
  - Starring repositories
  - Opening issues
  - Forking repositories
- Handles errors gracefully for invalid usernames or API issues.

---

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) installed on your system.

---

## How to Use

1. **Clone the Repository**  

2. **Build the Project**  
   ```bash
   dotnet build
   ```

3. **Run the CLI Tool**  
   Provide the GitHub username as a command-line argument:  
   ```bash
   dotnet run <username>
   ```

   Example:
   ```bash
   dotnet run kamranahmedse
   ```

4. **Output Example**  
   ```bash
   Fetching activity for user: kamranahmedse...
   Recent Activity:
   - Pushed commits to kamranahmedse/developer-roadmap
   - Starred kamranahmedse/developer-roadmap
   - Opened a new issue in kamranahmedse/developer-roadmap
   ```

---

## Error Handling
- If no activity is found, the tool displays:  
  `No recent activity found for this user.`
- For invalid usernames or API issues, the tool displays an error message.
