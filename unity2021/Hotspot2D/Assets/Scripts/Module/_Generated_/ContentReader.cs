
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.59.0.  DO NOT EDIT!
//*************************************************************************************

using System;
using System.IO;
using UnityEngine;

namespace XTC.FMP.MOD.Hotspot2D.LIB.Unity
{
    /// <summary>
    /// 内容读取器
    /// </summary>
    public class ContentReader
    {
        protected ObjectsPool contentObjectPool_ { get; private set; }

        public ContentReader(ObjectsPool _contentObjectPool)
        {
            contentObjectPool_ = _contentObjectPool;
        }

        /// <summary>
        /// 资产库的根目录的绝对路径
        /// </summary>
        public string AssetRootPath { get; set; }

        /// <summary>
        /// 内容的短路径，格式为 包名/内容名
        /// </summary>
        public string ContentUri { get; set; }

        /// <summary>
        /// 加载精灵
        /// </summary>
        /// <param name="_file">文件相对路径，相对于包含format.json的资源文件夹</param>
        /// <param name="_onFinish"></param>
        public void LoadSprite(string _file, Action<Sprite> _onFinish)
        {
            string dir = Path.Combine(AssetRootPath, ContentUri);
            string filefullpath = Path.Combine(dir, _file);
            contentObjectPool_.LoadTexture(filefullpath, null, (_texture) =>
            {
                var sprite = Sprite.Create(_texture as Texture2D, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));
                _onFinish(sprite);
            });
        }

        /// <summary>
        /// 加载文本
        /// </summary>
        /// <param name="_file">文件相对路径，相对于包含format.json的资源文件夹</param>
        /// <param name="_onFinish"></param>
        public void LoadText(string _file, Action<byte[]> _onFinish)
        {
            string dir = Path.Combine(AssetRootPath, ContentUri);
            string filefullpath = Path.Combine(dir, _file);
            contentObjectPool_.LoadText(filefullpath, null, _onFinish);
        }
    }
}

