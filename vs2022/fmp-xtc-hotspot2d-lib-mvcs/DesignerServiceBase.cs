
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.86.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.Proto;

namespace XTC.FMP.MOD.Hotspot2D.LIB.MVCS
{
    /// <summary>
    /// Designer服务层基类
    /// </summary>
    public class DesignerServiceBase : Service
    {
        public DesignerServiceMock mock { get; set; } = new DesignerServiceMock();
    
        /// <summary>
        /// 带uid参数的构造函数
        /// </summary>
        /// <param name="_uid">实例化后的唯一识别码</param>
        /// <param name="_gid">直系的组的ID</param>
        public DesignerServiceBase(string _uid, string _gid) : base(_uid)
        {
            gid_ = _gid;
        }

        /// <summary>
        /// 注入GRPC通道
        /// </summary>
        /// <param name="_channel">GRPC通道</param>
        public void InjectGrpcChannel(GrpcChannel? _channel)
        {
            grpcChannel_ = _channel;
        }


        /// <summary>
        /// 调用ReadStyleSheet
        /// </summary>
        /// <param name="_request">ReadStyleSheet的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallReadStyleSheet(ScopeRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call ReadStyleSheet ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            DesignerReadStylesResponse? response = null;
            if (null != mock.CallReadStyleSheetDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallReadStyleSheetDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.ReadStyleSheetAsync(_request);
            }

            getModel()?.UpdateProtoReadStyleSheet(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用WriteStyle
        /// </summary>
        /// <param name="_request">WriteStyle的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallWriteStyle(DesignerWriteStylesRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call WriteStyle ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            BlankResponse? response = null;
            if (null != mock.CallWriteStyleDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallWriteStyleDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.WriteStyleAsync(_request);
            }

            getModel()?.UpdateProtoWriteStyle(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用ReadInstances
        /// </summary>
        /// <param name="_request">ReadInstances的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallReadInstances(ScopeRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call ReadInstances ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            DesignerReadInstancesResponse? response = null;
            if (null != mock.CallReadInstancesDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallReadInstancesDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.ReadInstancesAsync(_request);
            }

            getModel()?.UpdateProtoReadInstances(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用WriteInstances
        /// </summary>
        /// <param name="_request">WriteInstances的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallWriteInstances(DesignerWriteInstancesRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call WriteInstances ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            BlankResponse? response = null;
            if (null != mock.CallWriteInstancesDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallWriteInstancesDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.WriteInstancesAsync(_request);
            }

            getModel()?.UpdateProtoWriteInstances(response, _context);
            return Error.OK;
        }


        /// <summary>
        /// 获取直系数据层
        /// </summary>
        /// <returns>数据层</returns>
        protected DesignerModel? getModel()
        {
            if(null == model_)
                model_ = findModel(DesignerModel.NAME + "." + gid_) as DesignerModel;
            return model_;
        }

        /// <summary>
        /// 获取GRPC客户端
        /// </summary>
        /// <returns>GRPC客户端</returns>
        protected Designer.DesignerClient? getGrpcClient()
        {
            if (null == grpcChannel_)
                return null;

            if(null == clientDesigner_)
            {
                clientDesigner_ = new Designer.DesignerClient(grpcChannel_);
            }
            return clientDesigner_;
        }

        /// <summary>
        /// 直系的MVCS的四个组件的组的ID
        /// </summary>
        protected string gid_ = "";

        /// <summary>
        /// GRPC客户端
        /// </summary>
        protected Designer.DesignerClient? clientDesigner_;

        /// <summary>
        /// GRPC通道
        /// </summary>
        protected GrpcChannel? grpcChannel_;

        /// <summary>
        /// 直系数据层
        /// </summary>
        private DesignerModel? model_;
    }

}
