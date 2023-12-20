using System;
using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Material;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Simple.Material
{
    /// <summary>
    /// FaceDilateのアニメーター
    /// </summary>
    public class TMPM_FaceDilateAnimator : AnimatorBase
    {
        /// <summary>
        /// テキスト
        /// </summary>
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI text;
        /// <summary>
        /// アニメーション
        /// </summary>
        [Header("Animation")]
        [SerializeField] private TMPM_FaceDilateAnimation faceDilateAnimation;

        private void Awake()
        {
            //初期化
            faceDilateAnimation.text = text;
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            sq.Append(faceDilateAnimation.GetSequence());
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            faceDilateAnimation?.Reset();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}