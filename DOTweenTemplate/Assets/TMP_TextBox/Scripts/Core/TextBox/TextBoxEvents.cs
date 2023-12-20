using System;
using UnityEngine.Events;

namespace TMP_TextBox.Core.TextBox
{
    /// <summary>
    /// テキストボックス用のイベントデータ
    /// </summary>
    [Serializable]
    public class TextBoxEvents
    {
        /// <summary>
        /// テキストボックスが開始したイベント
        /// </summary>
        public UnityEvent onStart = new UnityEvent();
        
        /// <summary>
        /// テキストボックスが終了したイベント
        /// </summary>
        public UnityEvent onEnd = new UnityEvent();
        
        /// <summary>
        /// 文字表示が始まったときに呼ばれるイベント
        /// key-イベントキー文字列
        /// index-文字配列の表示されるindex番号
        /// </summary>
        public UnityEvent<(string key,int index)> onStringShowStart = new UnityEvent<(string key,int index)>();
        
        /// <summary>
        /// 文字表示が終わったときに呼ばれるイベント
        /// key-イベントキー文字列
        /// index-文字配列の表示されていたindex番号
        /// </summary>
        public UnityEvent<(string key,int index)> onStringShowEnd = new UnityEvent<(string key,int index)>();
        
        /// <summary>
        /// 表示スキップがされたときに呼ばれるイベント
        /// </summary>
        public UnityEvent onStringShowSkip = new UnityEvent();
    }
}