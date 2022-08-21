
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.26.0.  DO NOT EDIT!
//*************************************************************************************

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    public class MySubjectBase
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// data["style"] = "default";
        /// model.Publish(/XTC/Hotspot2D/Create, data);
        /// </example>
        public const string Create = "/XTC/Hotspot2D/Create";

        /// <summary>
        /// 打开
        /// </summary>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// data["source"] = "file";
        /// data["uri"] = "";
        /// data["delay"] = 0f;
        /// model.Publish(/XTC/Hotspot2D/Open, data);
        /// </example>
        public const string Open = "/XTC/Hotspot2D/Open";

        /// <summary>
        /// 关闭
        /// </summary>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// data["delay"] = 0f;
        /// model.Publish(/XTC/Hotspot2D/Close, data);
        /// </example>
        public const string Close = "/XTC/Hotspot2D/Close";

        /// <summary>
        /// 销毁
        /// </summary>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// model.Publish(/XTC/Hotspot2D/Close, data);
        /// </example>
        public const string Delete = "/XTC/Hotspot2D/Delete";
    }
}
