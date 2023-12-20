using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Simple.Pro
{
    /// <summary>
    /// シェイク回転のアニメーター
    /// </summary>
    public class TMP_CharShakeRotateAnimator : AnimatorBase
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
        [SerializeField] private TMP_CharShakeRotateAnimation charShakeRotateAnimation;

        private void Awake()
        {
            //初期化
            charShakeRotateAnimation.text = text;
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            sq.Append(charShakeRotateAnimation.GetSequence());
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            charShakeRotateAnimation?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}