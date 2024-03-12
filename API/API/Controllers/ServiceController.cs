
using Driver.Service.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.ExceptionHandling;

namespace Driver.API.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly UpdateAddressAfterRecievedService updateAddressAfterRecievedService;
        private readonly RecivedService recivedService;
        public ServiceController(RecivedService recivedService,UpdateAddressAfterRecievedService updateAddressAfterRecievedService)
        {
            this.recivedService = recivedService;
            this.updateAddressAfterRecievedService = updateAddressAfterRecievedService;
        }

        [HttpGet]
        [Route("")]
        public async Task  GetData(string a,string b,string c1,string c2,string c3,string? c4,string?c5,string? c6,string? c7,string? c8)
        {
            bool isReturn=string.IsNullOrWhiteSpace(c8) ? false:c8=="0"?true:false;
            int code = Convert.ToInt32(a);
            switch (code)
            {
                case 0: await CreateErrors(Convert.ToInt32(a),
                                       Convert.ToInt64(b),
                                       Convert.ToInt64(c1),
                                       Convert.ToInt64(c2),
                                       Convert.ToDateTime(c3));
                        break;
                case 1: await CreateRecievedSpeedMission(Convert.ToInt32(a),
                                                     Convert.ToInt64(b),
                                                     Convert.ToDecimal(c1),
                                                     Convert.ToDecimal(c2),
                                                     Convert.ToDecimal(c3),
                                                     Convert.ToInt64(c4),
                                                     Convert.ToInt64(c5),
                                                     Convert.ToInt64(c6),
                                                     Convert.ToDateTime(c7),
                                                     isReturn);
                        break;
                case 2: await CreateRecievedStartedMission(Convert.ToInt32(a),
                                                        Convert.ToInt64(b),
                                                        Convert.ToDecimal(c1),
                                                        Convert.ToDecimal(c2),
                                                        Convert.ToInt32(c3),
                                                        Convert.ToInt32(c4),
                                                        Convert.ToInt64(c5),
                                                        Convert.ToInt64(c6),
                                                        Convert.ToDateTime(c7));
                        break;
                case 3: await CreateRecievedFinishedMission(Convert.ToInt32(a),
                                                         Convert.ToInt64(b),
                                                         Convert.ToDecimal(c1),
                                                         Convert.ToDecimal(c2),
                                                         Convert.ToInt64(c3),
                                                         Convert.ToInt64(c4),
                                                         Convert.ToDateTime(c5));
                        break;
                case 4: await CreateRecievedNumberMission(Convert.ToInt32(a),
                                                       Convert.ToInt64(b),
                                                       Convert.ToDecimal(c1),
                                                       Convert.ToDecimal(c2),
                                                       c3.Trim(),
                                                       Convert.ToInt64(c4),
                                                       Convert.ToDateTime(c5));
                        break;
                default:throw new BizException("Operation Invalid");
            }
        }

        [NonAction]
        public async Task CreateErrors(int a,long b,long c1,long c2,DateTime c3)
        {
            if (a != 0)
                throw new BizException("value is not valid");

            await recivedService.CreateErrorAsync(b,c1,c2, c3);

        }

        [NonAction]
        public async Task CreateRecievedStartedMission(int a, long b, decimal c1, decimal c2, int c3,int c4,long c5,long c6,DateTime c7)
        {
            if (a != 2)
                throw new BizException("value is not valid");

            var id=await recivedService.CreateRecievedStartedMissionAsync(b, c1, c2, c3,c4,c5,c6,c7);
            var jobId = BackgroundJob.Enqueue(() => UpdateAddressInStartedMission(id));
        }

        [NonAction]
        public async Task CreateRecievedSpeedMission(int a, long b, decimal c1, decimal c2, decimal c3, long c4, long c5, long c6, DateTime c7,bool c8=false)
        {
            if (a != 1)
                throw new BizException("value is not valid");

            var id=await recivedService.CreateRecievedSpeedAndTempratureAsync(b, c1, c2, c3,c4,c5,c6,c7,c8);

            var jobId = BackgroundJob.Enqueue(() => UpdateAddressInSpeedMission(id));
        }

        [NonAction]
        public async Task CreateRecievedFinishedMission(int a, long b, decimal c1, decimal c2, long c3, long c4, DateTime c5)
        {
            if (a != 3)
                throw new BizException("value is not valid");

            var id=await recivedService.CreateRecievedFinishedMissionAsync(b, c1, c2, c3, c4, c5);

            var jobId = BackgroundJob.Enqueue(() => UpdateAddressInFinishedMission(id));
        }

        [NonAction]
        public async Task CreateRecievedNumberMission(int a, long b, decimal c1, decimal c2, string c3, long c4, DateTime c5)
        {
            if (a != 4)
                throw new BizException("value is not valid");

            var id=await recivedService.CreateRecievedNumberAsync(b, c1, c2, c3, c4, c5);

            var jobId = BackgroundJob.Enqueue(() => UpdateAddressInNumberMission(id));
        }

        [NonAction]
        public void UpdateAddressInSpeedMission(long id)
        {
            var task=updateAddressAfterRecievedService.UpdateAddressInSpeedMission(id);
            task.Wait();
        }

        [NonAction]
        public void UpdateAddressInStartedMission(long id)
        {
            var task = updateAddressAfterRecievedService.UpdateAddressInStartMission(id);
            task.Wait();
        }

        [NonAction]
        public void UpdateAddressInFinishedMission(long id)
        {
            var task = updateAddressAfterRecievedService.UpdateAddressInFinishedMission(id);
            task.Wait();
        }

        [NonAction]
        public void UpdateAddressInNumberMission(long id)
        {
            var task = updateAddressAfterRecievedService.UpdateAddressInNumberMission(id);
            task.Wait();
        }
    }
}
