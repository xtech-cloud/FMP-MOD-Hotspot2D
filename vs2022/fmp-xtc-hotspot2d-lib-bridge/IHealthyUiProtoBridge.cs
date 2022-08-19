
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.23.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using XTC.FMP.LIB.MVCS;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Bridge
{
    /// <summary>
    /// Healthy的UI桥接层（协议部分）
    /// 刷新从视图收到的数据
    /// </summary>
    public interface IHealthyUiProtoBridge : View.Facade.Bridge
    {
        /// <summary>
        /// 全局警告
        /// </summary>
        /// <param name="_code">错误码</param>
        /// <param name="_code">错误信息</param>
        void Alert(string _code, string _message, SynchronizationContext? _context);

        /// <summary>
        /// 刷新Echo的数据
        /// </summary>
        void RefreshEcho(IDTO _dto, SynchronizationContext? _context);


    }
}

