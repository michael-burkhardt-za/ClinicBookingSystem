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

    public class ClinitDto1
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string AddressLine1 { get; set; } = string.Empty;

        [StringLength(200)]
        public string? AddressLine2 { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
