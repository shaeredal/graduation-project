using System;

namespace OnlinerNotifier.BLL.Services.Interfaces
{
    public interface ITimeCalculationService
    {
        DateTime CalculateNotificationTime(DateTime userNotificationTimeUtc);
    }
}
