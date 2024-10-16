using Dapper;

using Microsoft.Extensions.Configuration;

using System.Data;
using System.Data.SqlClient;

public class MyUploadedFileHandler
{
    private readonly IConfiguration _configuration;


    // Constructor to inject the connection string
    public MyUploadedFileHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    // CREATE: Add a new uploaded file record to the database
    public async Task<int> AddUploadedFileAsync(MyUploadedFile uploadedFile)
    {
        const string query = @"
            INSERT INTO MyUploadedFiles (Username, UploadDate, State, TuningVariantId, DTCList, Information, SelectedVariants, CarmodelId,FileName,ModifyDate,Title)
            VALUES (@Username, @UploadDate, @State, @TuningVariantId, @DTCList, @Information, @SelectedVariants, @CarmodelId,@FileName,@ModifyDate,@Title);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            var result = await connection.QuerySingleAsync<int>(query, new
            {
                uploadedFile.Username,
                uploadedFile.UploadDate,
                uploadedFile.State,
                uploadedFile.TuningVariantId,
                uploadedFile.DTCList,
                uploadedFile.Information,
                uploadedFile.SelectedVariants,
                uploadedFile.CarmodelId,
                uploadedFile.FileName,
                uploadedFile.Title,
                uploadedFile.ModifyDate

            });

            return result; // Returns the newly created record's ID
        }
    }

    // READ: Get an uploaded file by its ID
    public async Task<MyUploadedFile> GetUploadedFileByIdAsync(int id)
    {
        const string query = "SELECT * FROM MyUploadedFiles WHERE Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            var uploadedFile = await connection.QuerySingleOrDefaultAsync<MyUploadedFile>(query, new { Id = id });
            return uploadedFile;
        }
    }

    // READ: Get all uploaded files
    public async Task<IEnumerable<MyUploadedFile>> GetAllUploadedFilesAsync()
    {
        const string query = "SELECT * FROM MyUploadedFiles";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            var uploadedFiles = await connection.QueryAsync<MyUploadedFile>(query);
            return uploadedFiles;
        }
    }

    // UPDATE: Update an existing uploaded file
    public async Task<bool> UpdateUploadedFileAsync(MyUploadedFile uploadedFile)
    {
        const string query = @"
            UPDATE MyUploadedFiles
            SET 
                Username = @Username,
                UploadDate = @UploadDate,
                State = @State,
                TuningVariantId = @TuningVariantId,
                DTCList = @DTCList,
                Information = @Information,
                SelectedVariants = @SelectedVariants,
                CarmodelId = @CarmodelId,
                FileName=@FileName,
                ModifyDate=@ModifyDate,
Title=@Title
            WHERE Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                uploadedFile.Username,
                uploadedFile.UploadDate,
                uploadedFile.State,
                uploadedFile.TuningVariantId,
                uploadedFile.DTCList,
                uploadedFile.Information,
                uploadedFile.SelectedVariants,
                uploadedFile.CarmodelId,
                uploadedFile.Id,
                uploadedFile.FileName,
                uploadedFile.ModifyDate,
                uploadedFile.Title
            });

            return rowsAffected > 0; // Return true if the update was successful
        }
    }

    // DELETE: Delete an uploaded file by its ID
    public async Task<bool> DeleteUploadedFileAsync(int id)
    {
        const string query = "DELETE FROM MyUploadedFiles WHERE Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0; // Return true if the delete was successful
        }
    }

    public IEnumerable<MyUploadedFile> GetAllUploadedFilesAsyncByUserName(string username)
    {
        const string query = "SELECT * FROM MyUploadedFiles where username=@username";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            var uploadedFiles = connection.Query<MyUploadedFile>(query, new { username });
            return uploadedFiles;
        }
    }
}
