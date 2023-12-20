namespace TMP_DOTweenTextBox.Core.TextBox
{
    /// <summary>
    /// テキストボックス用データクラス
    /// </summary>
    public class TextBoxStringData
    {
        /// <summary>
        /// readonly
        /// 名前
        /// </summary>
        public readonly string name;
        
        /// <summary>
        /// readonly
        /// メインの文字列データ
        /// </summary>
        public readonly string mainData;
        
        /// <summary>
        /// readonly
        /// イベントに使うキー文字列
        /// </summary>
        public readonly string eventKey;
        
        /// <summary>
        /// readonly
        /// 表示完了までにかかる1文字あたりの表示時間(上書き用)
        /// </summary>
        public readonly float oneCharDelaySecForCompleteOverride = -1;
        
        /// <summary>
        /// readonly
        /// 表示完了までにかかる表示時間のオフセット(上書き用)
        /// </summary>
        public readonly float offsetDelaySecOverride = -1;

        /// <summary>
        /// コンストラクタ
        /// 初期化
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="mainData">メインの文字列データ</param>
        /// <param name="eventKey">イベントに使うキー文字列</param>
        public TextBoxStringData(string name, string mainData, string eventKey)
        {
            this.name = name;
            this.mainData = mainData;
            this.eventKey = eventKey;
        }

        /// <summary>
        /// コンストラクタ
        /// 初期化
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="mainData">メインの文字列データ</param>
        /// <param name="eventKey">イベントに使うキー文字列</param>
        /// <param name="oneCharDelaySecForCompleteOverride">表示完了までにかかる1文字あたりの表示時間(上書き用)</param>
        /// <param name="offsetDelaySecOverride">表示完了までにかかる表示時間のオフセット(上書き用)</param>
        public TextBoxStringData(string name, string mainData, string eventKey, float oneCharDelaySecForCompleteOverride, float offsetDelaySecOverride)
        {
            this.name = name;
            this.mainData = mainData;
            this.eventKey = eventKey;
            this.oneCharDelaySecForCompleteOverride = oneCharDelaySecForCompleteOverride;
            this.offsetDelaySecOverride = offsetDelaySecOverride;
        }
    }
}