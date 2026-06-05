using System.ComponentModel.DataAnnotations;

namespace ModelBindingDemo.API.Models;

public class StudentCreateRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(18, 60)]
    public int Age { get; set; }

    [Required]
    [StringLength(50)]
    public string Department { get; set; } = string.Empty;
}
