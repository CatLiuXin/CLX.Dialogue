using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    [System.Serializable]
    public class DialogueClip
    {
        public string clipContext;
        public string roleName;
        public string roleEmotion;
        public int     eventMask;
    }
}
