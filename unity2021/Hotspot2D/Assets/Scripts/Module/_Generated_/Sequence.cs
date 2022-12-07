
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.67.0.  DO NOT EDIT!
//*************************************************************************************

using System.Collections.Generic;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    /// <summary>
    /// 序列类基类
    /// </summary>
    public class SequenceBase
    {
        public System.Action OnFinish;
    } //class

    /// <summary>
    /// 计数器序列
    /// </summary>
    public class CounterSequence : SequenceBase
    {
        public int total { get; protected set; }
        public int ticker { get; protected set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_total">初始化总数</param>
        public CounterSequence(int _total)
        {
            total = _total;
            ticker = 0;
        }

        /// <summary>
        /// 总数加一
        /// </summary>
        public void Dial()
        {
            total += 1;
        }

        /// <summary>
        /// 计数器加一
        /// </summary>
        public void Tick()
        {
            if (ticker >= total)
                return;

            ticker += 1;
            if (ticker >= total)
            {
                if (null != OnFinish)
                {
                    OnFinish();
                }
            }
        }

    } //class
} //namespace

