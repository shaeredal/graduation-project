using System.Web.Hosting;
using FluentScheduler;
using OnlinerNotifier.BLL.Services.Interfaces.PriceChangesServices;

namespace OnlinerNotifier.Scheduler.Jobs
{
    public class CheckPricesJob : IJob, IRegisteredObject
    {
        private readonly object lockObject = new object();

        private bool shuttingDown;

        private readonly IPricesChangesInfoService _pricesChangesObserverService;

        public CheckPricesJob(IPricesChangesInfoService _pricesChangesObserverService)
        {
            HostingEnvironment.RegisterObject(this);

            this._pricesChangesObserverService = _pricesChangesObserverService;
        }

        public void Execute()
        {
            lock (lockObject)
            {
                if (shuttingDown)
                    return;

                _pricesChangesObserverService.Update();
            }
        }

        public void Stop(bool immediate)
        {
            lock (lockObject)
            {
                shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }
}