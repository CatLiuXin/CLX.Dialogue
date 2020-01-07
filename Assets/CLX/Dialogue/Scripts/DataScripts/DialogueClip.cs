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

        public bool HasMaskBit(int mask)
        {
            return (mask & eventMask) == mask;
        }

        public void AddMaskBit(int mask)
        {
            eventMask |= mask;
        }

        public void RemoveMaskBit(int mask)
        {
            eventMask &= (~mask);
        }

        public void CopyBy(DialogueClip clip)
        {
            clipContext = clip.clipContext;
            roleName = clip.roleName;
            roleEmotion = clip.roleEmotion;
            eventMask = clip.eventMask;
        }
    }
}
