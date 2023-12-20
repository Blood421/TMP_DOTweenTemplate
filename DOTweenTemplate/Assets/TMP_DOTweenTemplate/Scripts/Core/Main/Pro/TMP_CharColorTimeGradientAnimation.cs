using System;
using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Main.Pro
{
    /// <summary>
    /// 色の時間グラデーションアニメーション
    /// </summary>
    [Serializable]
    public class TMP_CharColorTimeGradientAnimation : CharAnimationBase
    {
        /// <summary>
        /// アニメーション時間グラデーションの色
        /// </summary>
        public Gradient afterColorGradient;

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

        protected override Tween CreateTween(int i)
        {
            Tween tw;
            
            //リレイティブ設定用
            Color col = tmp_Animator.GetCharColor(i);
            Color nowCharOffset = Color.clear;
            if(additional.isRelative) nowCharOffset = col;
                
            //アニメーション設定 + From設定
            float time = 0;
            int index = i;
            if (additional.isFrom)
            {
                tw = DOTween.To(
                    () => time,
                    value =>
                    {
                        time = value;
                        tmp_Animator.SetCharColor(index, afterColorGradient.Evaluate(time) + nowCharOffset);
                    },
                    1f,
                    duration).From();
            }
            else
            {
                tw = DOTween.To(
                    () => time,
                    value =>
                    {
                        time = value;
                        tmp_Animator.SetCharColor(index, afterColorGradient.Evaluate(time) + nowCharOffset);
                    },
                    1f,
                    duration);
            }
               
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