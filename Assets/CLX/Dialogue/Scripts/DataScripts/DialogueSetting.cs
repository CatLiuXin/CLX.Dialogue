using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    public enum DialogueResourceType
    {
        None,
        AudioClip,
        Sprite,
    }

    /// <summary>
    /// 对白系统设置
    /// </summary>
    [CreateAssetMenu(menuName = "CLX/Dialogue/Create Dialogue Setting")]
    public class DialogueSetting : ScriptableObject
    {
        [System.Serializable]
        public class MaskInfo
        {
            public string maskName;
            public DialogueResourceType type;
        }

        /// 用于记录登场的所有角色
        public List<Role> roles;
        /// 特殊对白标记的提示名
        /// 编辑器使用时提示用户的作用
        public MaskInfo[] maskInfos;

        /// <summary>
        /// 编辑器上每行最多的mask数量
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

        public static System.Type GetTypeByResourceType(DialogueResourceType type)
        {
            switch (type)
            {
                case DialogueResourceType.None: return null;
                case DialogueResourceType.Sprite:return typeof(Sprite);
                case DialogueResourceType.AudioClip:return typeof(AudioClip);
            }
            return null;
        }
    }

}