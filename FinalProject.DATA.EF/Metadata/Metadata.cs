using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.DATA.EF
{
    #region CarMetadata
    public class CarMetadata
    {
        [Required(ErrorMessage ="*Make is REQUIRED")]
        [StringLength(30, ErrorMessage = "*Make must be 30 characters or less")]
        public string Make { get; set; }
        [Required(ErrorMessage = "*Year is REQUIRED")]
        public int Year { get; set; }
        [Required(ErrorMessage = "*Model is REQUIRED")]
        [StringLength(50, ErrorMessage = "*Model must be 50 characters or less")]
        public string Model { get; set; }
        [Required(ErrorMessage = "*Color is REQUIRED")]
        [StringLength(20, ErrorMessage = "*Color must be 20 characters or less")]
        public string Color { get; set; }
        [StringLength(50, ErrorMessage = "*Car pictures must be 50 characters or less")]
        public string CarPhoto { get; set; }
        [Required(ErrorMessage = "*Description is REQUIRED")]
        [StringLength(5000, ErrorMessage = "*Description must be 5000 characters or less")]
        public string Description { get; set; }
        [Required(ErrorMessage = "*Price Per Day is REQUIRED")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        public decimal PricePerDay { get; set; }
        public bool IsBooked { get; set; }
        public bool IsAutomatic { get; set; }
        public bool IsDiesel { get; set; }
        public bool IsElectric { get; set; }
        public bool HasGPS { get; set; }
        public bool HasBluetooth { get; set; }
    }
    [MetadataType(typeof(CarMetadata))]
    public partial class Car
    {
        [Display(Name = "Car Selection")]
        public string CarSelection
        { 
            get { return Year + " " + Make + " " + Model + " " + "(" + Color + ")"; }
        }
    }
    #endregion
    #region DealershipMetadata
    public class DealershipMetadata
    {
        [StringLength(100, ErrorMessage = "*Dealership Name must be 100 characters or less")]
        [Required(ErrorMessage = "*Dealership Name is REQUIRED")]
        [Display(Name = "Dealership Name")]
        public string DealershipName { get; set; }
        [StringLength(100, ErrorMessage = "*Address must be 30 characters or less")]
        [Required(ErrorMessage = "*Address is REQUIRED")]
        public string Address { get; set; }
        [StringLength(100, ErrorMessage = "*City must be 30 characters or less")]
        [Required(ErrorMessage = "*City is REQUIRED")]
        public string City { get; set; }
        [StringLength(2, ErrorMessage = "*State must be 2 characters or less")]
        [Required(ErrorMessage = "*State is REQUIRED")]
        public string State { get; set; }
        [Required(ErrorMessage = "*Zip Code is REQUIRED")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "*Phone Number is REQUIRED")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
    [MetadataType(typeof(DealershipMetadata))]
    public partial class Dealership
    {

    }
    #endregion
    #region UserDetailsMetadata
    public class UserDetailsMetadata
    {
        [Required(ErrorMessage = "*First Name is REQUIRED")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "*Last Name is REQUIRED")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*Phone Number is REQUIRED")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "*Address is REQUIRED")]
        public string Address { get; set; }
        [Required(ErrorMessage = "*City is REQUIRED")]
        public string City { get; set; }
        [Required(ErrorMessage = "*State is REQUIRED")]
        public string State { get; set; }
        [Required(ErrorMessage = "*Zip Code is REQUIRED")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
    [MetadataType(typeof(UserDetailsMetadata))]
    public partial class UserDetail
    {
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
    #endregion
    #region ReservationsMetadata
    public class ReservationsMetadata
    {
        [Required(ErrorMessage = "*CarID is REQUIRED")]
        public int CarId { get; set; }
        [Required(ErrorMessage = "*UserID is REQUIRED")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "*Reservation Date is REQUIRED")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = false)]
        public System.DateTime ReservationDate { get; set; }
    }
    [MetadataType(typeof(ReservationsMetadata))]
    public partial class Reservation
    {

    }
    #endregion
}
