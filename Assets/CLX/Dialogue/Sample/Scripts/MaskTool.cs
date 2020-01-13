using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue.Sample
{
    public class MaskTool 
    {
        /// 用于标记存在分支的片段
        public const int BranchClip = 1 << 0;

        /// 用于标记片段结束后则整个对白结束的片段
        public const int DialogueEndClip = 1 << 1;

        /// 用于标记需要更换BGM的片段
        public const int ChangeBGM = 1 << 2;

        /// 用于标记需要更换背景图片的片段
        public const int ChangeBackground = 1 << 3;
    }
}