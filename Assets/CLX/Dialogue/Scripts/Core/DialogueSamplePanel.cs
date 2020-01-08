using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    /// <summary>
    /// 主要用于注册Dialogue事件
    /// </summary>
    public class DialogueSamplePanel : MonoBehaviour, IDialogueMgrRegister
    {
        private Dialogue nowDialogue;
        private IDialogueController dialogueController;
        private int nowClipNumber=0;

        public int NowClipNumber
        {
            get => nowClipNumber;
            set
            {
                OnDialogueClipEnd(nowDialogue.dialogueClips[nowClipNumber]);
                OnDialogueClipEnter(nowDialogue.dialogueClips[value]);
                nowClipNumber = value;
            }
        }

        private void Awake()
        {
            DialogueMgr.Instance.AddRegister(this);
        }

        public void OnDialogueStart(Dialogue dialogue)
        {
            dialogueController.OnDialogueEnter(dialogue);
            /// 此处不通过属性修改，因为通过属性修改的话无法区分Number的变化原因
            /// 有两种将nowClipNumber变化为0的情况
            /// 1. 结束一个对白后开启另一个对白
            /// 2. 从一个对白的某个片段跳转到第0个片段
            /// 此为第一种情况，需要手动调用ClipEnter事件
            nowClipNumber = 0;
            OnDialogueClipEnter(nowDialogue.dialogueClips[0]);
        }

        private void OnDialogueClipEnter(DialogueClip clip)
        {
            dialogueController.OnDialogueClipEnter(clip);
        }

        private void OnDialogueClipEnd(DialogueClip clip)
        {
            dialogueController.OnDialogueClipEnd(clip);
        }

        /// <summary>
        /// 切换到clipCount个片段 若输入的片段号不合法 则触发结束对白事件
        /// </summary>
        public void ClipSwitchTo(int clipCount)
        {
            if(clipCount<0 || clipCount >= nowDialogue.dialogueClips.Count)
            {
                OnDialogueEnd();
                return;
            }

            NowClipNumber = clipCount;
        }

        public void OnDialogueEnd()
        {
            OnDialogueClipEnd(nowDialogue.dialogueClips[nowClipNumber]);
            dialogueController.OnDialogueEnd(nowDialogue);
            nowDialogue = null;
        }

        public void BindController(IDialogueController controller)
        {
            this.dialogueController = controller;
        }
    }

}