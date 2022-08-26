
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.29.2.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.Proto;

namespace XTC.FMP.MOD.Hotspot2D.LIB.MVCS
{
    /// <summary>
    /// Designer控制层基类
    /// </summary>
    public class DesignerControllerBase : Controller
    {
        /// <summary>
        /// 带uid参数的构造函数
        /// </summary>
        /// <param name="_uid">实例化后的唯一识别码</param>
        /// <param name="_gid">直系的组的ID</param>
        public DesignerControllerBase(string _uid, string _gid) : base(_uid)
        {
            gid_ = _gid;
        }


        /// <summary>
        /// 更新ReadStyleSheet的数据
        /// </summary>
        /// <param name="_status">直系状态</param>
        /// <param name="_response">ReadStyleSheet的回复</param>
        public virtual void UpdateProtoReadStyleSheet(DesignerModel.DesignerStatus? _status, DesignerReadStylesResponse _response, SynchronizationContext? _context)
        {
            Error err = new Error(_response.Status.Code, _response.Status.Message);
            DesignerReadStylesResponseDTO? dto = new DesignerReadStylesResponseDTO(_response);
            getView()?.RefreshProtoReadStyleSheet(err, dto, _context);
        }

        /// <summary>
        /// 更新WriteStyle的数据
        /// </summary>
        /// <param name="_status">直系状态</param>
        /// <param name="_response">WriteStyle的回复</param>
        public virtual void UpdateProtoWriteStyle(DesignerModel.DesignerStatus? _status, BlankResponse _response, SynchronizationContext? _context)
        {
            Error err = new Error(_response.Status.Code, _response.Status.Message);
            BlankResponseDTO? dto = new BlankResponseDTO(_response);
            getView()?.RefreshProtoWriteStyle(err, dto, _context);
        }

        /// <summary>
        /// 更新ReadInstances的数据
        /// </summary>
        /// <param name="_status">直系状态</param>
        /// <param name="_response">ReadInstances的回复</param>
        public virtual void UpdateProtoReadInstances(DesignerModel.DesignerStatus? _status, DesignerReadInstancesResponse _response, SynchronizationContext? _context)
        {
            Error err = new Error(_response.Status.Code, _response.Status.Message);
            DesignerReadInstancesResponseDTO? dto = new DesignerReadInstancesResponseDTO(_response);
            getView()?.RefreshProtoReadInstances(err, dto, _context);
        }

        /// <summary>
        /// 更新WriteInstances的数据
        /// </summary>
        /// <param name="_status">直系状态</param>
        /// <param name="_response">WriteInstances的回复</param>
        public virtual void UpdateProtoWriteInstances(DesignerModel.DesignerStatus? _status, BlankResponse _response, SynchronizationContext? _context)
        {
            Error err = new Error(_response.Status.Code, _response.Status.Message);
            BlankResponseDTO? dto = new BlankResponseDTO(_response);
            getView()?.RefreshProtoWriteInstances(err, dto, _context);
        }


        /// <summary>
        /// 获取直系视图层
        /// </summary>
        /// <returns>视图层</returns>
        protected DesignerView? getView()
        {
            if(null == view_)
                view_ = findView(DesignerView.NAME + "." + gid_) as DesignerView;
            return view_;
        }

        /// <summary>
        /// 直系的MVCS的四个组件的组的ID
        /// </summary>
        protected string gid_ = "";

        /// <summary>
        /// 直系视图层
        /// </summary>
        private DesignerView? view_;
    }
}
