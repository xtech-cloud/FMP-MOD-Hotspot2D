
using System.Xml.Serialization;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class MyConfig : MyConfigBase
    {
        public class InfoBox
        {
            [XmlAttribute("active")]
            public bool active { get; set; } = false;
            [XmlElement("Panel")]
            public UiElement panel { get; set; } = new UiElement();
            [XmlElement("CloseButton")]
            public UiElement closeButton { get; set; } = new UiElement();
            [XmlElement("OpenButton")]
            public UiElement openButton { get; set; } = new UiElement();
        }

        public class Board
        {
            [XmlElement("BackButton")]
            public UiElement backButton { get; set; } = new UiElement();
        }

        public class Layer
        {
            [XmlAttribute("image")]
            public string image { get; set; } = "";
            [XmlAttribute("horizontalAlign")]
            public string horizontalAlign { get; set; } = "";
            [XmlAttribute("verticallAlign")]
            public string verticalAlign { get; set; } = "";
        }

        public class Hotspot : UiElement
        {
            /// <summary>
            /// Content中键值对的键
            /// </summary>
            [XmlAttribute("key")]
            public string key { get; set; } = "";
            [XmlAttribute("debugFrameColor")]
            public string debugFrameColor { get; set; } = "#00000000";
            [XmlArray("OnSubjects"), XmlArrayItem("Subject")]
            public Subject[] onSubjects { get; set; } = new Subject[0];

            [XmlArray("OffSubjects"), XmlArrayItem("Subject")]
            public Subject[] offSubjects { get; set; } = new Subject[0];
        }

        public class Style
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";
            [XmlElement("Board")]
            public Board board { get; set; } = new Board();
            [XmlElement("MainLayer")]
            public Layer mainLayer { get; set; } = new Layer();

            [XmlArray("ExtraLayerS"), XmlArrayItem("ExtraLayer")]
            public Layer[] extraLayers { get; set; } = new Layer[0];
            [XmlElement("Hotspot")]
            public Hotspot hotspot { get; set; } = new Hotspot();
            [XmlElement("InfoBox")]
            public InfoBox infoBox { get; set; } = new InfoBox();
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}

