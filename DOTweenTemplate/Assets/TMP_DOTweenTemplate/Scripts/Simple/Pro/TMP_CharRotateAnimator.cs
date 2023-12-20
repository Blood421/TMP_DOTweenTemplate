using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Simple.Pro
{
    /// <summary>
    /// 回転のアニメーター
    /// </summary>
    public class TMP_CharRotateAnimator : AnimatorBase
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
        [SerializeField] private TMP_CharRotateAnimation charRotateAnimation;

        private void Awake()
        {
            //初期化
            charRotateAnimation.text = text;
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            sq.Append(charRotateAnimation.GetSequence());
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            charRotateAnimation?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}