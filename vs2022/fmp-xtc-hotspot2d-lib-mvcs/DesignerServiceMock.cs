
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.86.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading.Tasks;
using XTC.FMP.MOD.Hotspot2D.LIB.Proto;

namespace XTC.FMP.MOD.Hotspot2D.LIB.MVCS
{
    /// <summary>
    /// Designer服务模拟类
    /// </summary>
    public class DesignerServiceMock
    {


        public System.Func<ScopeRequest, Task<DesignerReadStylesResponse>>? CallReadStyleSheetDelegate { get; set; } = null;

        public System.Func<DesignerWriteStylesRequest, Task<BlankResponse>>? CallWriteStyleDelegate { get; set; } = null;

        public System.Func<ScopeRequest, Task<DesignerReadInstancesResponse>>? CallReadInstancesDelegate { get; set; } = null;

        public System.Func<DesignerWriteInstancesRequest, Task<BlankResponse>>? CallWriteInstancesDelegate { get; set; } = null;

    }
}
