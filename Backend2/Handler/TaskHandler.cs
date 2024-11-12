
using Dapper;

using System.Data.SqlClient;

namespace BaseBackend.Controllers
{
    public class TaskHandler
    {
        private readonly StringReplacementHandler _stringReplacementHandler;
        private readonly MyUploadedFileHandler _myUploadedFileHandler;
        private readonly IConfiguration _configuration;

        public TaskHandler(IConfiguration configuration, StringReplacementHandler stringReplacementHandler, MyUploadedFileHandler myUploadedFileHandler)
        {
            _configuration = configuration;
            _stringReplacementHandler = stringReplacementHandler;
            _myUploadedFileHandler = myUploadedFileHandler;
        }

        public async void CheckForOpenTasksAndHandle()
        {
            var listOfItems = await _myUploadedFileHandler.GetAllOpenTasksAsync(0, -1); // get only new ones witch was not already done and state =0
            foreach (var item in listOfItems)
            {
                string expectedPath = "UploadedFiles\\" + item.MyUploadedFile.Username + "\\" + item.MyUploadedFile.FileName;
                var testPath = Path.Combine(Directory.GetCurrentDirectory(), expectedPath);
                bool AllCanBeDoneAndFinished = await _stringReplacementHandler.ReplaceStringsInFile(item.MyUploadedFile, testPath, item.MyUploadedFile.SelectedVariants.Replace(" ", "").ToLower().Split(',').ToList(), item.MyUploadedFile.TuningVariantId);
                if (AllCanBeDoneAndFinished)
                {
                    string outputFilePath = testPath + "_modded";
                    this.UpdateFileName(item.MyUploadedFile.Id, outputFilePath);
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
    }
}
