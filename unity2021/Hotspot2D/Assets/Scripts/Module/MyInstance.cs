
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.Proto;
using XTC.FMP.MOD.Hotspot2D.LIB.MVCS;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        private MyConfig.Style style_ { get; set; }
        /// <summary>
        /// 应用样式
        /// </summary>
        /// <param name="_style">样式</param>
        public void ApplyStyle(MyConfig.Style _style)
        {
            style_ = _style;
            var layer = rootUI.transform.Find("LayerContainers/layer");
            layer.gameObject.SetActive(false);
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
        public void HandleOpened()
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
