using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class StringReplacementHandler
{
    private readonly IConfiguration _configuration;

    // Constructor to inject the connection string
    public StringReplacementHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Main function to replace strings in the file based on mappings and thresholds
    public string ReplaceStringsInFile(string filePath, List<string> names, string tuningId)
    {
        // Step 1: Read the content of the file as binary.
        byte[] fileContentBytes = File.ReadAllBytes(filePath); // Use binary file content

        // Step 2: Get mappings from SolutionMappings based on tuningId.
        var solutionMappings = GetSolutionMappings(tuningId, names);

        // Step 3: Loop through the solution mappings.
        foreach (var mapping in solutionMappings)
        {
            // Step 4: Get ReplacementStrings based on the mapping's ReplacementId.
            var replacementCommands = GetReplacementCommands(mapping.ReplacementId);

            // Step 5: For each replacement command, check if SearchString exists in the file and meets threshold.
            foreach (var command in replacementCommands)
            {
                var searchBytes = System.Text.Encoding.UTF8.GetBytes(command.SearchString);

                // Find the search string in the file with threshold similarity.
                var matchingPositions = GetMatchingPositions(fileContentBytes, searchBytes, command.Threshold);

                // Step 6: Replace the matches found
                foreach (var position in matchingPositions)
                {
                    // Replace the matched binary sequence with ReplaceString at the identified positions
                    fileContentBytes = ReplaceAtPosition(fileContentBytes, position, command.ReplaceString);
                }
            }
        }

        // Step 7: Save the modified content back to the file.
        string outputFilePath = filePath + "_modded";
        File.WriteAllBytes(outputFilePath, fileContentBytes);

        // Return the modified content as a string (optional)
        return System.Text.Encoding.UTF8.GetString(fileContentBytes);
    }

    // Find matching positions where the SearchString almost matches in the binary content
    private List<int> GetMatchingPositions(byte[] contentBytes, byte[] searchBytes, int threshold)
    {
        var matchingPositions = new List<int>();

        // Loop through content and find positions where matches are above the threshold
        for (int i = 0; i <= contentBytes.Length - searchBytes.Length; i++)
        {
            // Extract a window from the content
            byte[] window = new byte[searchBytes.Length];
            Array.Copy(contentBytes, i, window, 0, searchBytes.Length);

            // Calculate the similarity between the window and the search string
            double matchPercentage = CalculateBinaryMatchPercentage(window, searchBytes);

            // If match percentage meets or exceeds the threshold, record the position
            if (matchPercentage >= threshold)
            {
                matchingPositions.Add(i);
            }
        }

        return matchingPositions;
    }

    // Replace content at the specific position with ReplaceString
    private byte[] ReplaceAtPosition(byte[] contentBytes, int position, string replaceString)
    {
        byte[] replaceBytes = System.Text.Encoding.UTF8.GetBytes(replaceString);
        int replaceLength = replaceBytes.Length;

        // Create a new byte array for the result
        byte[] modifiedContent = new byte[contentBytes.Length];

        // Copy all content before the replacement position
        Array.Copy(contentBytes, 0, modifiedContent, 0, position);

        // Replace the bytes at the given position
        Array.Copy(replaceBytes, 0, modifiedContent, position, Math.Min(replaceLength, contentBytes.Length - position));

        // Copy remaining content after the replaced section
        if (position + replaceLength < contentBytes.Length)
        {
            Array.Copy(contentBytes, position + replaceLength, modifiedContent, position + replaceLength, contentBytes.Length - (position + replaceLength));
        }

        return modifiedContent;
    }

    // Calculate the similarity between two binary byte arrays
    private double CalculateBinaryMatchPercentage(byte[] contentBytes, byte[] searchBytes)
    {
        if (contentBytes.Length != searchBytes.Length)
        {
            return 0; // Only compare arrays of the same length
        }

        int matchingBytes = 0;

        // Compare byte-by-byte and count the matches
        for (int i = 0; i < contentBytes.Length; i++)
        {
            if (contentBytes[i] == searchBytes[i])
            {
                matchingBytes++;
            }
        }

        // Calculate match percentage based on the number of matching bytes
        double matchPercentage = ((double)matchingBytes / contentBytes.Length) * 100;
        return matchPercentage;
    }

    // Helper function to retrieve solution mappings from the database
    private List<SolutionMapping> GetSolutionMappings(string tuningId, List<string> names)
    {
        var mappings = new List<SolutionMapping>();

        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            string query = @"
                SELECT MappingId, ReplacementId
                FROM SolutionMappings
                WHERE TuningSpecialInfoId = @TuningId AND Name IN (@Names)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TuningId", tuningId);
            command.Parameters.AddWithValue("@Names", string.Join(",", names));

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                mappings.Add(new SolutionMapping
                {
                    MappingId = reader.GetInt32(0),
                    ReplacementId = reader.GetInt32(1)
                });
            }
        }

        return mappings;
    }

    // Helper function to retrieve replacement commands from the database
    private List<ReplacementCommand> GetReplacementCommands(int replacementId)
    {
        var commands = new List<ReplacementCommand>();

        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            string query = @"
                SELECT ReplacementId, SearchString, ReplaceString, Threshold, Action, Description
                FROM ReplacementStrings
                WHERE ReplacementId = @ReplacementId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReplacementId", replacementId);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                commands.Add(new ReplacementCommand
                {
                    ReplacementId = reader.GetInt32(0),
                    SearchString = reader.GetString(1),
                    ReplaceString = reader.GetString(2),
                    Threshold = reader.GetInt32(3),
                    Action = reader.GetInt32(4),
                    Description = reader.IsDBNull(5) ? null : reader.GetString(5)
                });
            }
        }

        return commands;
    }

    // Find matching positions where the SearchString almost matches in the binary content
    private List<int> GetMatchingPositions(byte[] contentBytes, string searchString, int threshold)
    {
        byte[] searchBytes = System.Text.Encoding.UTF8.GetBytes(searchString);
        var matchingPositions = new List<int>();

        // Loop through content and find positions where matches are above the threshold
        for (int i = 0; i <= contentBytes.Length - searchBytes.Length; i++)
        {
            int currentMatchCount = 0;

            // Compare bytes at the current position with search string
            for (int j = 0; j < searchBytes.Length; j++)
            {
                if (contentBytes[i + j] == searchBytes[j])
                {
                    currentMatchCount++;
                }
            }

            // Calculate the match percentage
            double matchPercentage = ((double)currentMatchCount / searchBytes.Length) * 100;

            // If match percentage meets or exceeds the threshold, record the position
            if (matchPercentage >= threshold)
            {
                matchingPositions.Add(i);
            }
        }

        return matchingPositions;
    }
}

// Models to represent the database entities
public class SolutionMapping
{
    public int MappingId { get; set; }
    public int ReplacementId { get; set; }
}

public class ReplacementCommand
{
    public int ReplacementId { get; set; }
    public string SearchString { get; set; }
    public string ReplaceString { get; set; }
    public int Threshold { get; set; }
    public int Action { get; set; }
    public string Description { get; set; }
}
