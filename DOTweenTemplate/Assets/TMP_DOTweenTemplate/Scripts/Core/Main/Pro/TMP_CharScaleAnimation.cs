using System;
using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Main.Pro
{
    /// <summary>
    /// 拡縮のアニメーション
    /// </summary>
    [Serializable]
    public class TMP_CharScaleAnimation : CharAnimationBase
    {
        /// <summary>
        /// アニメーション後の拡縮
        /// </summary>
        public Vector3 afterScale;

        protected override Sequence CreateSequence(RangeInt animationRange)
        {
            //文字数分アニメーションツイーンを生成してシーケンスにジョイン(範囲内の文字のみ)
            Sequence sq = DOTween.Sequence();
            for (int i = 0; i < tmp_Animator.textInfo.characterCount; ++i) {
                if (!tmp_Animator.textInfo.characterInfo[i].isVisible) continue;
                if(i < animationRange.start) continue;
                if(i >= animationRange.end) continue;
                
                Tween tw = CreateTween(i);
                sq.Join(tw);
            }

            //ループと遅延設定
            sq.SetDelay(additional.delay);
            sq.SetLoops(additional.loop, additional.loopType);
            //シーケンスを返す
            return sq;
        }

        protected override Sequence CreateSequence()
        {
            //文字数分アニメーションツイーンを生成してシーケンスにジョイン
            Sequence sq = DOTween.Sequence();
            for (int i = 0; i < tmp_Animator.textInfo.characterCount; ++i) {
                if (!tmp_Animator.textInfo.characterInfo[i].isVisible) continue;

                Tween tw = CreateTween(i);
                sq.Join(tw);
            }

            //ループと遅延設定
            sq.SetDelay(additional.delay);
            sq.SetLoops(additional.loop, additional.loopType);
            //シーケンスを返す
            return sq;
        }
        
        protected override Tween CreateTween(int index)
        {
            Tween tw;
            
            //リレイティブ設定用
            Vector3 nowCharOffset = Vector3.zero;
            if(additional.isRelative) nowCharOffset = tmp_Animator.GetCharScale(index);
                
            //アニメーション設定 + From設定
            if(additional.isFrom) tw = tmp_Animator.DOScaleChar(index, afterScale + nowCharOffset, duration).From();
            else tw = tmp_Animator.DOScaleChar(index, afterScale + nowCharOffset, duration);
                
            //イージング設定
            if (additional.useCurveEase) tw.SetEase(additional.curveEase);
            else tw.SetEase(ease);
                
            //文字毎の遅延設定
            tw.SetDelay(charDelay);
            //ツイーンを返す
            return tw;
        }

        public override void Dispose()
        {
            //破棄
            tmp_Animator?.Dispose();
        }
    }
}