using Quartz;

namespace QuartzTest;

public class JobSchedule
{
    private IScheduler _scheduler;

    public JobSchedule(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }
}
