
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.Proto;
using XTC.FMP.MOD.Hotspot2D.LIB.MVCS;
using XTC.FMP.LIB.MVCS;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    public class Ui
    {
        public class InfoBox
        {
            public GameObject root;
            public Text title;
            public Text text;
            public GameObject closeButton;
            public GameObject openButton;
        }

        public ScrollRect mainLayer;
        public GameObject hotspot;
        public GameObject board;
        public InfoBox infoBox = new InfoBox();
    }

    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        private Ui ui_ = new Ui();

        private ContentMetaSchema activeContent_;
        private ContentReader contentReader_;

        public MyInstance(string _uid, string _style, MyConfig _config, MyCatalog _catalog, LibMVCS.Logger _logger, Dictionary<string, Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
            : base(_uid, _style, _config, _catalog, _logger, _settings, _entry, _mono, _rootAttachments)
        {
        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        public void HandleCreated()
        {
            contentReader_ = new ContentReader(contentObjectsPool);
            contentReader_.AssetRootPath = settings_["path.assets"].AsString();
            ui_.board = rootUI.transform.Find("Board").gameObject;

            ui_.mainLayer = rootUI.transform.Find("Home/ScrollView").GetComponent<ScrollRect>();
            // 初始化热点
            ui_.hotspot = rootUI.transform.Find("Home/ScrollView/Viewport/mainLayer/hotspot").gameObject;
            ui_.hotspot.SetActive(false);

            // 初始化信息框
            ui_.infoBox.root = rootUI.transform.Find("Home/InfoBox").gameObject;
            ui_.infoBox.root.gameObject.SetActive(false);
            ui_.infoBox.root.transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                ui_.infoBox.root.gameObject.SetActive(false);
            });
            ui_.infoBox.root.transform.Find("OpenButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                openResource();
            });
            ui_.infoBox.openButton = rootUI.transform.Find("Home/InfoBox/OpenButton").gameObject;
            ui_.infoBox.closeButton = rootUI.transform.Find("Home/InfoBox/CloseButton").gameObject;
            ui_.infoBox.closeButton.SetActive(true);
            ui_.infoBox.text = rootUI.transform.Find("Home/InfoBox/ScrollView/Viewport/Content/text").GetComponent<Text>();
            ui_.infoBox.title = rootUI.transform.Find("Home/InfoBox/title").GetComponent<Text>();

            applyStyle().OnFinish = () =>
            {
                applyCatalog();

                var btnBack = rootUI.transform.Find("Board/btnBack").GetComponent<Button>();
                btnBack.onClick.RemoveAllListeners();
                btnBack.onClick.AddListener(() =>
                {
                    logger_.Debug("hotspot off");
                    closeResource();
                });
            };

            // 应用字体
            Any anyFont;
            if (settings_.TryGetValue("font.main", out anyFont))
            {
                var mainFont = anyFont.AsObject() as Font;
                if (null == mainFont)
                    return;
                ui_.infoBox.text.font = mainFont;
                ui_.infoBox.title.font = mainFont;
            }
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
            contentReader_.ContentUri = _uri;

            ui_.board.SetActive(false);
            rootUI.gameObject.SetActive(true);

            if (style_.mainLayer.horizontalAlign == "left")
                ui_.mainLayer.horizontalNormalizedPosition = 0;
            else if (style_.mainLayer.horizontalAlign == "right")
                ui_.mainLayer.horizontalNormalizedPosition = 1;
            else
                ui_.mainLayer.horizontalNormalizedPosition = 0.5f;

            if (style_.mainLayer.verticalAlign == "left")
                ui_.mainLayer.verticalNormalizedPosition = 0;
            else if (style_.mainLayer.verticalAlign == "right")
                ui_.mainLayer.verticalNormalizedPosition = 1;
            else
                ui_.mainLayer.verticalNormalizedPosition = 0.5f;
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);
        }

        public void applyCatalog()
        {
            foreach (var section in catalog_.sectionS)
            {
                var anchor = style_.hotspot.anchor;

                string strHorizontal = "center";
                if (section.kvS.TryGetValue("horizontal", out strHorizontal))
                    anchor.horizontal = strHorizontal;

                string strVertical = "center";
                if (section.kvS.TryGetValue("vertical", out strVertical))
                    anchor.vertical = strHorizontal;

                string strMarginH = "0";
                if (section.kvS.TryGetValue("marginH", out strMarginH))
                    anchor.marginH = strMarginH;

                string strMarginV = "0";
                if (section.kvS.TryGetValue("marginV", out strMarginV))
                    anchor.marginV = strMarginV;

                var clone = GameObject.Instantiate(ui_.hotspot, ui_.hotspot.transform.parent);
                clone.SetActive(true);
                alignByAncor(clone.transform, anchor);

                string strImage = "";
                if (section.kvS.TryGetValue("image", out strImage))
                {
                    loadTextureFromTheme(strImage, (_texture) =>
                    {
                        clone.GetComponent<RawImage>().texture = _texture;
                    }, () => { });
                }

                clone.GetComponent<Button>().onClick.AddListener(() =>
                {
                    activeContent_ = null;
                    if (style_.infoBox.active)
                    {
                        ui_.infoBox.openButton.SetActive(false);
                        ui_.infoBox.text.text = "";
                        ui_.infoBox.title.text = "";
                        ui_.infoBox.root.SetActive(true);
                    }

                    // 读取 content
                    var firstSection = section.contentS.First();
                    string metafile = Path.Combine(firstSection, "meta.json");
                    contentReader_.LoadText(metafile, (_text) =>
                    {
                        try
                        {
                            activeContent_ = JsonConvert.DeserializeObject<ContentMetaSchema>(System.Text.Encoding.UTF8.GetString(_text));
                        }
                        catch (System.Exception ex)
                        {
                            logger_.Exception(ex);
                            return;
                        }

                        if (style_.infoBox.active)
                        {
                            refreshInfoBox();
                            return;
                        }

                        logger_.Debug("hotspot on");
                        openResource();
                    }, () => { });
                });
            }
        }

        public void Forward()
        {
            rootUI.transform.Find("Home").gameObject.SetActive(false);
            ui_.board.SetActive(true);
        }

        public void Back()
        {
            rootUI.transform.Find("Home").gameObject.SetActive(true);
            ui_.board.SetActive(false);
        }

        private CounterSequence applyStyle()
        {
            CounterSequence sequence = new CounterSequence(0);

            // 应用主层样式
            sequence.Dial();
            loadTextureFromTheme(style_.mainLayer.image, (_texture) =>
            {
                var imgLayer = rootUI.transform.Find("Home/ScrollView/Viewport/mainLayer").GetComponent<RawImage>();
                imgLayer.texture = _texture;
                imgLayer.SetNativeSize();
                sequence.Tick();
            }, () =>
            {
                sequence.Tick();
            });

            // 应用扩展层样式
            foreach (var layer in style_.extraLayers)
            {
                //TODO
                /*
                var layerClone = GameObject.Instantiate(tLayer.gameObject, tLayer.parent);
                sequence.Dial();
                loadTextureFromTheme(layer.image, (_texture) =>
                {
                    var imgLayer = layerClone.transform.Find("Viewport/image").GetComponent<RawImage>();
                    imgLayer.texture = _texture;
                    imgLayer.SetNativeSize();
                    sequence.Tick();
                }, () => { });
                layerClone.SetActive(true);
                */
            }

            // 应用热点样式
            loadTextureFromTheme(style_.hotspot.image, (_texture) =>
            {
                var img = ui_.hotspot.GetComponent<RawImage>();
                img.texture = _texture;
                sequence.Tick();
            }, () =>
            {
                sequence.Tick();
            });

            Color debugFrameColor = Color.clear;
            if (!ColorUtility.TryParseHtmlString(style_.hotspot.debugFrameColor, out debugFrameColor))
                debugFrameColor = Color.clear;
            ui_.hotspot.transform.Find("debugFrame").GetComponent<Image>().color = debugFrameColor;

            // 应用返回样式
            var btnBack = rootUI.transform.Find("Board/btnBack");
            alignByAncor(btnBack, style_.board.backButton.anchor);
            sequence.Dial();
            loadTextureFromTheme(style_.board.backButton.image, (_texture) =>
            {
                var img = btnBack.GetComponent<RawImage>();
                img.texture = _texture;
                sequence.Tick();
            }, () =>
            {
                sequence.Tick();
            });

            // 应用信息框面板样式
            alignByAncor(ui_.infoBox.root.transform, style_.infoBox.panel.anchor);
            sequence.Dial();
            loadTextureFromTheme(style_.infoBox.panel.image, (_texture) =>
            {
                var img = ui_.infoBox.root.GetComponent<RawImage>();
                img.texture = _texture;
                sequence.Tick();
            }, () =>
            {
                sequence.Tick();
            });

            // 应用信息框关闭按钮样式
            alignByAncor(ui_.infoBox.closeButton.transform, style_.infoBox.closeButton.anchor);
            sequence.Dial();
            loadTextureFromTheme(style_.infoBox.closeButton.image, (_texture) =>
            {
                var img = ui_.infoBox.closeButton.GetComponent<RawImage>();
                img.texture = _texture;
                sequence.Tick();
            }, () =>
            {
                sequence.Tick();
            });

            // 应用信息框打开按钮样式
            alignByAncor(ui_.infoBox.openButton.transform, style_.infoBox.openButton.anchor);
            sequence.Dial();
            loadTextureFromTheme(style_.infoBox.openButton.image, (_texture) =>
            {
                var img = ui_.infoBox.openButton.transform.GetComponent<RawImage>();
                img.texture = _texture;
                sequence.Tick();
            }, () =>
            {
                sequence.Tick();
            });

            return sequence;
        }

        private void refreshInfoBox()
        {
            string text = "";
            string title = "";
            if (null != activeContent_)
            {
                text = activeContent_.description;
                title = activeContent_.title;
            }
            ui_.infoBox.text.text = text;
            ui_.infoBox.title.text = title;
            ui_.infoBox.openButton.SetActive(null != activeContent_);
        }

        /// <summary>
        /// 打开资源
        /// </summary>
        private void openResource()
        {
            if (null == activeContent_)
            {
                logger_.Error("None Content is active");
                return;
            }

            if (string.IsNullOrWhiteSpace(activeContent_.foreign_bundle_uuid))
            {
                logger_.Error("bundle is null or whitespace");
                return;
            }

            string strValue = "";
            activeContent_.kvS.TryGetValue(style_.hotspot.key, out strValue);
            if (string.IsNullOrWhiteSpace(strValue))
            {
                logger_.Error("content.kv.{0} is null or whitespace", MyEntryBase.ModuleName);
                return;
            }

            Dictionary<string, object> variableS = new Dictionary<string, object>();
            variableS["{{uri}}"] = string.Format("{0}/_resources/{1}", activeContent_.foreign_bundle_uuid, strValue);
            publishSubjects(style_.hotspot.onSubjects, variableS);
        }

        /// <summary>
        /// 关闭资源
        /// </summary>
        private void closeResource()
        {
            Dictionary<string, object> variableS = new Dictionary<string, object>();
            publishSubjects(style_.hotspot.offSubjects, variableS);
        }
    }
}
