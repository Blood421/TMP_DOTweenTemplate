using System;

namespace TMP_DOTweenTextBox.Core.Animator
{
    /// <summary>
    /// テキストボックスとアニメーターを繋ぐクラス
    /// </summary>
    public class TextBoxTextAnimatorPresenter : IDisposable
    {
        /// <summary>
        /// テキストボックスクラス
        /// </summary>
        private TextBox.TMP_DOTweenTextBox textBox;
        
        /// <summary>
        /// テキストボックスのアニメータークラス
        /// </summary>
        private TextBoxTextAnimator textBoxTextAnimator;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="textBox">テキストボックスクラスの参照</param>
        /// <param name="textBoxTextAnimator">テキストボックスのアニメータークラスの参照</param>
        public TextBoxTextAnimatorPresenter(TextBox.TMP_DOTweenTextBox textBox, TextBoxTextAnimator textBoxTextAnimator)
        {
            this.textBox = textBox;
            this.textBoxTextAnimator = textBoxTextAnimator;
            
            //イベント購読
            Subscribe();
        }

        /// <summary>
        /// イベント購読
        /// </summary>
        private void Subscribe()
        {
            //表示アニメーション開始と表示アニメーションスキップを購読
            textBox.textBoxEvents.onStringShowStart.AddListener(TextBoxToAnimator);
            textBox.textBoxEvents.onStringShowSkip.AddListener(AnimationSkip);
        }
        
        /// <summary>
        /// テキストボックスからアニメーターへ
        /// アニメーションの開始
        /// </summary>
        /// <param name="data">イベントキーデータとインデックスデータ</param>
        private void TextBoxToAnimator((string key,int index) data)
        {
            textBoxTextAnimator.PlayAnimation();
        }

        /// <summary>
        /// アニメーションのスキップ
        /// </summary>
        private void AnimationSkip()
        {
            textBoxTextAnimator.AnimationSkip();
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            textBox.textBoxEvents.onStringShowStart.RemoveListener(TextBoxToAnimator);
            textBox.textBoxEvents.onStringShowSkip.RemoveListener(AnimationSkip);
        }
    }
}