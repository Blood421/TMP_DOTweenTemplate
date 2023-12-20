using System;
using DG.Tweening;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Base
{
    /// <summary>
    /// アニメーションのベースクラス
    /// </summary>
    [Serializable]
    public abstract class AnimationBase
    {
        /// <summary>
        /// 追加設定クラス
        /// </summary>
        [Serializable]
        public class Additional
        {
            /// <summary>
            /// イージングをアニメーションカーブで設定したい場合
            /// </summary>
            public AnimationCurve curveEase;
            
            /// <summary>
            /// イージングをアニメーションカーブにするかどうか
            /// </summary>
            public bool useCurveEase;
            
            /// <summary>
            /// アニメーションの遅延
            /// </summary>
            public float delay;
            
            /// <summary>
            /// ループ回数
            /// </summary>
            public int loop;
            
            /// <summary>
            /// ループする場合の種類
            /// </summary>
            public LoopType loopType;
            
            /// <summary>
            /// リレイティブ設定にするかどうか
            /// </summary>
            public bool isRelative;
            
            /// <summary>
            /// From設定にするかどうか
            /// </summary>
            public bool isFrom;
        }
        
        /// <summary>
        /// 追加設定
        /// </summary>
        public Additional additional;
        
        /// <summary>
        /// アニメーション秒数
        /// </summary>
        public float duration;
        
        /// <summary>
        /// イージング設定
        /// </summary>
        public Ease ease;

        /// <summary>
        /// アニメーションのシーケンスを取得
        /// </summary>
        /// <returns>シーケンスを返す</returns>
        public abstract Sequence GetSequence();
        
        /// <summary>
        /// シーケンスを作る
        /// </summary>
        /// <returns>作ったシーケンスを返す</returns>
        protected abstract Sequence CreateSequence();
        
        
    }
}