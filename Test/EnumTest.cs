using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class EnumTest
    {
        [Fact]
        public void Test_Language_attributes()
        {
            Assert.Equal("enGB", Language.English.GetDescription());
            Assert.Equal("English", Language.English.GetName());
        }

        [Fact]
        public void Test_Country_attributes()
        {
            Assert.Equal("AZE", Country.Azerbaijan.GetDescription());
            Assert.Equal("Российская Федерация", Country.Russia.GetName());
        }

        [Fact]
        public void Test_Language() {
           Language result = EnumHelpers.TryGetValueFromCode("EN", Language.Russian);
        }
    }
}
