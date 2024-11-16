
using Dapper;

using System.Data.SqlClient;

using YourNamespace.Controllers;
using YourNamespace.Handler;

namespace BaseBackend.Controllers
{
    public class TaskHandler
    {
        private readonly ILogger<TaskHandler> _logger;

        private readonly StringReplacementHandler _stringReplacementHandler;
        private readonly MyUploadedFileHandler _myUploadedFileHandler;
        private readonly IConfiguration _configuration;

        public TaskHandler(IConfiguration configuration, StringReplacementHandler stringReplacementHandler, MyUploadedFileHandler myUploadedFileHandler, ILogger<TaskHandler> ilogger)
        {
            _configuration = configuration;
            _stringReplacementHandler = stringReplacementHandler;
            _myUploadedFileHandler = myUploadedFileHandler;
            _logger = ilogger;
        }

        public async void CheckForOpenTasksAndHandle()
        {
            var listOfItems = await _myUploadedFileHandler.GetAllOpenTasksAsync(0, -1); // get only new ones witch was not already done and state =0
            _logger.LogError(listOfItems.Count() + "files will be checked..");
            foreach (var item in listOfItems)
            {
                try
                {
                    _logger.LogError(item.MyUploadedFile.Username + " uploaded " + item.MyUploadedFile.FileName);

                    string expectedPath = "UploadedFiles\\" + item.MyUploadedFile.Username + "\\" + item.MyUploadedFile.FileName;
                    var testPath = Path.Combine(Directory.GetCurrentDirectory(), expectedPath);
                    bool AllCanBeDoneAndFinished = await _stringReplacementHandler.ReplaceStringsInFile(item.MyUploadedFile, testPath, item.MyUploadedFile.SelectedVariants.Replace(" ", "").ToLower().Split(',').ToList(), item.MyUploadedFile.TuningVariantId);
                    if (AllCanBeDoneAndFinished)
                    {
                        string outputFilePath = testPath + "_modded";
                        this.UpdateFileName(item.MyUploadedFile.Id, outputFilePath);
                    }
                }
                catch (Exception ex)
                {
                    UpdateStateMyUploadedFile(item.MyUploadedFileId, 6, "File not found...."+ex.Message);
                }

            }
        }

        public async void UpdateFileName(int id, string NewFileName)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo").ToString()))
            {
                connection.Open();

                // Get the stored hash for the user
                string sql = "UPDATE [Task] Set [NewFileName] =@NewFileName WHERE [MyUploadedFileId] = @id";
                connection.Execute(sql, new { id = id, NewFileName = NewFileName });
            }
        }

        public async void UpdateStateMyUploadedFile(int id, int state, string comment="")
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo").ToString()))
            {
                connection.Open();

                // Get the stored hash for the user
                string sql = "UPDATE [MyUploadedFiles] Set [State] =@state, additionalInfo=@comment WHERE [Id] = @id";
                connection.Execute(sql, new { id = id, state = state , comment = comment });
            }
        }
    }
}
