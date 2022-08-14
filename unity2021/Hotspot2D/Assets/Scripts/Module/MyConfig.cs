
using System.Xml.Serialization;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class MyConfig : MyConfigBase
    {
        public class Layer
        {
            [XmlAttribute("image")]
            public string image { get; set; } = "";
        }

        public class Style
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";

            [XmlArray("Layers"), XmlArrayItem("Layer")]
            public Layer[] layers{ get; set; } = new Layer[0];
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}

