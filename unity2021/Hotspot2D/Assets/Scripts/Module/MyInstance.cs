
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

        public GameObject hotspot;
        public InfoBox infoBox = new InfoBox();
    }

    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        private Ui ui_ = new Ui();

        private MyContent activeContent_;
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

            // 初始化热点
            ui_.hotspot = rootUI.transform.Find("Home/HotspotContainers/hotspot").gameObject;
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

            rootUI.gameObject.SetActive(true);
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
                    loadSpriteFromTheme(strImage, (_sprite) =>
                    {
                        clone.GetComponent<Image>().sprite = _sprite;
                    });
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
                            activeContent_ = JsonConvert.DeserializeObject<MyContent>(System.Text.Encoding.UTF8.GetString(_text));
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
                    });
                });
            }
        }

        public void Forward()
        {
            rootUI.transform.Find("Home").gameObject.SetActive(false);
        }

        public void Back()
        {
            rootUI.transform.Find("Home").gameObject.SetActive(true);
        }

        private CounterSequence applyStyle()
        {
            CounterSequence sequence = new CounterSequence(0);

            // 应用层样式
            var tLayer = rootUI.transform.Find("Home/LayerContainers/layer");
            tLayer.gameObject.SetActive(false);
            foreach (var layer in style_.layers)
            {
                var layerClone = GameObject.Instantiate(tLayer.gameObject, tLayer.parent);
                sequence.Dial();
                loadSpriteFromTheme(layer.image, (_sprite) =>
                {
                    var imgLayer = layerClone.GetComponent<Image>();
                    imgLayer.sprite = _sprite;
                    sequence.Tick();
                });
                layerClone.SetActive(true);
            }

            // 应用热点样式
            loadSpriteFromTheme(style_.hotspot.image, (_sprite) =>
            {
                var img = ui_.hotspot.GetComponent<Image>();
                img.sprite = _sprite;
                sequence.Tick();
            });

            // 应用返回样式
            var btnBack = rootUI.transform.Find("Board/btnBack");
            alignByAncor(btnBack, style_.board.backButton.anchor);
            sequence.Dial();
            loadSpriteFromTheme(style_.board.backButton.image, (_sprite) =>
            {
                var img = btnBack.GetComponent<Image>();
                img.sprite = _sprite;
                sequence.Tick();
            });

            // 应用信息框面板样式
            alignByAncor(ui_.infoBox.root.transform, style_.infoBox.panel.anchor);
            sequence.Dial();
            loadSpriteFromTheme(style_.infoBox.panel.image, (_sprite) =>
            {
                var img = ui_.infoBox.root.GetComponent<Image>();
                img.sprite = _sprite;
                sequence.Tick();
            });

            // 应用信息框关闭按钮样式
            alignByAncor(ui_.infoBox.closeButton.transform, style_.infoBox.closeButton.anchor);
            sequence.Dial();
            loadSpriteFromTheme(style_.infoBox.closeButton.image, (_sprite) =>
            {
                var img = ui_.infoBox.closeButton.GetComponent<Image>();
                img.sprite = _sprite;
                sequence.Tick();
            });

            // 应用信息框打开按钮样式
            alignByAncor(ui_.infoBox.openButton.transform, style_.infoBox.openButton.anchor);
            sequence.Dial();
            loadSpriteFromTheme(style_.infoBox.openButton.image, (_sprite) =>
            {
                var img = ui_.infoBox.openButton.transform.GetComponent<Image>();
                img.sprite = _sprite;
                sequence.Tick();
            });

            return sequence;
        }

        private void publishSubject(MyConfig.Subject _subject, Dictionary<string, string> _replaces)
        {
            var dummyModel = (entry_ as MyEntry).getDummyModel();
            var data = new Dictionary<string, object>();
            foreach (var parameter in _subject.parameters)
            {
                if (parameter.type.Equals("string"))
                {
                    var strValue = parameter.value;
                    foreach (var pair in _replaces)
                    {
                        strValue = strValue.Replace(pair.Key, pair.Value);
                    }
                    data[parameter.key] = strValue;
                }
                else if (parameter.type.Equals("int"))
                    data[parameter.key] = int.Parse(parameter.value);
                else if (parameter.type.Equals("float"))
                    data[parameter.key] = float.Parse(parameter.value);
                else if (parameter.type.Equals("bool"))
                    data[parameter.key] = bool.Parse(parameter.value);
            }
            dummyModel.Publish(_subject.message, data);
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

            if (string.IsNullOrWhiteSpace(activeContent_.bundle))
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

            Dictionary<string, string> replaces = new Dictionary<string, string>();
            replaces["{{resource_uri}}"] = string.Format("{0}/_resources/{1}", activeContent_.bundle, strValue);
            foreach (var subject in style_.hotspot.onSubjects)
            {
                publishSubject(subject, replaces);
            }
        }

        /// <summary>
        /// 关闭资源
        /// </summary>
        private void closeResource()
        {

            Dictionary<string, string> replaces = new Dictionary<string, string>();
            foreach (var subject in style_.hotspot.offSubjects)
            {
                publishSubject(subject, replaces);
            }
        }
    }
}
