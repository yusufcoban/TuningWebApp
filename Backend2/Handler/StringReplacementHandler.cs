using Dapper;

using Microsoft.AspNetCore.Identity.UI.Services;

using System.Data.SqlClient;

namespace TuningWebApp.Handler
{
    public class StringReplacementHandler
    {
        private readonly ILogger<StringReplacementHandler> _logger;

        private readonly IConfiguration _configuration;
        private MyUploadedFileHandler _myUploadedFileHandler;


        // Constructor to inject the connection string
        public StringReplacementHandler(IConfiguration configuration, MyUploadedFileHandler myUploadedFile, ILogger<StringReplacementHandler> ilogger)
        {
            _configuration = configuration;
            _myUploadedFileHandler = myUploadedFile;
            _logger = ilogger;
        }

        public bool checkIfAllSolutionsAvailable(List<SolutionMapping> solutions, List<string> selectedTunings)
        {
            if (solutions.Any() && selectedTunings.Count() == solutions.Select(x => x.Name).Distinct().Count())
            {
                return true;
            }
            return false;
        }

        // Main function to replace strings in the file based on mappings and thresholds
        public async Task<bool> ReplaceStringsInFile(MyUploadedFile MyUploadedFile, string filePath, List<string> names, string tuningId)
        {
            _logger.LogError(MyUploadedFile.FileName + " replacement strings will be checked...");

            // Step 1: Read the content of the file as binary.
            byte[] fileContentBytes = File.ReadAllBytes(filePath); // Use binary file content
            string outputFilePath = filePath + "_modded";

            // Step 2: Get mappings from SolutionMappings based on tuningId.
            var solutionMappings = GetSolutionMappings(tuningId, names);
            _logger.LogError(solutionMappings.Count() + " automations were found for this solution...");

            if (checkIfAllSolutionsAvailable(solutionMappings, names))
            { // Step 3: Loop through the solution mappings.
                _logger.LogError("All selected solutions are available for this file request");

                foreach (var mapping in solutionMappings)
                {
                    _logger.LogError("Solution " + mapping.Name + " will be procedured...");

                    // Step 4: Get ReplacementStrings based on the mapping's ReplacementId.
                    var replacementCommands = GetReplacementCommands(mapping.ReplacementId);

                    // Step 5: For each replacement command, check if SearchString exists in the file and meets threshold.
                    foreach (var command in replacementCommands)
                    {
                        // Convert the hex SearchString to byte array
                        var searchBytes = HexStringToByteArray(command.SearchString);

                        // Find the search string in the file with threshold similarity.
                        var matchingPositions = GetMatchingPositions(fileContentBytes, searchBytes, command.Threshold);
                        _logger.LogError("Solution " + mapping.Name + " could be found on the target file...");

                        // Step 6: Replace the matches found
                        foreach (var position in matchingPositions)
                        {

                            // Replace the matched binary sequence with ReplaceString at the identified positions
                            fileContentBytes = ReplaceAtPosition(fileContentBytes, position, command.ReplaceString);
                            _logger.LogError("Solution " + mapping.Name + " step algorithm is effected...");
                        }
                    }
                    await _myUploadedFileHandler.UpdateUploadedFileStateAsync(MyUploadedFile.Id, 5, "Automation did this file..."); // set to finished
                }
                // Step 7: Save the modified content back to the file.
                File.WriteAllBytes(outputFilePath, fileContentBytes);

                //Set finished to file
                return true;
            }
            else
            {
                // set lookup 
                await _myUploadedFileHandler.UpdateUploadedFileStateAsync(MyUploadedFile.Id, 1, "Automation couldn't do this file...Free for tuner"); // set to auto lookup finished
                return false;

            }
        }

        // Helper function to convert hex string to byte array
        private byte[] HexStringToByteArray(string hex)
        {
            // Pad with '0' if the length is odd
            if (hex.Length % 2 != 0)
            {
                hex = "0" + hex;
            }

            int length = hex.Length;
            byte[] bytes = new byte[length / 2];

            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        // Find matching positions where the SearchString almost matches in the binary content
        private List<int> GetMatchingPositions(byte[] contentBytes, byte[] searchBytes, int threshold)
        {
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

        // Replace content at the specific position with ReplaceString
        private byte[] ReplaceAtPosition(byte[] contentBytes, int position, string replaceString)
        {
            // Convert the hex string to a byte array
            byte[] replaceBytes = Enumerable.Range(0, replaceString.Length / 2)
                                            .Select(x => Convert.ToByte(replaceString.Substring(x * 2, 2), 16))
                                            .ToArray();

            int replaceLength = replaceBytes.Length;

            // Create a new byte array for the result with the same length as contentBytes
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
        public List<SolutionMapping> GetSolutionMappings(string tuningId, List<string> names)
        {
            var mappings = new List<SolutionMapping>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                connection.Open();
                string query = @"
                SELECT MappingId, ReplacementId, Name
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
                        ReplacementId = reader.GetInt32(1),
                        Name = reader.GetString(2),
                    });
                }
            }

            return mappings;
        }

        public void InsertReplacementStringAsync(string tuningspecialid, string name, string searchString, string replaceString, int threshold, int action = 1, string description = "")
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                string query = @"
                INSERT INTO ReplacementStrings (SearchString, ReplaceString, Threshold, Action, Description, isDeleted)
                VALUES (@SearchString, @ReplaceString, @Threshold, @Action, @Description, 0);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

                int newId = connection.ExecuteScalar<int>(query, new
                {
                    SearchString = searchString,
                    ReplaceString = replaceString,
                    Threshold = threshold,
                    Action = action,
                    Description = description
                });
                //generate [SolutionMappings]
                string solutionMappingsQuery = @"
                INSERT INTO SolutionMappings (TuningSpecialInfoId, Name, ReplacementId)
                VALUES (@TuningSpecialInfoId, @Name, @ReplacementId)";

                connection.ExecuteScalar<int>(solutionMappingsQuery, new
                {
                    TuningSpecialInfoId = tuningspecialid,
                    Name = name,
                    ReplacementId = newId
                });
            }
        }

        // Helper function to retrieve replacement commands from the database
        public List<ReplacementCommand> GetReplacementCommands(int replacementId)
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

    }

    // Models to represent the database entities
    public class SolutionMapping
    {
        public int MappingId { get; set; }
        public int ReplacementId { get; set; }
        public string Name { get; set; }

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
}