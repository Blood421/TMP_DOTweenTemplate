using System.Collections.Generic;
using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTagAnimator.Core
{
    /// <summary>
    /// タグアニメーション用のテキストクラス
    /// </summary>
    public class TMP_TagAnimatorText : MonoBehaviour
    {
        /// <summary>
        /// タグアニメーションさせるTMP_Text
        /// </summary>
        [SerializeField] private TextMeshProUGUI text;
        
        /// <summary>
        /// シーケンスアニメーション
        /// </summary>
        [SerializeField] private SequenceAnimation sequenceAnimation;
        
        /// <summary>
        /// スタートでアニメーションを始めるかどうか
        /// </summary>
        [SerializeField] private bool isStartAnimationPlay = false;
        
        /// <summary>
        /// シーケンスイベント取得
        /// </summary>
        /// <returns>シーケンスイベント</returns>
        public SequenceEvent GetSequenceEvent() => sequenceAnimation.sequenceEvent;

        /// <summary>
        /// 文字アニメーションとその範囲データのリスト
        /// </summary>
        private List<TMP_TagAnimationData> tmpTagAnimationDataList = new List<TMP_TagAnimationData>();
        
        /// <summary>
        /// 文字アニメーション用DOTweenTMPAnimator
        /// </summary>
        private DOTweenTMPAnimator animator;
        
        private void Awake()
        {
            //初期化
            
            //タグをチェックしてその結果から生成されたアニメーションとその範囲データをセット
            var result = TMP_TagAnimatorManager.instance.CheckTags(text.text,text.richText);
            tmpTagAnimationDataList = result.dataList;
            
            //テキストをセット
            text.text = result.checkedText;
        }

        private void Start()
        {
            //フラグが立ってたらアニメーション開始
            if(isStartAnimationPlay) Play();
        }

        /// <summary>
        /// アニメーション開始
        /// </summary>
        public void Play()
        {
            //初期化
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);
            
            //アニメーションを設定
            //アニメーションとその範囲データのある分だけループ
            foreach (TMP_TagAnimationData tmpTagAnimationData in tmpTagAnimationDataList)
            {
                //範囲の設定
                int beginIndex = tmpTagAnimationData.beginIndex;
                int endIndex = tmpTagAnimationData.endIndex;
                RangeInt range = new RangeInt(beginIndex, endIndex - beginIndex);
                
                //アニメーションの数分設定
                foreach (CharAnimationBase charAnimationBase in tmpTagAnimationData.charAnimationBases)
                {
                    //Debug.Log(range.start + " : " + range.end);
                    //文字範囲付きでアニメーション設定
                    sq.Join(charAnimationBase.GetSequence(animator,range));
                }
            }
            
            //アニメーション開始
            sequenceAnimation.PlayAndInit(sq);
        }
    }
}