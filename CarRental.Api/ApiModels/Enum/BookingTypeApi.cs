using System.ComponentModel.DataAnnotations;

namespace CarRental.Api.ApiModels.Enum;

public enum BookingTypeApi
{
    Active,
    Reserved,
    Cancelled,
    Finished
}
