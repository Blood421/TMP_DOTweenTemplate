using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Complex
{
    /// <summary>
    /// ColorGradient + MoveCurve + RotateCurve + ScaleCurveのアニメーター
    /// </summary>
    public class TMPC_CharUniversalCurveAnimator : AnimatorBase
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
        /// Before ColorGradientアニメーション
        /// </summary>
        [SerializeField] private TMP_CharColorGradientAnimation beforeCharColorGradientAnimation;
        /// <summary>
        /// Before MoveCurveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharMoveCurveAnimation beforeCharMoveCurveAnimation;
        /// <summary>
        /// Before RotateCurveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharRotateCurveAnimation beforeCharRotateCurveAnimation;
        /// <summary>
        /// Before ScaleCurveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharScaleCurveAnimation beforeCharScaleCurveAnimation;
        
        /// <summary>
        /// Now ColorGradientアニメーション
        /// </summary>
        [Header("Now")]
        [SerializeField] private TMP_CharColorGradientAnimation nowCharColorGradientAnimation;
        /// <summary>
        /// Now MoveCurveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharMoveCurveAnimation nowCharMoveCurveAnimation;
        /// <summary>
        /// Now RotateCurveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharRotateCurveAnimation nowCharRotateCurveAnimation;
        /// <summary>
        /// Now ScaleCurveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharScaleCurveAnimation nowCharScaleCurveAnimation;
        
        /// <summary>
        /// Nowの後Afterを実行するまでの遅延
        /// </summary>
        [Header("After")]
        [SerializeField] private float afterAnimationDelay = 1f;
        /// <summary>
        /// After ColorGradientアニメーション
        /// </summary>
        [SerializeField] private TMP_CharColorGradientAnimation afterCharColorGradientAnimation;
        /// <summary>
        /// After MoveCurveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharMoveCurveAnimation afterCharMoveCurveAnimation;
        /// <summary>
        /// After RotateCurveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharRotateCurveAnimation afterCharRotateCurveAnimation;
        /// <summary>
        /// After ScaleCurveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharScaleCurveAnimation afterCharScaleCurveAnimation;
        
        /// <summary>
        /// メインのDOTweenTMPAnimator
        /// </summary>
        DOTweenTMPAnimator animator;
        private void Awake()
        {
            //初期化
            AnimationInit();
            
            //before
            if(!isBeforeAnimationPlayOnAwake) return;
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);

            sq.Join(beforeCharColorGradientAnimation.GetSequence(animator));
            sq.Join(beforeCharMoveCurveAnimation.GetSequence(animator));
            sq.Join(beforeCharRotateCurveAnimation.GetSequence(animator));
            sq.Join(beforeCharScaleCurveAnimation.GetSequence(animator));
            
        }

        /// <summary>
        /// Animationの初期化
        /// </summary>
        private void AnimationInit()
        {
            //before
            beforeCharColorGradientAnimation.text = text;
            beforeCharMoveCurveAnimation.text = text;
            beforeCharRotateCurveAnimation.text = text;
            beforeCharScaleCurveAnimation.text = text;
            
            //now
            nowCharColorGradientAnimation.text = text;
            nowCharMoveCurveAnimation.text = text;
            nowCharRotateCurveAnimation.text = text;
            nowCharScaleCurveAnimation.text = text;
            
            //after
            afterCharColorGradientAnimation.text = text;
            afterCharMoveCurveAnimation.text = text;
            afterCharRotateCurveAnimation.text = text;
            afterCharScaleCurveAnimation.text = text;
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);
            
            //before
            sq.Join(beforeCharColorGradientAnimation.GetSequence(animator));
            sq.Join(beforeCharMoveCurveAnimation.GetSequence(animator));
            sq.Join(beforeCharRotateCurveAnimation.GetSequence(animator));
            sq.Join(beforeCharScaleCurveAnimation.GetSequence(animator));
            
            //now
            sq.Append(nowCharColorGradientAnimation.GetSequence(animator));
            sq.Join(nowCharMoveCurveAnimation.GetSequence(animator));
            sq.Join(nowCharRotateCurveAnimation.GetSequence(animator));
            sq.Join(nowCharScaleCurveAnimation.GetSequence(animator));

            if (afterAnimationDelay >= 0)
            {
                //after
                sq.Append(afterCharColorGradientAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
                sq.Join(afterCharMoveCurveAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
                sq.Join(afterCharRotateCurveAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
                sq.Join(afterCharScaleCurveAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
            }
            
            //event
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            beforeCharColorGradientAnimation?.Dispose();
            beforeCharMoveCurveAnimation?.Dispose();
            beforeCharRotateCurveAnimation?.Dispose();
            beforeCharScaleCurveAnimation?.Dispose();
            nowCharColorGradientAnimation?.Dispose();
            nowCharMoveCurveAnimation?.Dispose();
            nowCharRotateCurveAnimation?.Dispose();
            nowCharScaleCurveAnimation?.Dispose();
            afterCharColorGradientAnimation?.Dispose();
            afterCharMoveCurveAnimation?.Dispose();
            afterCharRotateCurveAnimation?.Dispose();
            afterCharScaleCurveAnimation?.Dispose();
            
            animator?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}