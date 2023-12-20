﻿using System;
using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Main.Pro
{
    /// <summary>
    /// 回転のアニメーション
    /// </summary>
    [Serializable]
    public class TMP_CharRotateAnimation : CharAnimationBase
    {
        /// <summary>
        /// アニメーション後の角度
        /// </summary>
        public Vector3 afterAngles;

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
            if(additional.isRelative) nowCharOffset = tmp_Animator.GetCharRotation(index);
                
            //アニメーション設定 + From設定
            if(additional.isFrom) tw = tmp_Animator.DORotateChar(index, afterAngles + nowCharOffset, duration , RotateMode.FastBeyond360).From();
            else tw = tmp_Animator.DORotateChar(index, afterAngles + nowCharOffset, duration, RotateMode.FastBeyond360);
                
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