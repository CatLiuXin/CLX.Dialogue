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
        private Dictionary<string, Role> roleDict;
        public string[] maskNames;
        [Range(1, 32)]
        public short maskColumeCount = 4;

        private void OnEnable()
        {
            roleDict = new Dictionary<string, Role>();
            foreach(var role in roles)
            {
                roleDict[role.roleName] = role;
            }
        }

        /// <summary>
        /// 根据roleName查找Role 若没找到则返回null
        /// </summary>
        public Role GetRoleByName(string roleName)
        {
            return roleDict.ContainsKey(roleName) ? roleDict[roleName] : null;
        }
    }

}