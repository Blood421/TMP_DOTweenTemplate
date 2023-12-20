using System;
using System.Collections.Generic;
using DG.Tweening;
using TMP_DOTweenTagAnimator.Assets;
using TMP_DOTweenTagAnimator.Core;
using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main;
using TMPro;
using UnityEngine;

namespace TMP_TextBox.Core.Animator
{
    /// <summary>
    /// テキストボックスのアニメーター
    /// </summary>
    public class TextBoxTextAnimator : MonoBehaviour
    {
        /// <summary>
        /// デフォルトのアニメーション設定
        /// </summary>
        [SerializeField] private TMP_AnimationSettingsBase defaultAnimationSettings;
        
        /// <summary>
        /// シーケンスアニメーション
        /// </summary>
        [SerializeField] private SequenceAnimation sequenceAnimation;
        
        /// <summary>
        /// テキストボックスとアニメーターを繋ぐ
        /// </summary>
        private TextBoxTextAnimatorPresenter textBoxTextAnimatorPresenter;

        /// <summary>
        /// 表示するTMPテキスト
        /// </summary>
        private TextMeshProUGUI text;
        
        /// <summary>
        /// アニメーションに使うDOTweenTMPAnimator
        /// </summary>
        DOTweenTMPAnimator animator;

        private void Awake()
        {
            //テキストボックスがアタッチされているかチェック
            //なければエラー
            var result = IsExistTextBox();
            if (!result.isExist)
            {
                Debug.LogWarning("TextBox does not Exist!");
                return;
            }
            
            //プレゼンターからこのクラスとテキストボックスを繋ぐ
            PresenterSetting(result.textBox);
            //テキストを更新
            text = result.textBox.GetMainText();
        }

        /// <summary>
        /// TMP_TextBoxがアタッチされているかチェックする
        /// </summary>
        /// <returns>チェックした結果と参照</returns>
        private (bool isExist,TextBox.TMP_TextBox textBox) IsExistTextBox()
        {
            var textBox = GetComponent<TextBox.TMP_TextBox>();
            return (textBox != null, textBox);
        }

        /// <summary>
        /// プレゼンター経由でこのクラスとTMP_TextBoxクラスを繋ぐ
        /// </summary>
        /// <param name="textBox">テキストボックスクラスの参照</param>
        private void PresenterSetting(TextBox.TMP_TextBox textBox) => textBoxTextAnimatorPresenter = new TextBoxTextAnimatorPresenter(textBox,this);

        /// <summary>
        /// アニメーション開始
        /// </summary>
        public void PlayAnimation()
        {
            //アニメーションタグのチェック
            var result = TMP_TagAnimatorManager.instance.CheckTags(text.text,text.richText);
            //結果のデータリストを取得
            var tmpTagAnimationDataList = result.dataList;
            //タグを削除した文字列データをテキストに入れる
            text.text = result.checkedText;
            
            //初期化
            Sequence sq = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(text);

            //デフォルトのアニメーションの範囲を取得
            var defaultAnimationRanges = GetDefaultRange(tmpTagAnimationDataList);

            int lengthSum = 0;
            RangeInt beforeRange = new RangeInt(0,0);
            foreach (RangeInt defaultAnimationRange in defaultAnimationRanges)
            {
                //デフォルトのアニメーションの設定
                lengthSum += defaultAnimationRange.start - beforeRange.end;
                foreach (CharAnimationBase charAnimationBase in defaultAnimationSettings.GetCharAnimations())
                {
                    //デフォルト以外の範囲の長さからアニメーション遅延を設定
                    var sqTemp = charAnimationBase.GetSequence(animator, defaultAnimationRange);
                    float delay = lengthSum * charAnimationBase.charDelay + sqTemp.Delay();
                    sq.Join(sqTemp.SetDelay(delay));
                }
                
                //文字数を加算してひとつ前の範囲を更新
                lengthSum += defaultAnimationRange.length;
                beforeRange = defaultAnimationRange;
            }

            foreach (TMP_TagAnimationData tmpTagAnimationData in tmpTagAnimationDataList)
            {
                //タグのアニメーションの設定
                int beginIndex = tmpTagAnimationData.beginIndex;
                int endIndex = tmpTagAnimationData.endIndex;
                RangeInt range = new RangeInt(beginIndex, endIndex - beginIndex);
                foreach (CharAnimationBase charAnimationBase in tmpTagAnimationData.charAnimationBases)
                {
                    //デフォルトの範囲の長さからアニメーション遅延を設定
                    var sqTemp = charAnimationBase.GetSequence(animator,range);
                    float delay = beginIndex * charAnimationBase.charDelay + sqTemp.Delay();
                    sq.Join(sqTemp.SetDelay(delay));
                }
            }

            //event
            sequenceAnimation.PlayAndInit(sq);
        }

        /// <summary>
        /// アニメーションをスキップする
        /// </summary>
        public void AnimationSkip()
        {
            sequenceAnimation.mainSequence.Complete();
        }

        /// <summary>
        /// タグアニメーション以外の部分の範囲を取得する
        /// </summary>
        /// <param name="rangeList">タグアニメーションデータのリスト</param>
        /// <returns>タグアニメーション以外の部分の範囲</returns>
        private List<RangeInt> GetDefaultRange(List<TMP_TagAnimationData> rangeList)
        {
            //返す用のリスト
            List<RangeInt> result = new List<RangeInt>(100);
            
            for (int beginIndex = 0; beginIndex < text.text.Length; beginIndex++)
            {
                bool isGenerate = false;
                int end = beginIndex;
                for (int endIndex = beginIndex + 1; endIndex < text.text.Length + 1; endIndex++)
                {
                    isGenerate = true;

                    bool isRangeIn = false;
                    foreach (TMP_TagAnimationData rangeInt in rangeList)
                    {
                        //範囲内入ってたら範囲内フラグを立てる
                        if ((beginIndex > rangeInt.beginIndex && beginIndex < rangeInt.endIndex) || 
                            (endIndex > rangeInt.beginIndex && endIndex < rangeInt.endIndex) )
                        {
                            isRangeIn = true;
                        }
                        //終わりと始まりが同じなら生成フラグを立てない
                        if(endIndex == rangeInt.beginIndex)
                        {
                            isGenerate = false;
                        };
                    }

                    //生成する場合終わりは-1
                    if (isGenerate) end = endIndex - 1;
                    //範囲内フラグが立って入ればスキップ
                    if (isRangeIn) break;

                    //終わりがテキストの文字列の終わりなら生成
                    if (endIndex == text.text.Length)
                    {
                        isGenerate = true;
                        end = text.text.Length;
                    }
                }

                //生成フラグが立っていて開始位置が終了位置より前なら範囲データを生成
                if (isGenerate && beginIndex < end)
                {
                    //Generate
                    result.Add(new RangeInt(beginIndex,end - beginIndex));
                    beginIndex = end;
                }
            }

            return result;
        }

        private void OnDestroy()
        {
            textBoxTextAnimatorPresenter.Dispose();
        }
    }
}