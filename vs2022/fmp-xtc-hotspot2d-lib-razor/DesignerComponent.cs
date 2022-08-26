
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.29.2.  DO NOT EDIT!
//*************************************************************************************

using Microsoft.AspNetCore.Components;
using AntDesign;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.MVCS;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Razor
{
    public partial class DesignerComponent
    {
        private DesignerFacade? getFacade()
        {
            if (null == facade_)
            {
                if (null == framework_)
                {
                    logger_?.Error("framework_ is null");
                    return null;
                }
                var entry = framework_.getUserData("XTC.FMP.MOD.Hotspot2D.LIB.MVCS.Entry") as Entry;
                if (null == entry)
                {
                    logger_?.Error("entry is null");
                    return null;
                }
                facade_ = entry?.getDynamicDesignerFacade("default");
                if (null == facade_)
                {
                    logger_?.Error("facade_ is null");
                    return null;
                }
                facade_?.setUiBridge(new DesignerUiBridge(this));
            }
            return facade_;
        }

        /// <summary>
        /// 注入的MVCS框架
        /// </summary>
        [Inject] Framework? framework_ { get; set; }

        /// <summary>
        /// 注入的日志
        /// </summary>
        [Inject] Logger? logger_ { get; set; }

        /// <summary>
        /// 直系UI装饰层
        /// </summary>
        private DesignerFacade? facade_;

        /// <summary>
        /// 注入的全局提示服务
        /// </summary>
        [Inject] MessageService? messageService_ { get; set; }
    }
}
