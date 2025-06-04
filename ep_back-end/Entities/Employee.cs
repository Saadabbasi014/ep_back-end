using System.ComponentModel.DataAnnotations;

namespace ep_back_end.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public required string Phone { get; set; }
        public required string Gender { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public int Salary { get; set; }

        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
        public int Age { get; set; }

        public bool Status { get; set; }

        [StringLength(100, ErrorMessage = "Designation cannot exceed 100 characters.")]
        public string? Designation { get; set; }

        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string? Department { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Joining")]
        public DateTime? DateOfJoining { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }

        [Url(ErrorMessage = "Invalid LinkedIn URL")]
        public string? LinkedInProfile { get; set; }

        [StringLength(20, ErrorMessage = "Employee code cannot exceed 20 characters.")]
        public string? EmployeeCode { get; set; }
    }
}
