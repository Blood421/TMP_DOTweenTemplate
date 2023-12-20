using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Simple.Pro
{
    /// <summary>
    /// パンチ回転のアニメーター
    /// </summary>
    public class TMP_CharPunchRotateAnimator : AnimatorBase
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
        [SerializeField] private TMP_CharPunchRotateAnimation charPunchRotateAnimation;

        private void Awake()
        {
            //初期化
            charPunchRotateAnimation.text = text;
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            sq.Append(charPunchRotateAnimation.GetSequence());
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            charPunchRotateAnimation?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}