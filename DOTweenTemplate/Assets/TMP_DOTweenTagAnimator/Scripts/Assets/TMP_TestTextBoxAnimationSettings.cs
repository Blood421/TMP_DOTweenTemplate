using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using UnityEngine;

namespace TMP_DOTweenTagAnimator.Assets
{
    /// <summary>
    /// テキストボックステスト用のアニメーション設定アセットクラス
    /// </summary>
    [CreateAssetMenu(fileName = "TestTextBoxAnimationSettings", menuName = "TMP_DOTweenTagAnimator/TestTextBoxAnimationSettings", order = 1)]
    public class TMP_TestTextBoxAnimationSettings : TMP_AnimationSettingsBase
    {
        //移動と色の初期値アリ版
        [SerializeField] private TMP_CharMoveAnimation tmpCharMoveAnimationBefore;
        [SerializeField] private TMP_CharColorAnimation tmpCharColorAnimationBefore;
        [SerializeField] private TMP_CharMoveAnimation tmpCharMoveAnimationAfter;
        [SerializeField] private TMP_CharColorAnimation tmpCharColorAnimationAfter;

        public override CharAnimationBase[] GetCharAnimations()
        {
            return new CharAnimationBase[] {tmpCharMoveAnimationBefore,tmpCharColorAnimationBefore,tmpCharMoveAnimationAfter,tmpCharColorAnimationAfter};
        }
    }
}