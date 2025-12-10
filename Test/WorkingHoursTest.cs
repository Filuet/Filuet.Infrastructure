using Filuet.Infrastructure.Abstractions.Helpers;
using Xunit;

namespace Test
{
    public class WorkingHoursTest
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetWorkingHours), MemberType = typeof(TestDataGenerator))]
        public void Test_WorkingHoursIs_Now(string schedule) {

            bool isWorkingHours = schedule.IsNowWorkingHours();
        }
    }
}