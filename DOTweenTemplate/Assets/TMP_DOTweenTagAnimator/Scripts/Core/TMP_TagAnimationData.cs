using TMP_DOTweenTemplate.Core.Base;

namespace TMP_DOTweenTagAnimator.Core
{
    /// <summary>
    /// アニメーションとその適用範囲のデータクラス
    /// </summary>
    public class TMP_TagAnimationData
    {
        /// <summary>
        /// readonly
        /// 文字アニメーションの配列
        /// </summary>
        public readonly CharAnimationBase[] charAnimationBases;
        
        /// <summary>
        /// readonly
        /// アニメーションの適用範囲の始まりと終わりのIndex
        /// </summary>
        public readonly int beginIndex, endIndex;

        /// <summary>
        /// コンストラクタ
        /// 初期化用
        /// </summary>
        /// <param name="charAnimationBases">文字アニメーションの配列</param>
        /// <param name="beginIndex">適用範囲の始まり</param>
        /// <param name="endIndex">適用範囲の終わり</param>
        public TMP_TagAnimationData(CharAnimationBase[] charAnimationBases, int beginIndex, int endIndex)
        {
            this.charAnimationBases = charAnimationBases;
            this.beginIndex = beginIndex;
            this.endIndex = endIndex;
        }
    }
}