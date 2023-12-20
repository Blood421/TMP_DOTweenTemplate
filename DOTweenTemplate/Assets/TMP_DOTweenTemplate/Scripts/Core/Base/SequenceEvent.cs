using System;
using UnityEngine.Events;

namespace TMP_DOTweenTemplate.Core.Base
{
    /// <summary>
    /// シーケンスのイベントをまとめたクラス
    /// </summary>
    [Serializable]
    public class SequenceEvent
    {
        /// <summary>
        /// プレイ時のイベント
        /// </summary>
        public UnityEvent onPlay = new UnityEvent();
        
        /// <summary>
        /// スタート時のイベント
        /// </summary>
        public UnityEvent onStart = new UnityEvent();
        
        /// <summary>
        /// ポーズ時のイベント
        /// </summary>
        public UnityEvent onPause = new UnityEvent();
        
        /// <summary>
        /// キル時のイベント
        /// </summary>
        public UnityEvent onKill = new UnityEvent();
        
        /// <summary>
        /// 完了時のイベント
        /// </summary>
        public UnityEvent onComplete = new UnityEvent();
    }
}