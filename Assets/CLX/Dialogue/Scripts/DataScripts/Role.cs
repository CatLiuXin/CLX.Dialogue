using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    /// <summary>
    /// 角色类 主要包括其名字与其主要图片
    /// </summary>
    [CreateAssetMenu(menuName = "CLX/Dialogue/Create Role")]
    public class Role : ScriptableObject
    {
        public string roleName;
        public List<RoleImage> images;
    }
}
