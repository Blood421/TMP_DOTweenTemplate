using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Simple.Pro
{
    /// <summary>
    /// パンチ拡縮のアニメーター
    /// </summary>
    public class TMP_CharPunchScaleAnimator : AnimatorBase
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
        [Header("---Can't Use Easing---")]
        [SerializeField] private TMP_CharPunchScaleAnimation charPunchScaleAnimation;

        private void Awake()
        {
            //初期化
            charPunchScaleAnimation.text = text;
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            sq.Append(charPunchScaleAnimation.GetSequence());
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            charPunchScaleAnimation?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}