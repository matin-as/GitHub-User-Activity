using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHubActivityCLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: github-activity <username>");
                return;
            }

            string username = args[0];
            string apiUrl = $"https://api.github.com/users/{username}/events";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Add a User-Agent header as required by GitHub API
                    client.DefaultRequestHeaders.Add("User-Agent", "GitHubActivityCLI");

                    Console.WriteLine($"Fetching activity for user: {username}...");
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        JsonDocument jsonDocument = JsonDocument.Parse(responseBody);

                        var events = jsonDocument.RootElement.EnumerateArray();

                        bool hasActivity = false;

                        Console.WriteLine("Recent Activity:");
                        foreach (var evt in events)
                        {
                            hasActivity = true;
                            string type = evt.GetProperty("type").GetString();
                            string repoName = evt.GetProperty("repo").GetProperty("name").GetString();

                            string activityDescription = type switch
                            {
                                "PushEvent" => $"Pushed commits to {repoName}",
                                "IssuesEvent" => evt.GetProperty("payload").GetProperty("action").GetString() == "opened"
                                    ? $"Opened a new issue in {repoName}"
                                    : $"Updated an issue in {repoName}",
                                "WatchEvent" => $"Starred {repoName}",
                                "ForkEvent" => $"Forked {repoName}",
                                _ => $"Performed {type} on {repoName}"
                            };

                            Console.WriteLine("- " + activityDescription);
                        }

                        if (!hasActivity)
                        {
                            Console.WriteLine("No recent activity found for this user.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: Unable to fetch activity (HTTP {response.StatusCode})");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
