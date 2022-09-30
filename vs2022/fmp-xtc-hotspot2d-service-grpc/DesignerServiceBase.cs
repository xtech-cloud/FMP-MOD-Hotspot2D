
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.59.0.  DO NOT EDIT!
//*************************************************************************************

using System.Net;
using Grpc.Core;
using System.Threading.Tasks;
using XTC.FMP.MOD.Hotspot2D.LIB.Proto;

namespace XTC.FMP.MOD.Hotspot2D.App.Service
{
    /// <summary>
    /// Designer基类
    /// </summary>
    public class DesignerServiceBase : LIB.Proto.Designer.DesignerBase
    {
    

        public override async Task<DesignerReadStylesResponse> ReadStyleSheet(ScopeRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeReadStyleSheet(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new DesignerReadStylesResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new DesignerReadStylesResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<BlankResponse> WriteStyle(DesignerWriteStylesRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeWriteStyle(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new BlankResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BlankResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<DesignerReadInstancesResponse> ReadInstances(ScopeRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeReadInstances(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new DesignerReadInstancesResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new DesignerReadInstancesResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<BlankResponse> WriteInstances(DesignerWriteInstancesRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeWriteInstances(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new BlankResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BlankResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }



        protected virtual async Task<DesignerReadStylesResponse> safeReadStyleSheet(ScopeRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new DesignerReadStylesResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<BlankResponse> safeWriteStyle(DesignerWriteStylesRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new BlankResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<DesignerReadInstancesResponse> safeReadInstances(ScopeRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new DesignerReadInstancesResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<BlankResponse> safeWriteInstances(DesignerWriteInstancesRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new BlankResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

    }
}

