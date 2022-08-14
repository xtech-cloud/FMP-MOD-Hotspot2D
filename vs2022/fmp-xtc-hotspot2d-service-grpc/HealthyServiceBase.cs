
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.9.1.  DO NOT EDIT!
//*************************************************************************************

using Grpc.Core;
using System.Threading.Tasks;
using XTC.FMP.MOD.Hotspot2D.LIB.Proto;

namespace XTC.FMP.MOD.Hotspot2D.App.Service
{
    /// <summary>
    /// Healthy基类
    /// </summary>
    public class HealthyServiceBase : LIB.Proto.Healthy.HealthyBase
    {
    

        public override async Task<HealthyEchoResponse> Echo(HealthyEchoRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new HealthyEchoResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

    }
}

