using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character
    /// words (lower case, no duplicates). Using sets, find an O(n)
    /// solution for returning all symmetric pairs of words.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var pairs = new List<string>();
        var seen = new HashSet<string>();

        foreach (var word in words)
        {
            // Ensure the word is 2 characters and doesn't have duplicate letters (e.g., "aa")
            if (word.Length == 2 && word[0] != word[1])
            {
                string reversed = new string(new char[] { word[1], word[0] });
                
                if (seen.Contains(reversed))
                {
                    pairs.Add($"{reversed} & {word}");
                }
            }
            seen.Add(word);
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        
        foreach (var line in File.ReadLines(filename))
        {
            // FIXED: Changed Split(",") to Split(',') to fix a C# syntax error in the original template
            var fields = line.Split(',');
            
            // Column 4 is at index 3
            if (fields.Length >= 4)
            {
                string degree = fields[3].Trim();
                
                if (!string.IsNullOrWhiteSpace(degree))
                {
                    if (degrees.ContainsKey(degree))
                    {
                        degrees[degree]++;
                    }
                    else
                    {
                        degrees[degree] = 1;
                    }
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Clean the strings: remove spaces and ignore case
        string clean1 = word1.Replace(" ", "").ToLower();
        string clean2 = word2.Replace(" ", "").ToLower();

        // If lengths differ after cleaning, they can't be anagrams
        if (clean1.Length != clean2.Length)
        {
            return false;
        }

        var charCounts = new Dictionary<char, int>();

        // Count characters in the first word (using index notation as hinted)
        for (int i = 0; i < clean1.Length; i++)
        {
            char c = clean1[i];
            if (charCounts.ContainsKey(c))
            {
                charCounts[c]++;
            }
            else
            {
                charCounts[c] = 1;
            }
        }

        // Decrement counts using the second word (using index notation)
        for (int i = 0; i < clean2.Length; i++)
        {
            char c = clean2[i];
            if (!charCounts.ContainsKey(c))
            {
                return false;
            }
            
            charCounts[c]--;
            
            if (charCounts[c] < 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON data from the USGS consisting of earthquake data.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var results = new List<string>();
        
        if (featureCollection?.features != null)
        {
            foreach (var feature in featureCollection.features)
            {
                if (feature?.properties != null)
                {
                    results.Add($"{feature.properties.place} - Mag {feature.properties.mag}");
                }
            }
        }
        
        return results.ToArray();
    }
}
