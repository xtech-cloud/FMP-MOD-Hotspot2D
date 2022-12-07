
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.68.0.  DO NOT EDIT!
//*************************************************************************************

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Hotspot2D.LIB.Bridge;
using XTC.FMP.MOD.Hotspot2D.LIB.MVCS;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    /// <summary>
    /// 虚拟视图基类，用于处理消息订阅
    /// </summary>
    public class DummyViewBase : LibMVCS.View
    {
        public const string NAME = "XTC.FMP.MOD.Hotspot2D.LIB.Unity.DummyView";
        public MyRuntime runtime { get; set; }

        protected DummyModel model_ { get; set; }

        public DummyViewBase(string _uid) : base(_uid)
        {
        }

        protected override void preSetup()
        {
            model_ = findModel(DummyModelBase.NAME) as DummyModel;
        }

        protected override void setup()
        {
            addSubscriber(MySubjectBase.Create, handleCreate);
            addSubscriber(MySubjectBase.Open, handleOpen);
            addSubscriber(MySubjectBase.Show, handleShow);
            addSubscriber(MySubjectBase.Hide, handleHide);
            addSubscriber(MySubjectBase.Close, handleClose);
            addSubscriber(MySubjectBase.Delete, handleDelete);
            addSubscriber("/Bootloader/Step/Execute", handleBootloaderStepExecute);
        }

        private void handleCreate(LibMVCS.Model.Status _status, object _data)
        {
            getLogger().Debug("handle create instance of {0} with data: {1}", MyEntryBase.ModuleName, JsonConvert.SerializeObject(_data));
            string uid = "";
            string style = "";
            string uiSlot = "";
            try
            {
                Dictionary<string, object> data = _data as Dictionary<string, object>;
                uid = (string)data["uid"];
                style = (string)data["style"];
                uiSlot = (string)data["uiSlot"];
            }
            catch (Exception ex)
            {
                getLogger().Exception(ex);
            }
            runtime.CreateInstanceAsync(uid, style, uiSlot, (_instance)=>
            {
            });
        }

        private void handleOpen(LibMVCS.Model.Status _status, object _data)
        {
            getLogger().Debug("handle open instance of {0} with data: {1}", MyEntryBase.ModuleName, JsonConvert.SerializeObject(_data));
            string uid = "";
            string source = "";
            string uri = "";
            float delay = 0f;
            try
            {
                Dictionary<string, object> data = _data as Dictionary<string, object>;
                uid = (string)data["uid"];
                source = (string)data["source"];
                uri = (string)data["uri"];
                delay = (float)data["delay"];
            }
            catch (Exception ex)
            {
                getLogger().Exception(ex);
            }
            runtime.OpenInstanceAsync(uid, source, uri, delay);
        }

        private void handleShow(LibMVCS.Model.Status _status, object _data)
        {
            getLogger().Debug("handle show instance of {0} with data: {1}", MyEntryBase.ModuleName, JsonConvert.SerializeObject(_data));
            string uid = "";
            float delay = 0f;
            try
            {
                Dictionary<string, object> data = _data as Dictionary<string, object>;
                uid = (string)data["uid"];
                delay = (float)data["delay"];
            }
            catch (Exception ex)
            {
                getLogger().Exception(ex);
            }
            runtime.ShowInstanceAsync(uid, delay);
        }

        private void handleHide(LibMVCS.Model.Status _status, object _data)
        {
            getLogger().Debug("handle hide instance of {0} with data: {1}", MyEntryBase.ModuleName, JsonConvert.SerializeObject(_data));
            string uid = "";
            float delay = 0f;
            try
            {
                Dictionary<string, object> data = _data as Dictionary<string, object>;
                uid = (string)data["uid"];
                delay = (float)data["delay"];
            }
            catch (Exception ex)
            {
                getLogger().Exception(ex);
            }
            runtime.HideInstanceAsync(uid, delay);
        }

        private void handleClose(LibMVCS.Model.Status _status, object _data)
        {
            getLogger().Debug("handle close instance of {0} with data: {1}", MyEntryBase.ModuleName, JsonConvert.SerializeObject(_data));
            string uid = "";
            float delay = 0f;
            try
            {
                Dictionary<string, object> data = _data as Dictionary<string, object>;
                uid = (string)data["uid"];
                delay = (float)data["delay"];
            }
            catch (Exception ex)
            {
                getLogger().Exception(ex);
            }
            runtime.CloseInstanceAsync(uid, delay);
        }

        private void handleDelete(LibMVCS.Model.Status _status, object _data)
        {
            getLogger().Debug("handle delete instance of {0} with data: {1}", MyEntryBase.ModuleName, JsonConvert.SerializeObject(_data));
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
            runtime.DeleteInstanceAsync(uid);
        }

        private void handleBootloaderStepExecute(LibMVCS.Model.Status _status, object _data)
        {
            string module = _data as string;
            if (!MyEntryBase.ModuleName.Equals(module))
                return;

            model_.Publish("/Bootloader/Step/Finish", MyEntryBase.ModuleName);

            /*
            var signal = new LibMVCS.Signal(model_);
            signal.Connect((_status, _data) =>
            {
                getLogger().Info($"receive signal: {_data}");
            });
            signal.Emit("test");
            */
        }
    }
}

