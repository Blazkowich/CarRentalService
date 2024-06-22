using System.ComponentModel.DataAnnotations;

namespace CarRental.Api.ApiModels.Enum;

public enum BookingTypeApi
{
    [Display(Name = "Active")]
    Active,

    [Display(Name = "Reserved")]
    Reserved,

    [Display(Name = "Cancelled")]
    Cancelled,

    [Display(Name = "Finished")]
    Finished
}
