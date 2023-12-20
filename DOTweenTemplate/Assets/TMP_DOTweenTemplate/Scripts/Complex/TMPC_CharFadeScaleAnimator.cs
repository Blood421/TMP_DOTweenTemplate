using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Complex
{
    /// <summary>
    /// Fade + Scaleのアニメーター
    /// </summary>
    public class TMPC_CharFadeScaleAnimator : AnimatorBase
    {
        /// <summary>
        /// メインのテキスト
        /// </summary>
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI text;
        
        
        /// <summary>
        /// AwakeでBeforeAnimationするかどうか
        /// </summary>
        [Header("Before")]
        [SerializeField] private bool isBeforeAnimationPlayOnAwake = false;
        /// <summary>
        /// Before Fadeアニメーション
        /// </summary>
        [SerializeField] private TMP_CharFadeAnimation beforeCharFadeAnimation;
        /// <summary>
        /// Before Scaleアニメーション
        /// </summary>
        [SerializeField] private TMP_CharScaleAnimation beforeCharScaleAnimation;
        
        /// <summary>
        /// Now Fadeアニメーション
        /// </summary>
        [Header("Now")]
        [SerializeField] private TMP_CharFadeAnimation nowCharFadeAnimation;
        /// <summary>
        /// Now Scaleアニメーション
        /// </summary>
        [SerializeField] private TMP_CharScaleAnimation nowCharScaleAnimation;
        
        /// <summary>
        /// Nowの後Afterを実行するまでの遅延
        /// </summary>
        [Header("After")]
        [SerializeField] private float afterAnimationDelay = 1f;
        /// <summary>
        /// After Fadeアニメーション
        /// </summary>
        [SerializeField] private TMP_CharFadeAnimation afterCharFadeAnimation;
        /// <summary>
        /// After Scaleアニメーション
        /// </summary>
        [SerializeField] private TMP_CharScaleAnimation afterCharScaleAnimation;
        
        /// <summary>
        /// メインのDOTweenTMPAnimator
        /// </summary>
        DOTweenTMPAnimator animator;
        private void Awake()
        {
            //初期化
            beforeCharFadeAnimation.text = text;
            beforeCharScaleAnimation.text = text;
            nowCharFadeAnimation.text = text;
            nowCharScaleAnimation.text = text;
            afterCharFadeAnimation.text = text;
            afterCharScaleAnimation.text = text;
            
            //before
            if(!isBeforeAnimationPlayOnAwake)return;
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);
            
            sq.Join(beforeCharFadeAnimation.GetSequence(animator));
            sq.Join(beforeCharScaleAnimation.GetSequence(animator));

        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);
            
            //before
            sq.Join(beforeCharFadeAnimation.GetSequence(animator));
            sq.Join(beforeCharScaleAnimation.GetSequence(animator));
            
            //now
            sq.Append(nowCharFadeAnimation.GetSequence(animator));
            sq.Join(nowCharScaleAnimation.GetSequence(animator));

            if (afterAnimationDelay >= 0)
            {
                //after
                sq.Append(afterCharFadeAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
                sq.Join(afterCharScaleAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
            }
            
            //event
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            beforeCharFadeAnimation?.Dispose();
            beforeCharScaleAnimation?.Dispose();
            nowCharFadeAnimation?.Dispose();
            nowCharScaleAnimation?.Dispose();
            afterCharFadeAnimation?.Dispose();
            afterCharScaleAnimation?.Dispose();
            
            animator?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}