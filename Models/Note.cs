using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models;

public class Note
{
    [Key]
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public string? Text { get; set; }
    public DateTime Date { get; set; }
    public string Tags { get; set; } = "";
}