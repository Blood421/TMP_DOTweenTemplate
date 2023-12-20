using System;
using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Main.Material
{
    /// <summary>
    /// FaceColorのアニメーション
    /// </summary>
    [Serializable]
    public class TMPM_FaceColorAnimation : MatAnimationBase
    {
        /// <summary>
        /// マテリアルのパラメータID
        /// </summary>
        private int matParamId = Shader.PropertyToID("_FaceColor");
        
        /// <summary>
        /// アニメーションで使うTMP_UGUI
        /// </summary>
        [NonSerialized] public TextMeshProUGUI text;
        
        /// <summary>
        /// アニメーション後の色
        /// </summary>
        [ColorUsage(true,true)] public Color afterColor;

        /// <summary>
        /// デフォルトの色
        /// </summary>
        private Color defaultColor;
        
        /// <summary>
        /// 初期化フラグ
        /// </summary>
        private bool isSetDefault = false;

        public override Sequence GetSequence()
        {
            return CreateSequence();
        }

        protected override Sequence CreateSequence()
        {
            //準備
            Sequence sq = DOTween.Sequence();
            mat = text.materialForRendering;
            Color value = mat.GetColor(matParamId);
            
            //リレイティブ設定用
            Color nowColor = Color.clear;
            if(additional.isRelative) nowColor = value;
            
            //初回はデフォルト初期化
            //二回目以降はリセット
            if (!isSetDefault)
            {
                isSetDefault = true;
                defaultColor = value;
            }
            else
            {
                Reset();
            }

            //ツイーンの設定
            Tween tw;
            //アニメーション設定 + From設定
            if(additional.isFrom) tw = mat.DOColor(afterColor + nowColor, matParamId, duration).From();
            else tw = mat.DOColor(afterColor + nowColor, matParamId, duration);
            
            //イージング設定
            if (additional.useCurveEase) tw.SetEase(additional.curveEase);
            else tw.SetEase(ease);
                
            //シーケンスにジョイン
            sq.Join(tw);

            //ループと遅延設定
            sq.SetDelay(additional.delay);
            sq.SetLoops(additional.loop, additional.loopType);
            
            //更新中はON_MATERIAL_PROPERTY_CHANGEDを呼ぶ
            sq.OnUpdate(() => TMPro_EventManager.ON_MATERIAL_PROPERTY_CHANGED(true, mat));
            
            //シーケンスを返す
            return sq;
        }

        public override void Reset()
        {
            //色のリセット
            if(isSetDefault) mat.SetColor(matParamId,defaultColor);
        }
    }
}