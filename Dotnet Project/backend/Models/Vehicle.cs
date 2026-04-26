using System.ComponentModel.DataAnnotations;

namespace GarageAPI.Models
{
    /// <summary>
    /// Represents a customer vehicle record for the garage.
    /// </summary>
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required.")]
        [MaxLength(150)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile Number is required.")]
        [MaxLength(50)]
        public string MobileNumber { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? VehicleModel { get; set; }

        [MaxLength(100)]
        public string? VehicleNumber { get; set; }

        public string? ProblemDescription { get; set; }

        [Required(ErrorMessage = "Date Received is required.")]
        [DataType(DataType.Date)]
        public DateTime DateReceived { get; set; }

        [Required(ErrorMessage = "Expected Return Date is required.")]
        [DataType(DataType.Date)]
        public DateTime ExpectedReturnDate { get; set; }
    }
}
