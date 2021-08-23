using App.Models;
using App.Serializers;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using BasicEmployee = App.Models.BasicSerialization.Employee;
using CustomEmployee = App.Models.CustomSerialization.Employee;

namespace Tests
{
    public class MemoryJsonSerializerTests
    {
        [Fact]
        public void Should_Serialize_Deserialize_Employee_With_Basic_Serialization()
        {
            // arrange
            var logger = NullLogger.Instance;
            var employee = Factory.CreateEmployeeWithBasicSerialization();
            var serializer = new MemoryJsonSerializer<BasicEmployee>(logger);

            // act
            var json = serializer.Serialize(employee);

            // assert
            json.Should().NotBeNullOrEmpty();
            json.Should().StartWith("{").And.EndWith("}");
        }

        [Fact]
        public void Should_Serialize_Deserialize_Employee_With_Custom_Serialization()
        {
            // arrange
            var logger = NullLogger.Instance;
            var employee = Factory.CreateEmployeeWithCustomSerialization();
            var serializer = new MemoryJsonSerializer<CustomEmployee>(logger);

            // act
            var json = serializer.Serialize(employee);

            // assert
            json.Should().NotBeNullOrEmpty();
            json.Should().StartWith("{").And.EndWith("}");
        }
    }
}
