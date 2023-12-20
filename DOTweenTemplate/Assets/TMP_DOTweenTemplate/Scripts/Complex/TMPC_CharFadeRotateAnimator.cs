using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Complex
{
    /// <summary>
    /// Fade + Rotateのアニメーター
    /// </summary>
    public class TMPC_CharFadeRotateAnimator : AnimatorBase
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
        /// Before Fadeアニメーション
        /// </summary>
        [SerializeField] private TMP_CharRotateAnimation beforeCharRotateAnimation;
        
        /// <summary>
        /// Now Fadeアニメーション
        /// </summary>
        [Header("Now")]
        [SerializeField] private TMP_CharFadeAnimation nowCharFadeAnimation;
        /// <summary>
        /// Now Rotateアニメーション
        /// </summary>
        [SerializeField] private TMP_CharRotateAnimation nowCharRotateAnimation;
        
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
        /// After Rotateアニメーション
        /// </summary>
        [SerializeField] private TMP_CharRotateAnimation afterCharRotateAnimation;
        
        /// <summary>
        /// メインのDOTweenTMPAnimator
        /// </summary>
        DOTweenTMPAnimator animator;
        private void Awake()
        {
            //初期化
            beforeCharFadeAnimation.text = text;
            beforeCharRotateAnimation.text = text;
            nowCharFadeAnimation.text = text;
            nowCharRotateAnimation.text = text;
            afterCharFadeAnimation.text = text;
            afterCharRotateAnimation.text = text;
            
            //before
            if(!isBeforeAnimationPlayOnAwake) return;
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);
            
            sq.Append(beforeCharFadeAnimation.GetSequence(animator));
            sq.Append(beforeCharRotateAnimation.GetSequence(animator));
            
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);
            
            //before
            sq.Join(beforeCharFadeAnimation.GetSequence(animator));
            sq.Join(beforeCharRotateAnimation.GetSequence(animator));
            
            //now
            sq.Append(nowCharFadeAnimation.GetSequence(animator));
            sq.Join(nowCharRotateAnimation.GetSequence(animator));

            if (afterAnimationDelay >= 0)
            {
                //after
                sq.Append(afterCharFadeAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
                sq.Join(afterCharRotateAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
            }
            
            //event
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            beforeCharFadeAnimation?.Dispose();
            beforeCharRotateAnimation?.Dispose();
            nowCharFadeAnimation?.Dispose();
            nowCharRotateAnimation?.Dispose();
            afterCharFadeAnimation?.Dispose();
            afterCharRotateAnimation?.Dispose();
            
            animator?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}