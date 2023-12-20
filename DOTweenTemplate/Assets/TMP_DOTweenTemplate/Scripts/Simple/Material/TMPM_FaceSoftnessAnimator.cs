using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Material;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Simple.Material
{
    /// <summary>
    /// FaceSoftnessのアニメーター
    /// </summary>
    public class TMPM_FaceSoftnessAnimator: AnimatorBase
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
        [SerializeField] private TMPM_FaceSoftnessAnimation faceSoftnessAnimation;

        private void Awake()
        {
            //初期化
            faceSoftnessAnimation.text = text;
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            sq.Append(faceSoftnessAnimation.GetSequence());
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            faceSoftnessAnimation?.Reset();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}