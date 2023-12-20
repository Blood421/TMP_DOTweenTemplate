using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Base
{
    /// <summary>
    /// Charアニメーションのベースクラス
    /// AnimationBaseを継承
    /// </summary>
    [Serializable]
    public abstract class CharAnimationBase : AnimationBase, IDisposable
    {
        /// <summary>
        /// 一文字毎の遅延
        /// </summary>
        public float charDelay;
        
        /// <summary>
        /// アニメーションで使うTMP_UGUI
        /// </summary>
        [NonSerialized] public TextMeshProUGUI text;
        
        /// <summary>
        /// アニメーションで使うDOTweenTMPAnimator
        /// </summary>
        public DOTweenTMPAnimator tmp_Animator = null;
        
        /// <summary>
        /// アニメーションのシーケンスを取得
        /// </summary>
        /// <returns>シーケンスを返す</returns>
        public override Sequence GetSequence()
        {
            //TMPAnimatorがNullかリレイティブ設定出ない場合インスタンス
            if(tmp_Animator == null || !additional.isRelative) tmp_Animator = new DOTweenTMPAnimator(text);
            return CreateSequence();
        }

        /// <summary>
        /// アニメーションのシーケンスを取得
        /// </summary>
        /// <param name="animationRange">アニメーションする文字範囲</param>
        /// <returns>シーケンスを返す</returns>
        public Sequence GetSequence(RangeInt animationRange)
        {
            //TMPAnimatorがNullかリレイティブ設定出ない場合インスタンス
            if(tmp_Animator == null || !additional.isRelative) tmp_Animator = new DOTweenTMPAnimator(text);
            return CreateSequence(animationRange);
        }

        /// <summary>
        /// アニメーションのシーケンスを取得
        /// </summary>
        /// <param name="animator">セットするDOTweenTMPAnimator</param>
        /// <returns>シーケンスを返す</returns>
        public Sequence GetSequence(DOTweenTMPAnimator animator)
        {
            tmp_Animator = animator;
            return CreateSequence();
        }

        /// <summary>
        /// アニメーションのシーケンスを取得
        /// </summary>
        /// <param name="animator">セットするDOTweenTMPAnimator</param>
        /// <param name="animationRange">アニメーションする文字範囲</param>
        /// <returns>シーケンスを返す</returns>
        public Sequence GetSequence(DOTweenTMPAnimator animator, RangeInt animationRange)
        {
            tmp_Animator = animator;
            return CreateSequence(animationRange);
        }
        /// <summary>
        /// シーケンスを作る
        /// </summary>
        /// <param name="animationRange">アニメーションする文字範囲</param>
        /// <returns>シーケンスを返す</returns>
        protected abstract Sequence CreateSequence(RangeInt animationRange);
        
        /// <summary>
        /// 1文字分のツイーンを作る
        /// </summary>
        /// <param name="index">文字のインデックス</param>
        /// <returns>1文字分のツイーンを返す</returns>
        protected abstract Tween CreateTween(int index);
        
        /// <summary>
        /// 破棄
        /// </summary>
        public abstract void Dispose();
    }
}