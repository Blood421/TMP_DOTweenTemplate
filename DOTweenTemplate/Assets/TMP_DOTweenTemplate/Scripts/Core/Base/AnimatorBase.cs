using System;
using TMP_DOTweenTemplate.Core.Main;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Base
{
    /// <summary>
    /// アニメーションを実行するアニメーターのベースクラス
    /// </summary>
    public abstract class AnimatorBase : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// メインのシーケンス
        /// </summary>
        [Header("Event")]
        [SerializeField] protected SequenceAnimation mainSequence;
        
        /// <summary>
        /// シーケンスイベントを取得
        /// </summary>
        /// <returns>シーケンスイベント</returns>
        public SequenceEvent Event() => mainSequence.sequenceEvent;
        
        /// <summary>
        /// 再生
        /// </summary>
        public abstract void Play();
        
        /// <summary>
        /// 破棄
        /// </summary>
        public abstract void Dispose();
    }
}