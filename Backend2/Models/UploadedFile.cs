namespace BaseBackend.Models
{
    public class UploadedFile
    {
        public int RequestID { get; set; }
        public int CarModelId { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int State { get; set; }
        public string Title { get; set; }
    }
}
