using System;
using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using UnityEngine;
using UnityEngine.Events;

namespace TMP_DOTweenTemplate.Core.Main
{
    /// <summary>
    /// シーケンスアニメーション管理クラス
    /// </summary>
    [Serializable]
    public class SequenceAnimation : IDisposable
    {
        /// <summary>
        /// シーケンスイベント
        /// </summary>
        public SequenceEvent sequenceEvent;
        
        /// <summary>
        /// メインのシーケンス
        /// </summary>
        public Sequence mainSequence = null;

        /// <summary>
        /// シーケンスのイベント初期化
        /// </summary>
        private void SequenceEventInit()
        {
            mainSequence.OnPlay(() => sequenceEvent.onPlay.Invoke());
            mainSequence.OnStart(() => sequenceEvent.onStart.Invoke());
            mainSequence.OnPause(() => sequenceEvent.onPause.Invoke());
            mainSequence.OnKill(() => sequenceEvent.onKill.Invoke());
            mainSequence.OnComplete(() => sequenceEvent.onComplete.Invoke());
        }

        /// <summary>
        /// 初期化(再生しない)
        /// </summary>
        /// <param name="playSequence">シーケンス</param>
        public void Init(Sequence playSequence)
        {
            mainSequence?.Kill();
            mainSequence = DOTween.Sequence();
            mainSequence = playSequence;
            SequenceEventInit();
            mainSequence.Pause();
        }
        
        /// <summary>
        /// 初期化+再生
        /// </summary>
        /// <param name="playSequence">シーケンス</param>
        public void PlayAndInit(Sequence playSequence)
        {
            mainSequence?.Kill();
            mainSequence = DOTween.Sequence();
            mainSequence = playSequence;
            SequenceEventInit();
            mainSequence.Play();
        }

        /// <summary>
        /// 再生
        /// </summary>
        public void Play()
        {
            if(mainSequence == null) Debug.LogError("Please Init Sequence.");
            mainSequence.Play();
        }

        /// <summary>
        /// ポーズ
        /// </summary>
        public void Pause()
        {
            if(mainSequence == null) Debug.LogError("Please Init Sequence.");
            mainSequence.Pause();
        }

        /// <summary>
        /// キル
        /// </summary>
        public void Kill()
        {
            if(mainSequence == null) Debug.LogError("Please Init Sequence.");
            mainSequence.Kill();
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            mainSequence?.Kill();
        }
    }
}