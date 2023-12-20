using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Complex
{
    /// <summary>
    /// Color + Move + Rotate + Scaleのアニメーター
    /// </summary>
    public class TMPC_CharUniversalAnimator : AnimatorBase
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
        /// Before Colorアニメーション
        /// </summary>
        [SerializeField] private TMP_CharColorAnimation beforeCharColorAnimation;
        /// <summary>
        /// Before Moveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharMoveAnimation beforeCharMoveAnimation;
        /// <summary>
        /// Before Rotateアニメーション
        /// </summary>
        [SerializeField] private TMP_CharRotateAnimation beforeCharRotateAnimation;
        /// <summary>
        /// Before Scaleアニメーション
        /// </summary>
        [SerializeField] private TMP_CharScaleAnimation beforeCharScaleAnimation;
        
        /// <summary>
        /// Now Colorアニメーション
        /// </summary>
        [Header("Now")]
        [SerializeField] private TMP_CharColorAnimation nowCharColorAnimation;
        /// <summary>
        /// Now Moveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharMoveAnimation nowCharMoveAnimation;
        /// <summary>
        /// Now Rotateアニメーション
        /// </summary>
        [SerializeField] private TMP_CharRotateAnimation nowCharRotateAnimation;
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
        /// After Colorアニメーション
        /// </summary>
        [SerializeField] private TMP_CharColorAnimation afterCharColorAnimation;
        /// <summary>
        /// After Moveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharMoveAnimation afterCharMoveAnimation;
        /// <summary>
        /// After Rotateアニメーション
        /// </summary>
        [SerializeField] private TMP_CharRotateAnimation afterCharRotateAnimation;
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
            AnimationInit();
            
            //before
            if(!isBeforeAnimationPlayOnAwake) return;
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);
            
            sq.Append(beforeCharColorAnimation.GetSequence(animator));
            sq.Append(beforeCharMoveAnimation.GetSequence(animator));
            sq.Append(beforeCharRotateAnimation.GetSequence(animator));
            sq.Append(beforeCharScaleAnimation.GetSequence(animator));

        }

        /// <summary>
        /// Animationの初期化
        /// </summary>
        private void AnimationInit()
        {
            //before
            beforeCharColorAnimation.text = text;
            beforeCharRotateAnimation.text = text;
            beforeCharRotateAnimation.text = text;
            beforeCharRotateAnimation.text = text;
            
            //now
            nowCharColorAnimation.text = text;
            nowCharRotateAnimation.text = text;
            nowCharRotateAnimation.text = text;
            nowCharRotateAnimation.text = text;
            
            //after
            afterCharColorAnimation.text = text;
            afterCharRotateAnimation.text = text;
            afterCharRotateAnimation.text = text;
            afterCharRotateAnimation.text = text;
        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);
            
            //before
            sq.Join(beforeCharColorAnimation.GetSequence(animator));
            sq.Join(beforeCharMoveAnimation.GetSequence(animator));
            sq.Join(beforeCharRotateAnimation.GetSequence(animator));
            sq.Join(beforeCharScaleAnimation.GetSequence(animator));
            
            //now
            sq.Append(nowCharColorAnimation.GetSequence(animator));
            sq.Join(nowCharMoveAnimation.GetSequence(animator));
            sq.Join(nowCharRotateAnimation.GetSequence(animator));
            sq.Join(nowCharScaleAnimation.GetSequence(animator));

            if (afterAnimationDelay >= 0)
            {
                //after
                sq.Append(afterCharColorAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
                sq.Join(afterCharMoveAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
                sq.Join(afterCharRotateAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
                sq.Join(afterCharScaleAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
            }
            
            //event
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            beforeCharColorAnimation?.Dispose();
            beforeCharMoveAnimation?.Dispose();
            beforeCharRotateAnimation?.Dispose();
            beforeCharScaleAnimation?.Dispose();
            nowCharColorAnimation?.Dispose();
            nowCharMoveAnimation?.Dispose();
            nowCharRotateAnimation?.Dispose();
            nowCharScaleAnimation?.Dispose();
            afterCharColorAnimation?.Dispose();
            afterCharMoveAnimation?.Dispose();
            afterCharRotateAnimation?.Dispose();
            afterCharScaleAnimation?.Dispose();
            
            animator?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}