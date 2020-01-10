using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue.Sample
{
    public class MaskTool 
    {
        /// <summary>
        /// 用于标记存在分支的片段
        /// </summary>
        public const int BranchClip = 1 << 0;

        /// <summary>
        /// 用于标记片段结束后则整个对白结束的片段
        /// </summary>
        public const int DialogueEndClip = 1 << 1;
    }
}