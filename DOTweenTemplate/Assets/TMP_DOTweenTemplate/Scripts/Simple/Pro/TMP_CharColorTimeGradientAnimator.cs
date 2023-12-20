using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Simple.Pro
{
    /// <summary>
    /// 色の時間グラデーションのアニメーター
    /// </summary>
    public class TMP_CharColorTimeGradientAnimator : AnimatorBase
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
        [SerializeField] private TMP_CharColorTimeGradientAnimation charColorTimeGradientAnimation;

        private void Awake()
        {
            //初期化
            charColorTimeGradientAnimation.text = text;
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            sq.Append(charColorTimeGradientAnimation.GetSequence());
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            charColorTimeGradientAnimation?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}