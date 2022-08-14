
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.9.6.  DO NOT EDIT!
//*************************************************************************************

using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.Bridge;
using XTC.FMP.MOD.Hotspot2D.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.Proto;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    public class MyInstanceBase
    {
        public LibMVCS.Logger logger { get; set; }
        public MyConfig config { get; set; }
        public Dictionary<string, LibMVCS.Any> settings { get; set; }
        public GameObject rootUI { get; set; }


        public IDesignerViewBridge viewBridgeDesigner { get; set; }

        public IHealthyViewBridge viewBridgeHealthy { get; set; }


        protected void loadSpriteFromTheme(string _file, System.Action<Sprite> _onFinish)
        {
            Sprite sprite = null;

            string datapath = settings["datapath"].AsString();
            string vendor = settings["vendor"].AsString();
            string dir = System.IO.Path.Combine(datapath, vendor);
            dir = System.IO.Path.Combine(dir, "theme");
            dir = System.IO.Path.Combine(dir, MyEntryBase.ModuleName);
            string filefullpath = System.IO.Path.Combine(dir, _file);
            if (System.IO.File.Exists(filefullpath))
            {
                var bytes = System.IO.File.ReadAllBytes(filefullpath);
                var texture = new Texture2D(10, 10, TextureFormat.RGBA32, false);
                texture.LoadImage(bytes);
                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                logger.Error("{0} not found", filefullpath);
            }

            _onFinish(sprite);
        }


        protected virtual void submitDesignerReadStyleSheet(ScopeRequest _request)
        {
            var dto = new ScopeRequestDTO(_request);
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnReadStyleSheetSubmit(dto);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger.Exception(ex);
                }
            });
        }

        protected virtual void submitDesignerWriteStyle(DesignerWriteStylesRequest _request)
        {
            var dto = new DesignerWriteStylesRequestDTO(_request);
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnWriteStyleSubmit(dto);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger.Exception(ex);
                }
            });
        }

        protected virtual void submitDesignerReadInstances(ScopeRequest _request)
        {
            var dto = new ScopeRequestDTO(_request);
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnReadInstancesSubmit(dto);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger.Exception(ex);
                }
            });
        }

        protected virtual void submitDesignerWriteInstances(DesignerWriteInstancesRequest _request)
        {
            var dto = new DesignerWriteInstancesRequestDTO(_request);
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnWriteInstancesSubmit(dto);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger.Exception(ex);
                }
            });
        }

        protected virtual void submitHealthyEcho(HealthyEchoRequest _request)
        {
            var dto = new HealthyEchoRequestDTO(_request);
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeHealthy.OnEchoSubmit(dto);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger.Exception(ex);
                }
            });
        }


    }
}
