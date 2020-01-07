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

        private void OnEnable()
        {
        }

        /// <summary>
        /// 根据Emotion字符串来获取RoleImage对象，若找不到则返回null
        /// </summary>
        public RoleImage GetImageByEmotion(string emotion)
        {
            foreach(var image in images)
            {
                if (image.emotion == emotion) return image;
            }
            return null;
        }
    }
}
