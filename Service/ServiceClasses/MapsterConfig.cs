using DataTransferObject.DTOClasses;
using DataTransferObject.DTOClasses.Address.Commands;
using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Mapster;
using Model.Entities.Addresses;
using Model.Entities.Orders;
using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses
{
    public static class MapsterConfig
    {
        public static void RegisterMapping()
        {

            TypeAdapterConfig<Address, AddressCommand>.NewConfig()
                .Map(dest => dest.ProvinceName, src => src.City.Province.ProvinceName)
                .Map(dest => dest.CityName, src => src.City.CityName);


            TypeAdapterConfig<User, UserCommand>.NewConfig()
                .Map(dest => dest.Email, src => src.UserName);


            TypeAdapterConfig<UserCommand, User>.NewConfig()
                .Map(dest => dest.UserName, src => src.Email);

            TypeAdapterConfig<Supplier, SupplierResult>.NewConfig()
               .Map(dest => dest.FirstName, src => src.User.FirstName)
               .Map(dest => dest.LastName, src => src.User.LastName)
               .Map(dest => dest.DateOfBirth, src => src.User.DateOfBirth)
               .Map(dest => dest.Email, src => src.User.Email)
               .Map(dest => dest.Id, src => src.User.Id)
               .Map(dest => dest.MobileNumber, src => src.User.MobileNumber)
               .Map(dest => dest.NationalCode, src => src.User.NationalCode)
               .Map(dest => dest.PhoneNumber, src => src.User.PhoneNumber);
             







            /*TypeAdapterConfig<Order, OrderDTO>.NewConfig()
                .Map(x => x.Cost, y => y.TotalCost)
                .Map(x => x.Discount, y => y.TotalDiscount);*/

            /* TypeAdapterConfig<Address, AddressDTO>.NewConfig()
                 .Map(x => x.Detail, y => y.AddressDetail)
                 .Map(x => x.unit, y => y.UnitNumber);


             TypeAdapterConfig<User, UserDTO>.NewConfig()
                 .Map(x => x.Email, y => y.UserName);


             TypeAdapterConfig<UserDTO, User>.NewConfig()
                 .Map(x => x.UserName, y => y.Email);*/

        }
    }
}
