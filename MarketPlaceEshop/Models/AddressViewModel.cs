using DataTransferObject.DTOClasses.Address.Commands;
using DataTransferObject.DTOClasses.Address.Results;

namespace MarketPlaceEshop.Models;

public class AddressViewModel
{
    public List<AddressResult> Addresses { get; set; }
    public AddressCommand AddressDto { get; set; }
    public List<ProvinceResult> Provinces { get; set; }
    public List<CityResult> Cities { get; set; }
}
