using System;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    /*
     * 此脚本各事件的执行顺序
     * 加载对白后，先调用所有注册的OnDialogueEnter事件
     *      对每一个对白片段，进行以下的事件循环
     *      1. OnDialogueClipEnter
     *      2. OnSpecialDialogueEnter
     *      3. OnSpecialDialogueEnd
     *      4. OnDialogueClipEnd
     *      值得注意的是，执行Enter系列事件的时候，OnDilogueClip的顺序在OnSpecialDialogue之前
     *      而End系列事件反之
     * 最后执行OnDialogueEnter事件
     */
    public class EventDialogueController : MonoBehaviour, IDialogueController
    {
        public event Action<Dialogue> OnDialogueEnterEvent;
        public event Action<Dialogue> OnDialogueEndEvent;
        public event Action<DialogueClip> OnDialogueClipEnterEvent;
        public event Action<DialogueClip> OnDialogueClipEndEvent;

        /// <summary>
        /// 使用Dictionary记录mask对应的所有事件
        /// </summary>
        private Dictionary<int, Action<DialogueClip>> onSpecialDialogueEnterDict = new Dictionary<int, Action<DialogueClip>>();
        private Dictionary<int, Action<DialogueClip>> onSpecialDialogueEndDict = new Dictionary<int, Action<DialogueClip>>();
        private DialogueSamplePanel panel;

        private void Awake()
        {
            panel = GetComponent<DialogueSamplePanel>();
            panel.BindController(this);
        }
        public void OnDialogueClipEnd(DialogueClip clip)
        {
            var mask = clip.eventMask;
            ExecuteMaskEvent(onSpecialDialogueEndDict, mask, clip);
            OnDialogueClipEndEvent?.Invoke(clip);
        }

        public void OnDialogueClipEnter(DialogueClip clip)
        {
            OnDialogueClipEnterEvent?.Invoke(clip);
            var mask = clip.eventMask;
            ExecuteMaskEvent(onSpecialDialogueEnterDict, mask, clip);
        }

        public void OnDialogueEnd(Dialogue dialogue)
        {
            OnDialogueEndEvent?.Invoke(dialogue);
        }

        public void OnDialogueEnter(Dialogue dialogue)
        {
            OnDialogueEnterEvent?.Invoke(dialogue);
        }

        /// <summary>
        /// 将action注册为mask所对应的Enter事件
        /// </summary>
        public void RegisterSpecialClipEnterAction(int mask,Action<DialogueClip> action)
        {
            if (action == null) {
                Debug.Log("action is null");
                return;
            }
            if (onSpecialDialogueEnterDict.ContainsKey(mask))
            {
                onSpecialDialogueEnterDict[mask] += action;
            }
            else
            {
                onSpecialDialogueEnterDict[mask] = action;
            }
        }

        /// <summary>
        /// 将action注册为mask所对应的End事件
        /// </summary>
        public void RegisterSpecialClipEndAction(int mask, Action<DialogueClip> action)
        {
            if (action == null)
            {
                Debug.Log("action is null");
                return;
            }
            if (onSpecialDialogueEndDict.ContainsKey(mask))
            {
                onSpecialDialogueEndDict[mask] += action;
            }
            else
            {
                onSpecialDialogueEndDict[mask] = action;
            }
        }

        /// <summary>
        /// 执行mask相对应的所有注册的事件
        /// </summary>
        private void ExecuteMaskEvent(Dictionary<int, Action<DialogueClip>> dict, int mask,DialogueClip clip)
        {
            for(int i = 0; i < 32 && mask != 0; i++)
            {
                int tmpMask = (1 << i);
                if((mask&tmpMask) == tmpMask)
                {
                    if (dict.ContainsKey(tmpMask))
                    {
                        dict[tmpMask]?.Invoke(clip);
                    }
                    mask &= (~tmpMask);
                }
            }
        }

        public void ShowNextClip()
        {
            panel.ShowNextClip();
        }

        public void ClipSwitchTo(int clipCount)
        {
            panel.ClipSwitchTo(clipCount);
        }
    }
}