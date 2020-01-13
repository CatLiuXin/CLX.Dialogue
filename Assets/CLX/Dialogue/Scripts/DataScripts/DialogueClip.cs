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
        [SerializeField]
        private List<Object> maskObjs=new List<Object>();
        [SerializeField]
        private List<int> objMaskBits=new List<int>();
        private Dictionary<int, Object> dict = null;

        public Object GetDialogueObjectByMaskBit(int mask)
        {
            if (dict == null)
            {
                dict = new Dictionary<int, Object>();
                for(int i = 0; i < maskObjs.Count; i++)
                {
                    dict[objMaskBits[i]] = maskObjs[i];
                }
            }
            if (dict.ContainsKey(mask)) return dict[mask];
            return null;
        }

        public void SetDialogueObjectByMaskBit(int mask,Object obj)
        {
            dict[mask] = obj;
            if (objMaskBits.Contains(mask))
            {
                for(int i = 0; i < objMaskBits.Count; i++)
                {
                    if(objMaskBits[i] == mask)
                    {
                        maskObjs[i] = obj;
                    }
                }
            }
            else
            {
                maskObjs.Add(obj);
                objMaskBits.Add(mask);
                dict[mask] = obj;
            }
        }

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
            maskObjs = new List<Object>(clip.maskObjs);
            objMaskBits = new List<int>(clip.objMaskBits);
        }
    }
}
