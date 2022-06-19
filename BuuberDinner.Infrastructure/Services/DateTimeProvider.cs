using BuuberDinner.Application.Common.Interfaces.Services;

namespace BuuberDinner.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}