
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.86.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading.Tasks;
using XTC.FMP.MOD.Hotspot2D.LIB.Proto;

namespace XTC.FMP.MOD.Hotspot2D.LIB.MVCS
{
    /// <summary>
    /// Healthy服务模拟类
    /// </summary>
    public class HealthyServiceMock
    {


        public System.Func<HealthyEchoRequest, Task<HealthyEchoResponse>>? CallEchoDelegate { get; set; } = null;

    }
}
