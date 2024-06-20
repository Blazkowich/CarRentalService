using System.ComponentModel.DataAnnotations;

namespace CarRental.Api.ApiModels.Enum;

public enum VehicleTypeEnumApi
{
    [Display(Name = "Sedan")]
    Sedan,
    [Display(Name = "Coupe")]
    Coupe,
    [Display(Name = "Van")]
    Van,
    [Display(Name = "HatchBack")]
    HatchBack,
    [Display(Name = "SUV")]
    SUV,
    [Display(Name = "Jeep")]
    Jeep,
    [Display(Name = "MiniVan")]
    MiniVan,
    [Display(Name = "Limousine")]
    Limousine
}
