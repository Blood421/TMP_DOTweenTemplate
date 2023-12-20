using System;
using DG.Tweening;
using TMP_DOTweenTemplate.Core.Base;
using TMPro;
using UnityEngine;

namespace TMP_DOTweenTemplate.Demo
{
    /// <summary>
    /// デモ用アニメーション一覧
    /// </summary>
    public class DemoAnimationGallery : MonoBehaviour
    {
        [SerializeField] private GameObject animatorParent;
        [SerializeField] private TextMeshProUGUI nameText;

        [Header("!!Read Only!!")]
        [SerializeField] private AnimatorBase[] animators;
        private int nowIndex = 0;

        private void Awake()
        {
            //DOTweenの初期設定
            DOTween.Init(false, false, LogBehaviour.ErrorsOnly).SetCapacity(400, 100);
        }

        private void Start()
        {
            //初期化
            animators = animatorParent.GetComponentsInChildren<AnimatorBase>();

            foreach (AnimatorBase animatorBase in animators)
            {
                animatorBase.gameObject.SetActive(false);
            }
            
            animators[nowIndex].gameObject.SetActive(true);
            animators[nowIndex].Play();
            nameText.text = animators[nowIndex].name;
        }

        private void Update()
        {
            //spaceで次へ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animators[nowIndex].gameObject.SetActive(false);
                animators[nowIndex].Dispose();
                UpdateIndex(1);
                animators[nowIndex].gameObject.SetActive(true);
                animators[nowIndex].Play();
                nameText.text = animators[nowIndex].name;
            }
            if(nowIndex == animators.Length -1) Destroy(this);
        }

        private void UpdateIndex(int add)
        {
            nowIndex = Mathf.Clamp(nowIndex + add, nowIndex, animators.Length - 1);
        }
    }
}