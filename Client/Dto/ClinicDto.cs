using System.ComponentModel.DataAnnotations;

namespace Client.Dto
{
    public class ClinicDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string? Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string? Address { get; set; } = string.Empty;
    }

   
}
