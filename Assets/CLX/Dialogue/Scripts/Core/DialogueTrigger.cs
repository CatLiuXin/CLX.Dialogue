using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    /// <summary>
    /// 用于对白触发
    /// </summary>
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField]
        private Dialogue dialogue=null;

        public Dialogue Dialogue { get => dialogue; set => dialogue = value; }

        /// <summary>
        /// 开启一段对白
        /// </summary>
        public void BeginDialogue()
        {
            DialogueMgr.Instance.DoDialogue(Dialogue);
        }
    }
}
