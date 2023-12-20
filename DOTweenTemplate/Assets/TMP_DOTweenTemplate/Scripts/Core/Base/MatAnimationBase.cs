using System;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Base
{
    /// <summary>
    /// マテリアルアニメーションのベースクラス
    /// AnimationBaseを継承
    /// </summary>
    [Serializable]
    public abstract class MatAnimationBase : AnimationBase
    {
        /// <summary>
        /// アニメーションするマテリアル
        /// </summary>
        [NonSerialized] public Material mat;
        
        /// <summary>
        /// マテリアルリセット
        /// </summary>
        public abstract void Reset();
    }
}