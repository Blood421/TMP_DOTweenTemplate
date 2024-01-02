using TMP_DOTweenTextBox.Core.TextBox;
using UnityEngine;

namespace TMP_DOTweenTextBox.Demo
{
    /// <summary>
    /// テキストボックスのデモ用クラス
    /// </summary>
    public class TextBoxTester : MonoBehaviour
    {
        /// <summary>
        /// テキストボックスの参照
        /// </summary>
        [SerializeField] private Core.TextBox.TMP_DOTweenTextBox textBox;
        
        private void Awake()
        {
            //表示データの配列
            TextBoxStringData[] dataArray =
            {
                //名前(name),文章(contents),表示開始と終了時に通知する文字(Event string)
                new TextBoxStringData("Name1", "<b>aa</b><Test1>aa</Test1>aa<Test0>aa</Test0><i>aa</i>", "a"),
                new TextBoxStringData("Name2", "b<Test1>bb</Test1>bb", "b"),
                new TextBoxStringData("Name3", "cccc", "c"),
                new TextBoxStringData("Name4", "dddd", "d"),
                new TextBoxStringData("Name5", "eeee", "e"),
            };
            
            //表示データの設定
            textBox.SetTextBoxStringDataArray(dataArray);
        }

        private void Start()
        {
            //イベント設定
            textBox.textBoxEvents.onStart.AddListener(() => Debug.Log("Start"));
            textBox.textBoxEvents.onEnd.AddListener(() => Debug.Log("End"));
            textBox.textBoxEvents.onStringShowStart.AddListener(value => Debug.Log("ShowStart " + value.key + " : " + value.index));
            textBox.textBoxEvents.onStringShowEnd.AddListener(value => Debug.Log("ShowEnd " + value.key + " : " + value.index));

            //テキストボックス開始
            textBox.StartTextBox();
        }

        public void StartTextBox() => textBox.StartTextBox();
        public void Next() => textBox.Next();
        public void Prev() => textBox.Prev();
        public void Stop() => textBox.Stop();
        public void Skip() => textBox.Skip();
    }
}