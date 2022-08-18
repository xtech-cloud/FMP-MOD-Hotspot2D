
using System;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.Bridge;
using XTC.FMP.MOD.Hotspot2D.LIB.MVCS;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using XTC.FMP.LIB.MVCS;
using Newtonsoft.Json;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    /// <summary>
    /// 虚拟视图，用于处理消息订阅
    /// </summary>
    public class DummyView : DummyViewBase
    {
        public DummyView(string _uid) : base(_uid)
        {
        }

        protected override void setup()
        {
            base.setup();
            addSubscriber(MySubject.Forward, handleForward);
            addSubscriber(MySubject.Back, handleBack);
        }

        private void handleForward(Model.Status _status, object _data)
        {
            getLogger().Debug("handle forward instance of {0} with data: {1}", MyEntryBase.ModuleName, JsonConvert.SerializeObject(_data));
            string uid = "";
            try
            {
                Dictionary<string, object> data = _data as Dictionary<string, object>;
                uid = (string)data["uid"];
            }
            catch (Exception ex)
            {
                getLogger().Exception(ex);
            }
            MyInstance instance;
            if (!runtime.instances.TryGetValue(uid, out instance))
                return;
            instance.Forward();
        }

        private void handleBack(Model.Status _status, object _data)
        {
            getLogger().Debug("handle back instance of {0} with data: {1}", MyEntryBase.ModuleName, JsonConvert.SerializeObject(_data));
            string uid = "";
            try
            {
                Dictionary<string, object> data = _data as Dictionary<string, object>;
                uid = (string)data["uid"];
            }
            catch (Exception ex)
            {
                getLogger().Exception(ex);
            }
            MyInstance instance;
            if (!runtime.instances.TryGetValue(uid, out instance))
                return;
            instance.Back();
        }
    }
}

