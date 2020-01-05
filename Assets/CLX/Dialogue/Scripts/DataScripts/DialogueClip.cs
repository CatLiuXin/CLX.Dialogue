using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    [System.Serializable]
    public class DialogueClip
    {
        string  clipContext;
        string  roleName;
        string  roleEmotion;
        bool    atScreenLeft;
        byte    eventMask;
        public string ClipContext { get => clipContext; set => clipContext = value; }
        public string RoleName { get => roleName; set => roleName = value; }
        public string RoleEmotion { get => roleEmotion; set => roleEmotion = value; }
        public bool AtScreenLeft { get => atScreenLeft; set => atScreenLeft = value; }
        public byte EventMask { get => eventMask; set => eventMask = value; }
    }
}
