using System;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Base
{
    /// <summary>
    /// アニメーションカーブを3方向分まとめたクラス
    /// </summary>
    [Serializable]
    public class AnimationCurveVector3
    {
        public AnimationCurve x;
        public AnimationCurve y;
        public AnimationCurve z;
    }
}