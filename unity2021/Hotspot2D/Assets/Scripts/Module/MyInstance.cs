
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.Proto;
using XTC.FMP.MOD.Hotspot2D.LIB.MVCS;
using XTC.FMP.LIB.MVCS;
using System.Collections.Generic;
using XTC.FMP.MOD.Assloud.LIB.MVCS;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        public MyInstance(string _uid, string _style, MyConfig _config, LibMVCS.Logger _logger, Dictionary<string, Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
            : base(_uid, _style, _config, _logger, _settings, _entry, _mono, _rootAttachments)
        {
        }

        /// <summary>
        /// 应用样式
        /// </summary>
        /// <param name="_style">样式</param>
        public void ApplyStyle()
        {
            var layer = rootUI.transform.Find("LayerContainers/layer");
            layer.gameObject.SetActive(false);
            var hotspot = rootUI.transform.Find("hotspot");
            hotspot.gameObject.SetActive(false);
        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        public void HandleCreated()
        {
            createLayers();
        }

        /// <summary>
        /// 当被删除时
        /// </summary>
        public void HandleDeleted()
        {
        }

        /// <summary>
        /// 当被打开时
        /// </summary>
        public void HandleOpened(string _source, string _uri)
        {
            rootUI.gameObject.SetActive(true);
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);
        }

        public void SlotRefreshHotspots(Model.Status _status, object _data)
        {
            var statusUID = ContentModel.NAME + "." + uid + ".Status";
            var status = _status.Access(statusUID) as ContentModel.ContentStatus;
            if(null == status)
            {
                logger_.Error("status: {0} not found", statusUID);
            }

            var goHotspot = rootUI.transform.Find("hotspot").gameObject;
            foreach (var hotspot in style_.hotspots)
            {
                var clone = GameObject.Instantiate(goHotspot, goHotspot.transform.parent);
                clone.SetActive(true);
                var rt = clone.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(hotspot.x, hotspot.y);
                var imgHotspot = clone.GetComponent<Image>();
                loadSpriteFromTheme(hotspot.image, (_sprite) =>
                {
                    imgHotspot.sprite = _sprite;
                });
                imgHotspot.SetNativeSize();
            }
        }

        private void createLayers()
        {
            var rLayer = rootUI.transform.Find("LayerContainers/layer");
            foreach (var layer in style_.layers)
            {
                var layerClone = GameObject.Instantiate(rLayer.gameObject, rLayer.parent);
                var imgLayer = layerClone.GetComponent<Image>();
                loadSpriteFromTheme(layer.image, (_sprite) =>
                {
                    imgLayer.sprite = _sprite;
                });
                layerClone.SetActive(true);
            }
        }
    }
}
