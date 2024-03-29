﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QuartzFactoryTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public QuartzService QuartzService { get; }

        public TestController(QuartzService quartzService)
        {
            QuartzService = quartzService;
        }

        [HttpGet]
        public async Task Start() => await QuartzService.Start();

        [HttpGet]
        public async Task AddJob(string jobName, string cron)
        {
            await QuartzService.AddJob(jobName, cron);
        }

        [HttpGet]
        public async Task RemoveJob(string jobName)
        {
            await QuartzService.RemoveJob(jobName);
        }

        [HttpGet]
        public async Task PauseJob(string jobName)
        {
            await QuartzService.PauseJob(jobName);
        }

        [HttpGet]
        public async Task ResumeJob(string jobName)
        {
            await QuartzService.ResumeJob(jobName);
        }
    }
}
