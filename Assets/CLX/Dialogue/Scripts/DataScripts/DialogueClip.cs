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

        /// 是否包含指定的特殊标记位
        public bool HasMaskBit(int mask)
        {
            return (mask & eventMask) == mask;
        }

        /// 附加指定的特殊标记位
        public void AddMaskBit(int mask)
        {
            eventMask |= mask;
        }

        /// 移除指定的特殊标记位
        public void RemoveMaskBit(int mask)
        {
            eventMask &= (~mask);
        }

        /// 复制另一个clip的信息
        public void CopyBy(DialogueClip clip)
        {
            clipContext = clip.clipContext;
            roleName = clip.roleName;
            roleEmotion = clip.roleEmotion;
            eventMask = clip.eventMask;
        }
    }
}
