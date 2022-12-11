
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.70.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using XTC.FMP.LIB.MVCS;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Bridge
{
    /// <summary>
    /// Designer的UI桥接层（协议部分）
    /// 刷新从视图收到的数据
    /// </summary>
    public interface IDesignerUiProtoBridge : View.Facade.Bridge
    {
        /// <summary>
        /// 全局警告
        /// </summary>
        /// <param name="_code">错误码</param>
        /// <param name="_code">错误信息</param>
        void Alert(string _code, string _message, object? _context);

        /// <summary>
        /// 刷新ReadStyleSheet的数据
        /// </summary>
        void RefreshReadStyleSheet(IDTO _dto, object? _context);


        /// <summary>
        /// 刷新WriteStyle的数据
        /// </summary>
        void RefreshWriteStyle(IDTO _dto, object? _context);


        /// <summary>
        /// 刷新ReadInstances的数据
        /// </summary>
        void RefreshReadInstances(IDTO _dto, object? _context);


        /// <summary>
        /// 刷新WriteInstances的数据
        /// </summary>
        void RefreshWriteInstances(IDTO _dto, object? _context);


    }
}

