using BaseBackend.Models;

using Dapper;

using System.Data.SqlClient;
using System.Linq;

public class MyUploadedFileHandler
{
    private readonly IConfiguration _configuration;


    // Constructor to inject the connection string
    public MyUploadedFileHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string getDownloadPathBase()
    {
        return _configuration["FileSettings:UploadedFilesPath"];
    }

    public string getDownloadPathBaseWithUserName(string username)
    {
        return Path.Combine(_configuration["FileSettings:UploadedFilesPath"], username);
    }


    // CREATE: Add a new uploaded file record to the database
    public async Task<int> AddUploadedFileAsync(MyUploadedFile uploadedFile)
    {
        const string query = @"
            INSERT INTO MyUploadedFiles (Username, UploadDate, State, TuningVariantId, DTCList, Information, SelectedVariants, CarmodelId,FileName,ModifyDate,Title,additionalInfo)
            VALUES (@Username, @UploadDate, @State, @TuningVariantId, @DTCList, @Information, @SelectedVariants, @CarmodelId,@FileName,@ModifyDate,@Title,@additionalInfo);
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
                uploadedFile.ModifyDate,
                uploadedFile.additionalInfo

            });
            CreateTaskAsync(result);
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
            if (uploadedFile != null)
            {
                //find all tasks to get modified files
                const string queryTasks = "SELECT * FROM [Task] where MyUploadedFileId=@taskId";
                uploadedFile.tasks = await connection.QueryAsync<BaseBackend.Models.Task>(queryTasks, new { taskId = uploadedFile.Id });

            }
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

    // UPDATE: Update state of uploaded file

    public async Task<bool> UpdateUploadedFileStateAsync(int id, int newState, string comment)
    {
        const string query = @"
            UPDATE MyUploadedFiles
            SET 
                State = @State,
                additionalInfo=ISNULL(additionalInfo, '') + @Comment,
                ModifyDate=GetDate()
            WHERE Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                Id = id,
                State = newState,
                comment

            });

            return rowsAffected > 0; // Return true if the update was successful
        }
    }
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
            IEnumerable<MyUploadedFile> uploadedFiles = connection.Query<MyUploadedFile>(query, new { username });
            if (uploadedFiles != null)
            {
                foreach (var item in uploadedFiles)
                {
                    //find all tasks to get modified files
                    const string queryTasks = "SELECT * FROM [Task] where MyUploadedFileId=@taskId";
                    item.tasks = connection.Query<BaseBackend.Models.Task>(queryTasks, new { taskId = item.Id });
                }

            }
            return uploadedFiles;
        }
    }


    // Admin area

    // Admin Functions

    // 1. CREATE: Add a new Task with a null filename (linked to MyUploadedFiles)
    public async Task<int> CreateTaskAsync(int uploadedFileId)
    {
        const string query = @"
            INSERT INTO Task (MyUploadedFileId, NewFileName, CreateDate)
            VALUES (@MyUploadedFileId, NULL, GETDATE());
            SELECT CAST(SCOPE_IDENTITY() as int);";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            var result = await connection.QuerySingleAsync<int>(query, new { MyUploadedFileId = uploadedFileId });
            return result; // Returns the newly created Task ID
        }
    }

    // 2. READ: Get all open tasks (where NewFileName is NULL)
    public async Task<IEnumerable<BaseBackend.Models.Task>> GetOpenTasksAsyncInNullState()
    {
        const string query = "SELECT * FROM Task left join MyUploadedFiles on [MyUploadedFileId]=MyUploadedFiles.Id\r\n  where MyUploadedFiles.State < 5 AND NewFileName IS NULL";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            var tasks = await connection.QueryAsync<BaseBackend.Models.Task>(query);
            return tasks;
        }
    }

    public async Task<IEnumerable<BaseBackend.Models.Task>> GetAllOpenTasksAsync(int maxState = 4, int minState = 0)
    {
        const string query = @"
        SELECT t.*, f.* 
        FROM [Task] t
        LEFT JOIN MyUploadedFiles f ON t.MyUploadedFileId = f.Id
        WHERE f.State < @stateInput and  f.State > @stateMinInput";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();

            // Multi-mapping with Dapper to map Task and MyUploadedFile
            var tasks = await connection.QueryAsync<BaseBackend.Models.Task, MyUploadedFile, BaseBackend.Models.Task>(
                query,
                (task, file) =>
                {
                    task.MyUploadedFile = file;
                    return task;
                },

            param: new { stateInput = maxState + 1, stateMinInput = minState - 1 },
                splitOn: "Id" // The column where Dapper should start splitting the result set
            );

            return tasks;
        }
    }

    

    // 3. UPDATE: Upload file and set NewFileName, and update the State of MyUploadedFiles
    public async Task<bool> CompleteTaskAsync(int taskId, string newFileName, int newState)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string directory = Path.GetDirectoryName(newFileName);
                    string fileName = Path.GetFileName(newFileName);

                    // Update the Task with the new filename
                    const string updateTaskQuery = "UPDATE Task SET NewFileName = @NewFileName WHERE TaskId = @TaskId";
                    var taskUpdated = await connection.ExecuteAsync(updateTaskQuery, new { NewFileName = fileName, TaskId = taskId }, transaction);

                    // Update the related MyUploadedFile with the new state
                    const string updateFileQuery = @"
                        UPDATE MyUploadedFiles
                        SET State = @NewState, ModifyDate = GETDATE()
                        WHERE Id = (SELECT MyUploadedFileId FROM Task WHERE TaskId = @TaskId)";
                    var fileUpdated = await connection.ExecuteAsync(updateFileQuery, new { NewState = newState, TaskId = taskId }, transaction);

                    // Commit the transaction if both updates succeed
                    if (taskUpdated > 0 && fileUpdated > 0)
                    {
                        transaction.Commit();
                        return true;
                    }

                    transaction.Rollback();
                    return false;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }



}
