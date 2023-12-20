using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace TMP_DOTweenTagAnimator.Editor
{
    /// <summary>
    /// アニメーション設定アセット用クラスを生成するエディタ拡張クラス
    /// </summary>
    public class TMP_AnimationSettingsCreatorWindow : EditorWindow
    {
        /// <summary>
        /// アニメーションの種類
        /// </summary>
        private static TMP_AnimationSettingsCreatorContentType[] tmpAnimationSettingsCreatorContentTypes;
        
        /// <summary>
        /// 生成するフォルダのパス
        /// </summary>
        private static string folderPath;
        
        /// <summary>
        /// 生成するクラスの名前
        /// </summary>
        private static string className;
        
        /// <summary>
        /// アニメーションの種類の数
        /// </summary>
        private static int arrayLength;
        
        /// <summary>
        /// 設定したアニメーションの種類から設定するアニメーションのクラス名に変換するDictionary
        /// </summary>
        private static Dictionary<TMP_AnimationSettingsCreatorContentType, string> contentTypeConverter;
        
        //ヘッダーとフッター
        
        private static string fileHeader =
            @"using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using UnityEngine;

namespace TMP_DOTweenTagAnimator.Assets
{
    [CreateAssetMenu(fileName = """ + className + @""", menuName = ""TMP_DOTweenTagAnimator/" + className +
            @""", order = 1)]
    public class " + className + @" : TMP_AnimationSettingsBase
    {";
        // [SerializeField] private TMP_CharMoveAnimation tmpCharMoveAnimation;
        // [SerializeField] private TMP_CharColorAnimation tmpCharColorAnimation;
        private static string fileMethodHeader = @"
         public override CharAnimationBase[] GetCharAnimations()
         {";
               //return new CharAnimationBase[] {tmpCharMoveAnimation,tmpCharColorAnimation};
               private static string fileMethodFooter = @"
         }";
        
        private static string fileFooter = @"
    }
}";

        [MenuItem ("Tools/TMP_AnimationSettings/CreatorWindow")]
        public static void  ShowWindow ()
        {
            //コンバーターを初期化
            ConverterInit();
            //デフォルトクラス名
            className = "TMP_AnimationSettings";
            //デフォルトパス
            folderPath = Application.dataPath + "/TMP_DOTweenTagAnimator/Scripts/Assets";
            //デフォルトアニメーション數
            arrayLength = 1;
            //ウィンドウを生成
            GetWindow(typeof(TMP_AnimationSettingsCreatorWindow));
            
        }

        private void OnGUI () 
        {
            //タイトルラベルとコンバーター初期化
            GUILayout.Label("Create AnimationAssetScript");
            ConverterInit();

            //クラス名とパスを設定
            SetClassName();
            SetPath();
            
            //仕切り描画
            DrawLine();
            GUILayout.Space(10);
            
            //配列の長さを設定
            SetArrayLength();
            GUILayout.Space(10);
            
            //配列を設定
            SetArray();
            GUILayout.Space(10);

            //仕切り描画
            DrawLine();
            GUILayout.Space(10);
            
            //配列がnullもしくは長さは例外なのでなにかを入れる
            if (tmpAnimationSettingsCreatorContentTypes == null)
            {
                tmpAnimationSettingsCreatorContentTypes = new[]
                {
                    TMP_AnimationSettingsCreatorContentType.Color
                }; 
            }

            if (tmpAnimationSettingsCreatorContentTypes.Length <= 0)
            {
                tmpAnimationSettingsCreatorContentTypes = new[]
                {
                    TMP_AnimationSettingsCreatorContentType.Color
                }; 
            }
            
            //ボタンを押したらセーブ
            if (GUILayout.Button("- Create -"))
            {
                Save();
            }
        }

        /// <summary>
        /// コンバーターの初期化
        /// </summary>
        private static void ConverterInit()
        {
            //nullなら生成
            if (contentTypeConverter != null) return;
            contentTypeConverter = new Dictionary<TMP_AnimationSettingsCreatorContentType, string>()
            {
                {(TMP_AnimationSettingsCreatorContentType) 0,"TMP_CharColorAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 1,"TMP_CharColorGradientAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 2,"TMP_CharColorTimeGradientAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 3,"TMP_CharFadeAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 4,"TMP_CharMoveAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 5,"TMP_CharMoveCurveAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 6,"TMP_CharPunchMoveAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 7,"TMP_CharPunchRotateAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 8,"TMP_CharPunchScaleAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 9,"TMP_CharRotateAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 10,"TMP_CharRotateCurveAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 11,"TMP_CharScaleAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 12,"TMP_CharScaleCurveAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 13,"TMP_CharShakeMoveAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 14,"TMP_CharShakeRotateAnimation"},
                {(TMP_AnimationSettingsCreatorContentType) 15,"TMP_CharShakeScaleAnimation"},
            };
        }

        /// <summary>
        /// クラス名を設定
        /// </summary>
        private void SetClassName()
        {
            //nullもしくは何もなければデフォルト値
            if (string.IsNullOrEmpty(className)) className = "TMP_AnimationSettings";
            className = EditorGUILayout.TextField("Class Name", className);
        }

        /// <summary>
        /// パスを設定
        /// </summary>
        private void SetPath()
        {
            //nullもしくは何もなければデフォルト値
            if (string.IsNullOrEmpty(folderPath)) folderPath = Application.dataPath + "/TMP_DOTweenTagAnimator/Scripts/Assets";
            string path = EditorGUILayout.TextField("Save Path", folderPath);

            //パスの設定用パネルを開くボタン
            if (GUILayout.Button("Save Path Panel")) {
                // 保存先のファイルパスを取得する
                string pathTemp = EditorUtility.SaveFolderPanel("ファイルの保存先", path,"");
                
                // キャンセルじゃなければ保存
                if (!string.IsNullOrEmpty(pathTemp)) 
                {
                    folderPath = pathTemp;
                }
            }
        }

        /// <summary>
        /// 線を引くだけ
        /// </summary>
        private void DrawLine()
        {
            var splitterRect = EditorGUILayout.GetControlRect(false, GUILayout.Height(1));
            splitterRect.x = 0;
            splitterRect.width = position.width;
            EditorGUI.DrawRect(splitterRect, Color.Lerp(Color.gray, Color.black, 0.7f));
        }

        /// <summary>
        /// アニメーションの数の設定
        /// 0はありえない
        /// </summary>
        private void SetArrayLength()
        {
            int arrayLengthTemp = EditorGUILayout.IntField("Array Length", arrayLength);
            if (arrayLengthTemp == arrayLength) return;
            if (arrayLengthTemp <= 0) arrayLengthTemp = 1;
            arrayLength = arrayLengthTemp;
            tmpAnimationSettingsCreatorContentTypes = new TMP_AnimationSettingsCreatorContentType[arrayLength];
        }

        /// <summary>
        /// 配列の設定
        /// </summary>
        private void SetArray()
        {
            //アニメーションの数から配列を設定
            for (int i = 0; i < arrayLength; i++)
            {
                var type = (TMP_AnimationSettingsCreatorContentType)EditorGUILayout.EnumPopup("Index " + i + " Animation Type", tmpAnimationSettingsCreatorContentTypes[i]);
                if( type == tmpAnimationSettingsCreatorContentTypes[i]) continue;
                tmpAnimationSettingsCreatorContentTypes[i] = type;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            //アニメーションのクラスの名前
            string[] classNames = new string[tmpAnimationSettingsCreatorContentTypes.Length];
            //アニメーションのクラスの変数名
            string[] classVariableNames = new string[tmpAnimationSettingsCreatorContentTypes.Length];
            //アニメーションのクラスの名前と変数名以外の部分
            string[] variableLines = new string[tmpAnimationSettingsCreatorContentTypes.Length];
            string variablesLine = "";
            
            //配列の長さ分
            //アニメーションクラスの変数定義部分を作る
            for (var i = 0; i < tmpAnimationSettingsCreatorContentTypes.Length; i++)
            {
                var classType = tmpAnimationSettingsCreatorContentTypes[i];
                //クラス名
                classNames[i] = contentTypeConverter[classType];
                
                //変数名に重複番号を振るためのカウント
                string findName = classNames[i];
                int count = 0;
                for (var j = 0; j < classNames.Length; j++)
                {
                    if(string.IsNullOrEmpty(classNames[j])) continue;
                    if(j == i) continue;
                    if (classNames[j] == findName) count++;
                }
                
                //クラスの変数名
                char head = char.ToLower(classNames[i][0]);
                string classNameWithoutHead = classNames[i].Remove(0, 1);
                classVariableNames[i] = head + classNameWithoutHead + count.ToString();
                
                //変数定義部分
                variableLines[i] = "        [SerializeField] private " + 
                                   classNames[i] + " " +
                                   classVariableNames[i] + ";\n";

                variablesLine += variableLines[i];
            }

            //メソッドの返り値部分
            string returnLine = "              return new CharAnimationBase[]" + "\n" +"              " + "{" + "\n";
            for (var i = 0; i < tmpAnimationSettingsCreatorContentTypes.Length; i++)
            {
                returnLine += "                  ";
                returnLine += classVariableNames[i] + "," + "\n";
            }
            returnLine += "              " + "};";
            
            fileHeader =
                @"using TMP_DOTweenTemplate.Core.Base;
using TMP_DOTweenTemplate.Core.Main.Pro;
using UnityEngine;

namespace TMP_DOTweenTagAnimator.Assets
{
    [CreateAssetMenu(fileName = """ + className + @""", menuName = ""TMP_DOTweenTagAnimator/" + className +
                @""", order = 1)]
    public class " + className + @" : TMP_AnimationSettingsBase
    {";

            //ファイルに書き込む文字列
            string content = fileHeader + "\n" +
                            variablesLine +
                            fileMethodHeader + "\n" +
                            returnLine +
                            fileMethodFooter +
                            fileFooter + "\n";
            try
            {
                //書きこみをしてファイル生成
                using (FileStream file = File.Open(folderPath + @"\" + className + ".cs", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        writer.Write(content);
                        writer.Flush();
                    }

                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
            
            //リフレッシュ
            AssetDatabase.Refresh();
        }
    }
}