using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBackend.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }  // Primary key

        [Required]
        public int MyUploadedFileId { get; set; }  // Foreign key linking to MyUploadedFiles

        [Required]
        [MaxLength(255)]
        public string NewFileName { get; set; }  // New file name

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;  // Creation date with a default value

        // Navigation property for the relationship with MyUploadedFiles
        [ForeignKey("MyUploadedFileId")]
        public MyUploadedFile MyUploadedFile { get; set; }
    }
}
