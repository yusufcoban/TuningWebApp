using System;
using System.ComponentModel.DataAnnotations;

public class MyUploadedFile
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Username { get; set; }

    [Required]
    public DateTime UploadDate { get; set; } = DateTime.Now;

    [Required]
    [StringLength(100)]
    public string State { get; set; }

    [Required]
    public string TuningVariantId { get; set; }

    public string DTCList { get; set; }

    public string Information { get; set; }

    [Required]
    public string FileName { get; set; }


    [StringLength(255)]
    public string SelectedVariants { get; set; }

    [Required]
    [StringLength(100)]
    public string CarmodelId { get; set; }
}
