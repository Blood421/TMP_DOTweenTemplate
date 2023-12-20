using System;
using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Main.Pro
{
    /// <summary>
    /// 文字ごとの拡縮のアニメーション
    /// </summary>
    [Serializable]
    public class TMP_CharScaleCurveAnimation : CharAnimationBase
    {
        /// <summary>
        /// 文字ごとの拡縮
        /// </summary>
        public AnimationCurveVector3 afterScaleCurve;

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
            Vector3 nowCharScale = Vector3.zero;
            if(additional.isRelative) nowCharScale = tmp_Animator.GetCharScale(index);
                
            //拡縮設定
            int charCount = Mathf.Clamp((tmp_Animator.textInfo.characterCount - 1), 1, int.MaxValue);
            Vector3 afterScale = new Vector3(
                afterScaleCurve.x.Evaluate((float) index / charCount),
                afterScaleCurve.y.Evaluate((float) index / charCount),
                afterScaleCurve.z.Evaluate((float) index / charCount)
            );
            
            //アニメーション設定 + From設定
            if(additional.isFrom) tw = tmp_Animator.DOScaleChar(index, afterScale + nowCharScale, duration).From();
            else tw = tmp_Animator.DOScaleChar(index, afterScale + nowCharScale, duration);
              
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