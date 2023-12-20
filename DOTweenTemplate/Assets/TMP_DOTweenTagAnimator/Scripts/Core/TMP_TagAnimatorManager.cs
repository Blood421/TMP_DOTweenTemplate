using System.Collections.Generic;
using System.Linq;
using TMP_DOTweenTagAnimator.Assets;
using TMP_DOTweenTagAnimator.Tags;
using UnityEngine;

namespace TMP_DOTweenTagAnimator.Core
{
    /// <summary>
    ///　設定してあるアニメーション設定アセットからタグを判定。その結果からアニメーションデータとその範囲を生成するクラス
    /// 
    /// タグのアニメーションテキストクラスより先に動いてほしいため
    /// DefaultExecutionOrder(-5)
    /// </summary>
    [DefaultExecutionOrder(-5)]
    public class TMP_TagAnimatorManager : MonoBehaviour
    {
        /// <summary>
        /// シングルトン用
        /// </summary>
        public static TMP_TagAnimatorManager instance;

        /// <summary>
        /// ここにアニメーション設定アセットをセット
        /// </summary>
        [SerializeField] private TMP_AnimationSettingsBase[] tmpAnimationSettingsBases;

        /// <summary>
        /// TMPのRichTextTagを判定する用
        /// </summary>
        private TMP_RTTChecker tmpRTTChecker;
        
        private void Awake()
        {
            //初期化
            ToSingleton();
            tmpRTTChecker = new TMP_RTTChecker();
        }

        /// <summary>
        /// シングルトン化
        /// </summary>
        private void ToSingleton()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        /// <summary>
        /// RichTextTagと設定したアニメーション設定のタグを判定
        /// </summary>
        /// <param name="checkText">判定する文字列</param>
        /// <param name="isRichTextTagUsed">RichTextTagを判定するかどうか</param>
        /// <returns>結果から生成されたアニメーション設定とその範囲データのリストとアニメーションタグを削除した文字列</returns>
        public (List<TMP_TagAnimationData> dataList, string checkedText) CheckTags(string checkText,bool isRichTextTagUsed)
        {
            //フラグが立っていて
            //リッチテキストタグ使ってたらそれを除外
            List<RangeInt> rttRangeDataList = new List<RangeInt>();
            if(isRichTextTagUsed) rttRangeDataList = tmpRTTChecker.RTTCheck(checkText);
            //Debug.Log(checkText);
            
            //RichTextTagの範囲データをStartIndex順に並べ替え
            rttRangeDataList = rttRangeDataList.OrderBy(value => value.start).ToList();

            //開始タグを検索してタグが見つかったらこの設定リストにアニメーション設定と始まりを追加
            List<(TMP_AnimationSettingsBase animationSettings, int beginIndexNum)> settings =
                new List<(TMP_AnimationSettingsBase animationSettings, int beginIndexNum)>(100);
            foreach (var tmpAnimationSettingsBase in tmpAnimationSettingsBases)
            {
                //タグを検索して見つかったらそのIndexが設定される
                string beginTag = tmpAnimationSettingsBase.BeginTagName();
                int beginIndex = checkText.IndexOf(beginTag);
                
                //見つからないからスキップ
                if(beginIndex == -1) continue;
                settings.Add((tmpAnimationSettingsBase,beginIndex));
            }
            
            //StartIndex順に並べ替え
            settings = settings.OrderBy(value => value.beginIndexNum).ToList();

            //返す用のアニメーション設定とその範囲のデータリスト
            List<TMP_TagAnimationData> tmpTagAnimationDataList = new List<TMP_TagAnimationData>();
            int removeCharNum = 0;
            foreach (var setting in settings)
            {
                //無限ループ
                while (true)
                {
                    //タグ取得
                    string beginTag = setting.animationSettings.BeginTagName();
                    string endTag = setting.animationSettings.EndTagName();

                    //始まりのIndexを検索
                    int beginIndex = checkText.IndexOf(beginTag);
                    //ヒットしたら消す
                    if (beginIndex > -1) checkText = checkText.Remove(beginIndex, beginTag.Length);
                
                    //終わりのIndexを検索
                    int endIndex = checkText.IndexOf(endTag);
                    //ヒットしたら消す
                    if (endIndex > -1) checkText = checkText.Remove(endIndex, endTag.Length);

                    //開始タグと終了タグがヒット
                    if (beginIndex > -1 && endIndex > -1)
                    {
                        //範囲の始まりのIndex設定
                        int bIndex = beginIndex;
                        int lengthSum = 0;
                        for (int i = 0; i < rttRangeDataList.Count; i++)
                        {
                            if (beginIndex < rttRangeDataList[i].start - removeCharNum) break;
                            lengthSum += rttRangeDataList[i].length;
                        }
                        bIndex = beginIndex - lengthSum;
                        
                        //範囲の終わりのIndex設定
                        int eIndex = endIndex;
                        lengthSum = 0;
                        for (int i = 0; i < rttRangeDataList.Count; i++)
                        {
                            if (endIndex < rttRangeDataList[i].start - removeCharNum)break;
                            lengthSum += rttRangeDataList[i].length;
                        }
                        eIndex = endIndex - lengthSum;
                        
                        //アニメーション設定と範囲を設定しリストに追加
                        var animations = setting.animationSettings.GetCharAnimations();
                        TMP_TagAnimationData tmpTagAnimationData =
                            new TMP_TagAnimationData(animations, bIndex, eIndex);
                        tmpTagAnimationDataList.Add(tmpTagAnimationData);
                    
                        //タグの文字数分消えたのでカウント
                        removeCharNum += beginTag.Length + endTag.Length;
                        continue;
                    }
                
                    //開始タグだけヒット
                    if (beginIndex > -1)
                    {
                        //範囲の始まりのIndex設定
                        int index = beginIndex;
                        int lengthSum = 0;
                        for (int i = 0; i < rttRangeDataList.Count; i++)
                        {
                            if (beginIndex < rttRangeDataList[i].start - removeCharNum)break;

                            lengthSum += rttRangeDataList[i].length;
                        }
                        index = beginIndex - lengthSum;
                        
                        //アニメーション設定と範囲を設定しリストに追加
                        var animations = setting.animationSettings.GetCharAnimations();
                        TMP_TagAnimationData tmpTagAnimationData =
                            new TMP_TagAnimationData(animations, index, int.MaxValue);
                        tmpTagAnimationDataList.Add(tmpTagAnimationData);
                        
                        //タグの文字数分消えたのでカウント
                        removeCharNum += beginTag.Length;
                        continue;
                    }
                
                    //どちらもヒットしない
                    break;
                }
            }

            return (tmpTagAnimationDataList, checkText);
        }
    }
}