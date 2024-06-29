using System.ComponentModel.DataAnnotations;

namespace CarRental.Shared.Extensions;

public static class EnumExtension
{
    public static async Task<IEnumerable<string>> GetAllDisplayValuesAsync<TEnum>()
        where TEnum : Enum
    {
        var displayValues = Enum.GetValues(typeof(TEnum))
                                .Cast<TEnum>()
                                .Select(value =>
                                {
                                    var displayAttribute = value
                                        .GetType()
                                        .GetField(value.ToString())
                                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                                        .SingleOrDefault() as DisplayAttribute;
                                    return displayAttribute?.Name ?? value.ToString();
                                });

        return await Task.FromResult(displayValues);
    }
}

