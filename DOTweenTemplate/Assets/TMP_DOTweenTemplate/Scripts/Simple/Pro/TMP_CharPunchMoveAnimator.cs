using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Simple.Pro
{
    /// <summary>
    /// パンチ移動のアニメーター
    /// </summary>
    public class TMP_CharPunchMoveAnimator : AnimatorBase
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
        [SerializeField] private TMP_CharPunchMoveAnimation charPunchMoveAnimation;

        private void Awake()
        {
            //初期化
            charPunchMoveAnimation.text = text;
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            sq.Append(charPunchMoveAnimation.GetSequence());
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            charPunchMoveAnimation?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}