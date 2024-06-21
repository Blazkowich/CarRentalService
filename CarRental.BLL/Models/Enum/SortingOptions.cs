using System.ComponentModel.DataAnnotations;

namespace CarRental.BLL.Models.Enum;

public enum SortingOptions
{
    [Display(Name = "Most popular")]
    MostPopular,

    [Display(Name = "Most commented")]
    MostCommented,

    [Display(Name = "Price ASC")]
    PriceAscending,

    [Display(Name = "Price DESC")]
    PriceDescending,

    [Display(Name = "New")]
    New,
}

