using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    /// <summary>
    /// 对白系统设置
    /// </summary>
    [CreateAssetMenu(menuName = "CLX/Dialogue/Create Dialogue Setting")]
    public class DialogueSetting : ScriptableObject
    {
        public List<Role> roles;
        public string[] maskNames;
    }

}