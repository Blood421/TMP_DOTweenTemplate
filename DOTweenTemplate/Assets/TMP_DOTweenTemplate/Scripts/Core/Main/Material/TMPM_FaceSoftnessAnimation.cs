using System;
using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Core.Main.Material
{
    /// <summary>
    /// FaceSoftのアニメーション
    /// </summary>
    [Serializable]
    public class TMPM_FaceSoftnessAnimation : MatAnimationBase
    {
        /// <summary>
        /// マテリアルのパラメータID
        /// </summary>
        private int matParamId = Shader.PropertyToID("_OutlineSoftness");
        
        /// <summary>
        /// アニメーションで使うTMP_UGUI
        /// </summary>
        [NonSerialized] public TextMeshProUGUI text;
        
        /// <summary>
        /// アニメーション後の値
        /// </summary>
        public float afterSoftness;

        /// <summary>
        /// デフォルトの値
        /// </summary>
        private float defaultFloat;
        
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
            mat = text.fontMaterial;
            float value = mat.GetFloat(matParamId);
            
            //リレイティブ設定用
            float nowFloat = 0;
            if(additional.isRelative) nowFloat = value;
            
            //初回はデフォルト初期化
            //二回目以降はリセット
            if (!isSetDefault)
            {
                isSetDefault = true;
                defaultFloat = value;
            }
            else
            {
                Reset();
            }

            //ツイーンの設定
            Tween tw;
            //アニメーション設定 + From設定
            if(additional.isFrom) tw = mat.DOFloat(afterSoftness + nowFloat, matParamId, duration).From();
            else tw = mat.DOFloat(afterSoftness + nowFloat, matParamId, duration);
                
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
            //値のリセット
            if(isSetDefault) mat.SetFloat(matParamId,defaultFloat);
        }
    }
}