using System;
using System.Collections.Generic;
using System.Linq;
using BasicAddress = App.Models.BasicSerialization.Address;
using BasicEmployee = App.Models.BasicSerialization.Employee;
using CustomAddress = App.Models.CustomSerialization.Address;
using CustomEmployee = App.Models.CustomSerialization.Employee;

namespace App.Models
{
    public static class Factory
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        private static readonly ICollection<BasicAddress> BasicAddresses = new List<BasicAddress>
        {
            new BasicAddress
            {
                City = "Paris",
                Country = "France"
            },
            new BasicAddress
            {
                City = "Brussels",
                Country = "Belgium"
            }
        };

        private static readonly ICollection<CustomAddress> CustomAddresses = new List<CustomAddress>
        {
            new CustomAddress
            {
                City = "Paris",
                Country = "France"
            },
            new CustomAddress
            {
                City = "Brussels",
                Country = "Belgium"
            }
        };

        private static readonly ICollection<BasicEmployee> BasicEmployees = new List<BasicEmployee>
        {
            new BasicEmployee
            {
                Id = 1,
                FirstName = "Bill",
                LastName = "Gates",
                Title = "Microsoft Founder",
                Address = CreateAddressWithBasicSerialization()
            },
            new BasicEmployee
            {
                Id = 1,
                FirstName = "Steve",
                LastName = "Jobs",
                Title = "Apple Founder",
                Address = CreateAddressWithBasicSerialization()
            }
        };

        private static readonly ICollection<CustomEmployee> CustomEmployees = new List<CustomEmployee>
        {
            new CustomEmployee
            {
                Id = 1,
                FirstName = "Bill",
                LastName = "Gates",
                Title = "Microsoft Founder",
                Address = CreateAddressWithCustomSerialization()
            },
            new CustomEmployee
            {
                Id = 1,
                FirstName = "Steve",
                LastName = "Jobs",
                Title = "Apple Founder",
                Address = CreateAddressWithCustomSerialization()
            }
        };

        public static BasicEmployee CreateEmployeeWithBasicSerialization()
        {
            return BasicEmployees.OrderBy(_ => Random.Next()).First();
        }

        public static CustomEmployee CreateEmployeeWithCustomSerialization()
        {
            return CustomEmployees.OrderBy(_ => Random.Next()).First();
        }

        private static BasicAddress CreateAddressWithBasicSerialization()
        {
            return BasicAddresses.OrderBy(_ => Random.Next()).First();
        }

        private static CustomAddress CreateAddressWithCustomSerialization()
        {
            return CustomAddresses.OrderBy(_ => Random.Next()).First();
        }
    }
}
