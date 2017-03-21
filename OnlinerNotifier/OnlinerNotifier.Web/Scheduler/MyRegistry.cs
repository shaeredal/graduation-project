using FluentScheduler;
using OnlinerNotifier.Scheduler.Jobs;

namespace OnlinerNotifier.Scheduler
{
    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            Schedule<CheckPricesJob>().ToRunNow().AndEvery(1).Days();
            Schedule<SendEmailJob>().ToRunNow();
            Schedule<PushEmailsJob>().ToRunNow().AndEvery(1).Days();
        }
    }
}