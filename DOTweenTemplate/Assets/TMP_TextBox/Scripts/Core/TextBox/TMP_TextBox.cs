using TMP_TextBox.Core.Animator;
using TMPro;
using UnityEngine;

namespace TMP_TextBox.Core.TextBox
{
    /// <summary>
    /// TMPテキストボックスクラス
    /// </summary>
    [RequireComponent(typeof(TextBoxTextAnimator))]
    public class TMP_TextBox : MonoBehaviour
    {
        /// <summary>
        /// 名前を表示するテキスト
        /// </summary>
        [SerializeField] private TextMeshProUGUI nameText;
        
        /// <summary>
        /// 名前を表示するテキストの取得
        /// </summary>
        /// <returns>名前を表示するテキスト</returns>
        public TextMeshProUGUI GetNameText() => nameText;
        
        /// <summary>
        /// メインの文字列を表示するテキスト
        /// </summary>
        [SerializeField] private TextMeshProUGUI mainText;
        
        /// <summary>
        /// メインの文字列を表示するテキストの取得
        /// </summary>
        /// <returns>メインの文字列を表示するテキスト</returns>
        public TextMeshProUGUI GetMainText() => mainText;
        
        /// <summary>
        /// 表示完了までにかかる1文字あたりの表示時間
        /// </summary>
        public float oneCharDelaySecForComplete = 0.1f;
        
        /// <summary>
        /// 表示完了までにかかる表示時間のオフセット
        /// </summary>
        public float offsetDelaySec = 1f;
        
        /// <summary>
        /// テキストボックス用のイベント
        /// </summary>
        public TextBoxEvents textBoxEvents;
        
        /// <summary>
        /// テキストボックス表示用データの配列
        /// </summary>
        private TextBoxStringData[] textBoxStringDataArray;
        
        /// <summary>
        /// *** 重要 ***
        /// このメソッドを呼ぶことでこのクラスが使える
        /// テキストボックス表示用データの配列の設定
        /// </summary>
        /// <param name="dataArray">テキストボックス表示用データの配列</param>
        public void SetTextBoxStringDataArray(TextBoxStringData[] dataArray) => textBoxStringDataArray = dataArray;

        /// <summary>
        /// 始まったかどうかのフラグ
        /// </summary>
        private bool isStarted = false;
        
        /// <summary>
        /// アニメーション中かどうかのフラグ
        /// </summary>
        private bool isNowAnimation = false;
        
        /// <summary>
        /// 終わったかどうかのフラグ
        /// </summary>
        private bool isEnd = false;

        /// <summary>
        /// 今表示中の配列のインデックス
        /// </summary>
        private int nowDataIndex = 0;
        
        /// <summary>
        /// 今の表示データ
        /// </summary>
        private TextBoxStringData nowData = null;

        /// <summary>
        /// 表示終了のための時間(sec)
        /// </summary>
        private float finishSec = 0;
        
        private void Update()
        {
            //表示が始まったときに時間経過する
            //時間経過が終わったら表示を終了させる
            if (!isNowAnimation) return;
            
            finishSec -= Time.deltaTime;
            if (finishSec < 0)
            {
                //表示終わり
                StringShowComplete();
            }
        }

        /// <summary>
        /// テキストボックスの表示開始
        /// </summary>
        public void StartTextBox()
        {
            //テキストボックスに表示するデータがなければエラー
            if (textBoxStringDataArray == null)
            {
                Debug.LogError("TextBoxStringDataArray is Null!!");
                return;
            }

            //初期化
            isStarted = true;
            isEnd = false;
            isNowAnimation = false;

            nowDataIndex = 0;
            mainText.text = "";
            nameText.text = "";
            nowData = null;
            
            //イベント
            textBoxEvents.onStart.Invoke();
            //表示テキストを更新
            UpdateText(textBoxStringDataArray[nowDataIndex]);
        }

        /// <summary>
        /// 表示を次へ
        /// </summary>
        public void Next()
        {
            //アニメーション中ならアニメーションをスキップ
            if (isNowAnimation)
            {
                //イベント
                textBoxEvents.onStringShowSkip.Invoke();
                //今の表示を終わりに
                StringShowComplete();
                return;
            }
            
            //終わっていたらスキップ
            if (isEnd) return;
            //最後の文字列を表示後に呼ばれたら終わりにしてスキップ
            if (nowDataIndex + 1 >= textBoxStringDataArray.Length)
            {
                End();
                return;
            }
            
            //インデックスを+1して表示テキストを更新
            nowDataIndex++;
            UpdateText(textBoxStringDataArray[nowDataIndex]);
        }

        /// <summary>
        /// 表示をひとつ前へ
        /// </summary>
        public void Prev()
        {
            //終わっていたらスキップ
            if (isEnd) return;
            //最初の文字列表示中の場合スキップ
            if (nowDataIndex - 1 < 0) return;
            
            //インデックスを-1して表示テキストを更新
            nowDataIndex--;
            UpdateText(textBoxStringDataArray[nowDataIndex]);
        }
        
        /// <summary>
        /// 表示テキストを更新
        /// </summary>
        /// <param name="data"></param>
        private void UpdateText(TextBoxStringData data)
        {
            //データを更新
            nowData = data;
            nameText.text = nowData.name;
            mainText.text = nowData.mainData;
            isNowAnimation = true;
            
            //イベント
            textBoxEvents.onStringShowStart.Invoke((nowData.eventKey,nowDataIndex));
            
            //表示終了までの時間を更新
            //もし上書きデータがあればそれを使う
            float oneCharDelay = oneCharDelaySecForComplete;
            float offsetDelay = offsetDelaySec;
            if (nowData.oneCharDelaySecForCompleteOverride >= 0) oneCharDelay = nowData.oneCharDelaySecForCompleteOverride;
            if (nowData.offsetDelaySecOverride >= 0) offsetDelay = nowData.offsetDelaySecOverride;
            finishSec = oneCharDelay * mainText.GetTextInfo(mainText.text).characterCount + offsetDelay;
        }

        /// <summary>
        /// 文字列の表示終了
        /// </summary>
        private void StringShowComplete()
        {
            //フラグ更新
            isNowAnimation = false;
            //イベント
            textBoxEvents.onStringShowEnd.Invoke((nowData.eventKey,nowDataIndex));
        }

        /// <summary>
        /// 表示を最後までスキップ
        /// </summary>
        public void Skip()
        {
            //終わっていたらスキップ
            if (isEnd) return;
            //データ更新
            nowDataIndex = textBoxStringDataArray.Length - 1;
            //表示テキストを更新
            UpdateText(textBoxStringDataArray[nowDataIndex]);
        }

        /// <summary>
        /// 終了させる
        /// </summary>
        public void Stop()
        {
            //終了していたらスキップ
            if (isEnd) return;
            End();
        }

        /// <summary>
        /// 終了
        /// </summary>
        private void End()
        {
            //フラグ更新
            isEnd = true;
            //イベント
            textBoxEvents.onEnd.Invoke();
        }
    }
}