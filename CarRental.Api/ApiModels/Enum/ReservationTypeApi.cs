using System.ComponentModel.DataAnnotations;

namespace CarRental.Api.ApiModels.Enum;

public enum ReservationTypeApi
{
    [Display(Name = "Reserved")]
    Reserved,

    [Display(Name = "Free")]
    Free
}
