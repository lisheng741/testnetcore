// you can have base properties
using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;

// var properties = new NameValueCollection();

// IScheduler scheduler = await SchedulerBuilder.Create(properties)
//     .BuildScheduler();

// await scheduler.Start();

// Grab the Scheduler instance from the Factory
StdSchedulerFactory factory = new StdSchedulerFactory();
IScheduler scheduler = await factory.GetScheduler();

// and start it off
await scheduler.Start();

IJobDetail job = JobBuilder.Create<TestJob>()
                .WithIdentity("job1", "group1")
                .UsingJobData("testKey", "test value")
                .Build();

ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

await scheduler.ScheduleJob(job, trigger);

// some sleep to show what's happening
await Task.Delay(TimeSpan.FromSeconds(60));

// and last shut down the scheduler when you are ready to close your program
await scheduler.Shutdown();
