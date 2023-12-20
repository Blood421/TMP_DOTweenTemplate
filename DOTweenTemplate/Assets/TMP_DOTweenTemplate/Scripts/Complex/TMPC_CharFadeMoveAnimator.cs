using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Complex
{
    /// <summary>
    /// Fade + Moveのアニメーター
    /// </summary>
    public class TMPC_CharFadeMoveAnimator : AnimatorBase
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
        /// Before Moveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharMoveAnimation beforeCharMoveAnimation;
        
        /// <summary>
        /// Now Fadeアニメーション
        /// </summary>
        [Header("Now")]
        [SerializeField] private TMP_CharFadeAnimation nowCharFadeAnimation;
        /// <summary>
        /// Now Moveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharMoveAnimation nowCharMoveAnimation;
        
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
        /// After Moveアニメーション
        /// </summary>
        [SerializeField] private TMP_CharMoveAnimation afterCharMoveAnimation;
        
        /// <summary>
        /// メインのDOTweenTMPAnimator
        /// </summary>
        DOTweenTMPAnimator animator;
        private void Awake()
        {
            //初期化
            beforeCharFadeAnimation.text = text;
            beforeCharMoveAnimation.text = text;
            nowCharFadeAnimation.text = text;
            nowCharMoveAnimation.text = text;
            afterCharFadeAnimation.text = text;
            afterCharMoveAnimation.text = text;
            
            //before
            if (!isBeforeAnimationPlayOnAwake) return;
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);
            
            sq.Append(beforeCharFadeAnimation.GetSequence(animator));
            sq.Append(beforeCharMoveAnimation.GetSequence(animator));

        }

        public override void Play()
        {
            //シーケンスを取得して再生
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);
            
            //before
            sq.Join(beforeCharFadeAnimation.GetSequence(animator));
            sq.Join(beforeCharMoveAnimation.GetSequence(animator));
            
            //now
            sq.Append(nowCharFadeAnimation.GetSequence(animator));
            sq.Join(nowCharMoveAnimation.GetSequence(animator));

            if (afterAnimationDelay >= 0)
            {
                //after
                sq.Append(afterCharFadeAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
                sq.Join(afterCharMoveAnimation.GetSequence(animator).SetDelay(afterAnimationDelay));
            }
            
            //event
            mainSequence.PlayAndInit(sq);
        }

        public override void Dispose()
        {
            //破棄
            mainSequence?.Dispose();
            beforeCharFadeAnimation?.Dispose();
            beforeCharMoveAnimation?.Dispose();
            nowCharFadeAnimation?.Dispose();
            nowCharMoveAnimation?.Dispose();
            afterCharFadeAnimation?.Dispose();
            afterCharMoveAnimation?.Dispose();
            
            animator?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}