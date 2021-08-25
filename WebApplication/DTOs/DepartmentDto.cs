using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class DepartmentDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}