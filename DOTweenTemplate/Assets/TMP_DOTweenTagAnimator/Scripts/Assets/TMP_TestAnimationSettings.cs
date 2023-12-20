using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using UnityEngine;

namespace TMP_DOTweenTagAnimator.Assets
{
    /// <summary>
    /// テスト用のアニメーション設定アセットクラス
    /// </summary>
    [CreateAssetMenu(fileName = "TestAnimationSettings", menuName = "TMP_DOTweenTagAnimator/TestAnimationSettings", order = 1)]
    public class TMP_TestAnimationSettings : TMP_AnimationSettingsBase
    {
        //移動と色
        [SerializeField] private TMP_CharMoveAnimation tmpCharMoveAnimation;
        [SerializeField] private TMP_CharColorAnimation tmpCharColorAnimation;

        public override CharAnimationBase[] GetCharAnimations()
        {
            return new CharAnimationBase[] {tmpCharMoveAnimation,tmpCharColorAnimation};
        }
    }
}