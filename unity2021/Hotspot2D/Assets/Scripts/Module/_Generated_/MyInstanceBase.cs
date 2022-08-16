
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.12.0.  DO NOT EDIT!
//*************************************************************************************

using System.IO;
using System.Threading;
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
        public string uid { get; private set; }
        public GameObject rootUI { get; private set; }
        public GameObject rootAttachments { get; private set; }


        public IDesignerViewBridge viewBridgeDesigner { get; set; }

        public IHealthyViewBridge viewBridgeHealthy { get; set; }


        protected MyEntryBase entry_ { get; set; }
        protected LibMVCS.Logger logger_ { get; set; }
        protected MyConfig config_ { get; set; }
        protected MyConfig.Style style_ { get; set; }
        protected Dictionary<string, LibMVCS.Any> settings_ { get; set; }
        protected MonoBehaviour mono_ {get;set;}

        public MyInstanceBase(string _uid, string _style, MyConfig _config, LibMVCS.Logger _logger, Dictionary<string, LibMVCS.Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
        {
            uid = _uid;
            config_ = _config;
            logger_ = _logger;
            settings_ = _settings;
            entry_ = _entry;
            mono_ = _mono;
            rootAttachments = _rootAttachments;
            foreach(var style in config_.styles)
            {
                if (style.name.Equals(_style))
                {
                    style_ = style;
                    break;
                }
            }
        }

        /// <summary>
        /// 实例化UI
        /// </summary>
        /// <param name="_instanceUI">ui的实例模板</param>
        public void InstantiateUI(GameObject _instanceUI)
        {
            rootUI = Object.Instantiate(_instanceUI, _instanceUI.transform.parent);
            rootUI.name = uid;
        }

        public void SetupBridges()
        {

            var facadeDesigner = entry_.getDynamicDesignerFacade(uid);
            var bridgeDesigner = new DesignerUiBridge();
            bridgeDesigner.logger = logger_;
            facadeDesigner.setUiBridge(bridgeDesigner);
            viewBridgeDesigner = facadeDesigner.getViewBridge() as IDesignerViewBridge;

            var facadeHealthy = entry_.getDynamicHealthyFacade(uid);
            var bridgeHealthy = new HealthyUiBridge();
            bridgeHealthy.logger = logger_;
            facadeHealthy.setUiBridge(bridgeHealthy);
            viewBridgeHealthy = facadeHealthy.getViewBridge() as IHealthyViewBridge;

        }

        protected void loadSpriteFromTheme(string _file, System.Action<Sprite> _onFinish)
        {
            Sprite sprite = null;

            string datapath = settings_["datapath"].AsString();
            string vendor = settings_["vendor"].AsString();
            string dir = System.IO.Path.Combine(datapath, vendor);
            dir = System.IO.Path.Combine(dir, "themes");
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
                logger_.Error("{0} not found", filefullpath);
            }

            _onFinish(sprite);
        }

         
        protected string combineAssetPath(string _source, string _uri)
        {
            if(_source.Equals("file://assloud"))
            {
                var dir = Path.Combine(settings_["datapath"].AsString(), settings_["vendor"].AsString());
                dir = Path.Combine(dir, "assloud");
                return Path.Combine(dir, _uri);
            }
            return _uri;
        }


        protected virtual void submitDesignerReadStyleSheet(ScopeRequest _request)
        {
            var dto = new ScopeRequestDTO(_request);
            SynchronizationContext context = SynchronizationContext.Current;
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnReadStyleSheetSubmit(dto, context);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger_.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger_.Exception(ex);
                }
            });
        }

        protected virtual void submitDesignerWriteStyle(DesignerWriteStylesRequest _request)
        {
            var dto = new DesignerWriteStylesRequestDTO(_request);
            SynchronizationContext context = SynchronizationContext.Current;
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnWriteStyleSubmit(dto, context);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger_.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger_.Exception(ex);
                }
            });
        }

        protected virtual void submitDesignerReadInstances(ScopeRequest _request)
        {
            var dto = new ScopeRequestDTO(_request);
            SynchronizationContext context = SynchronizationContext.Current;
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnReadInstancesSubmit(dto, context);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger_.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger_.Exception(ex);
                }
            });
        }

        protected virtual void submitDesignerWriteInstances(DesignerWriteInstancesRequest _request)
        {
            var dto = new DesignerWriteInstancesRequestDTO(_request);
            SynchronizationContext context = SynchronizationContext.Current;
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeDesigner.OnWriteInstancesSubmit(dto, context);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger_.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger_.Exception(ex);
                }
            });
        }

        protected virtual void submitHealthyEcho(HealthyEchoRequest _request)
        {
            var dto = new HealthyEchoRequestDTO(_request);
            SynchronizationContext context = SynchronizationContext.Current;
            Task.Run(async () =>
            {
                try
                {
                    var reslut = await viewBridgeHealthy.OnEchoSubmit(dto, context);
                    if (!LibMVCS.Error.IsOK(reslut))
                    {
                        logger_.Error(reslut.getMessage());
                    }
                }
                catch (System.Exception ex)
                {
                    logger_.Exception(ex);
                }
            });
        }


    }
}