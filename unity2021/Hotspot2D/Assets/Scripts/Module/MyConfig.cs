
using System.Xml.Serialization;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class MyConfig : MyConfigBase
    {
        public class Board
        {
            [XmlAttribute("buttonBackIcon")]
            public string buttonBackIcon { get; set; } = "";
        }

        public class Layer
        {
            [XmlAttribute("image")]
            public string image { get; set; } = "";
        }

        public class Hotspot
        {
            [XmlAttribute("image")]
            public string image { get; set; } = "";
            [XmlAttribute("x")]
            public float x { get; set; } = 0f; 
            [XmlAttribute("y")]
            public float y { get; set; } = 0f;
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

            [XmlArray("Layers"), XmlArrayItem("Layer")]
            public Layer[] layers{ get; set; } = new Layer[0];
            [XmlArray("Hotspots"), XmlArrayItem("Hotspot")]
            public Hotspot[] hotspots { get; set; } = new Hotspot[0];
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}

