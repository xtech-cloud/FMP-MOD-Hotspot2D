
//*************************************************************************************
//   !!! Generated by the fmp-cli.  DO NOT EDIT!
//*************************************************************************************

using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using UnityEngine;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.Bridge;
using XTC.FMP.MOD.Hotspot2D.LIB.MVCS;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    /// <summary>
    /// 模块入口基类
    /// </summary>
    public class MyEntryBase : Entry
    {
        /// <summary>
        /// 模块名
        /// </summary>
        public const string ModuleName = "XTC_Hotspot2D";

        protected MonoBehaviour mono_;
        protected MyConfig config_;
        protected LibMVCS.Logger logger_;
        protected Dictionary<string, LibMVCS.Any> settings_;
        protected MyRuntime runtime_ { get; set; }


        /// <summary>
        /// 手动注入
        /// </summary>
        /// <remarks>
        /// 此函数会在宿主程序加载模块后调用，并注入需要的实现
        /// </remarks>
        /// <param name="_mono"></param>
        /// <param name="_logger"></param>
        /// <param name="_config"></param>
        /// <param name="_settings"></param>
        public void UniInject(MonoBehaviour _mono, Options _options, LibMVCS.Logger _logger, LibMVCS.Config _config, Dictionary<string, LibMVCS.Any> _settings)
        {
            mono_ = _mono;
            settings_ = _settings;
            logger_ = _logger;

            string xml = _config.getField(ModuleName).AsString();
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            MemoryStream ms = new MemoryStream(bytes);
            var xs = new XmlSerializer(typeof(MyConfig));
            config_ = xs.Deserialize(ms) as MyConfig;

            if (!string.IsNullOrEmpty(config_.grpc.address))
            {
                var channel = GrpcChannel.ForAddress(config_.grpc.address, new GrpcChannelOptions
                {
                    HttpHandler = new GrpcWebHandler(new HttpClientHandler())
                });
                _options.setChannel(channel);
            }

            runtime_ = new MyRuntime();
            runtime_.entry = this;
            runtime_.logger = _logger;
            runtime_.config = config_;
            runtime_.mono = mono_;
        }

        public virtual void Preload()
        {
        }


        /// <summary>
        /// 处理实例化的根对象
        /// </summary>
        /// <param name="_root">根对象</param>
        protected void processRoot(GameObject _root)
        {
            if (null == _root)
            {
                logger_.Error("parse [ExportRoot] failed, it is null");
                return;
            }
            logger_.Debug("ready to parse {0}.{1}", ModuleName, _root.name);

            // 从设置中获取主画布参数
            LibMVCS.Any anyMainCanvas;
            if (!settings_.TryGetValue("main.canvas", out anyMainCanvas))
            {
                logger_.Error("the main.canvas not found in settings");
                return;
            }
            Transform mainCanvas = anyMainCanvas.AsObject() as Transform;
            if (null == mainCanvas)
            {
                logger_.Error("the mainCanvas is null");
                return;
            }

            // 查找UI的挂载槽
            Transform slotUi = mainCanvas.Find(config_.ui.slot);
            if (null == slotUi)
            {
                logger_.Error("the slotUi is null");
                return;
            }

            runtime_.ProcessRoot(_root, slotUi);

            logger_.Debug("process {0} success", _root.name);

            foreach (var instance in config_.instances)
            {
                runtime_.CreateInstance(instance.uid, instance.style);
            }
        }


        /// <summary>
        /// 加载UnityAssetBundle
        /// </summary>
        /// <param name="_source"></param>
        /// <param name="_onFinish"></param>
        /// <param name="_onError"></param>
        /// <returns></returns>
        private IEnumerator loadUAB(string _source, System.Action<GameObject> _onFinish, System.Action<LibMVCS.Error> _onError)
        {
            if (_source.Equals("file"))
            {
                yield return loadUABFromFile(_onFinish, _onError);
            }
        }

        /// <summary>
        /// 从文件加载UnityAssetBundle
        /// </summary>
        /// <param name="_onFinish">成功的回调</param>
        /// <param name="_onError">失败的回调</param>
        /// <returns></returns>
        private IEnumerator loadUABFromFile(System.Action<GameObject> _onFinish, System.Action<LibMVCS.Error> _onError)
        {
            // 从设置中获取vendor参数
            LibMVCS.Any vendor;
            if (!settings_.TryGetValue("vendor", out vendor))
            {
                _onError(LibMVCS.Error.NewParamErr("vendor not found in settings"));
                yield break;
            }

            // 从设置中获取datapath参数
            LibMVCS.Any datapath;
            if (!settings_.TryGetValue("datapath", out datapath))
            {
                _onError(LibMVCS.Error.NewParamErr("datapath not found in settings"));
                yield break;
            }

            // 拼接出uab的绝对路径
            string uabpath = Path.Combine(datapath.AsString(), vendor.AsString());
            uabpath = Path.Combine(uabpath, string.Format("uab/module.{0}.uab", MyEntryBase.ModuleName));
            logger_.Debug("ready to load {0}", uabpath);

            // 从文件异步加载uab
            var blr = AssetBundle.LoadFromFileAsync(uabpath);
            yield return blr;
            var bundle = blr.assetBundle;
            if (null == bundle)
            {
                _onError(LibMVCS.Error.NewNullErr("bundle is null"));
                yield break;
            }
            logger_.Debug("GetContent() success");

            // 从包中异步加载根对象
            var alr = bundle.LoadAssetAsync<GameObject>("[ExportRoot]");
            yield return alr;
            var go = alr.asset as GameObject;
            if (null == go)
            {
                _onError(LibMVCS.Error.NewNullErr("gameobject is null"));
                yield break;
            }
            logger_.Debug("LoadAsset([ExportRoot]) success");
            go.SetActive(false);

            // 实例化根对象
            var clone = GameObject.Instantiate<GameObject>(go);
            // 重置transform参数
            clone.transform.SetParent(go.transform.parent);
            clone.transform.localPosition = go.transform.localPosition;
            clone.transform.localRotation = go.transform.localRotation;
            clone.transform.localScale = go.transform.localScale;

            // 调用回调
            _onFinish(clone);

            // 清理资源
            bundle.Unload(false);
            Resources.UnloadUnusedAssets();
        }
    }
}
