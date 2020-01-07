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

        /// <summary>
        /// 编辑器上每行应有的mask数量
        /// </summary>
        [Range(1, 32)]
        public short maskColumeCount = 4;

        /// <summary>
        /// 根据roleName查找Role 若没找到则返回null
        /// </summary>
        public Role GetRoleByName(string roleName)
        {
            for(int i = 0; i < roles.Count; i++)
            {
                if(roles[i] .roleName == roleName)
                {
                    return roles[i];
                }
            }
            return null;
        }
    }

}