using TMP_DOTweenTemplate.Core.Base;
using UnityEngine;

namespace TMP_DOTweenTagAnimator.Assets
{
    /// <summary>
    /// タグとそのアニメーション設定のアセット用のベースクラス
    /// </summary>
    public abstract class TMP_AnimationSettingsBase : ScriptableObject
    {
        /// <summary>
        /// 開始タグ
        /// </summary>
        [SerializeField] private string beginTagName = "<OriginalTag>";
        
        /// <summary>
        /// 開始タグの取得
        /// </summary>
        /// <returns><開始タグ/returns>
        public string BeginTagName() => beginTagName;
        
        /// <summary>
        /// 終了タグ
        /// </summary>
        [SerializeField] private string endTagName = "</OriginalTag>";
        /// <summary>
        /// 終了タグの取得
        /// </summary>
        /// <returns>終了タグ</returns>
        public string EndTagName() => endTagName;
        
        /// <summary>
        /// アニメーション設定取得用
        /// </summary>
        /// <returns>アニメーション設定の配列</returns>
        public abstract CharAnimationBase[] GetCharAnimations();
    }
}