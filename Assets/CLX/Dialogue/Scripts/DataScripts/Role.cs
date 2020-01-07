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
        public Dictionary<string, RoleImage> imageDict;

        private void OnEnable()
        {
            /// 初始化字典 便于以后查找
            imageDict = new Dictionary<string, RoleImage>();
            foreach(var image in images)
            {
                imageDict[image.emotion] = image;
            }
        }

        /// <summary>
        /// 根据Emotion字符串来获取RoleImage对象，若找不到则返回null
        /// </summary>
        public RoleImage GetImageByEmotion(string emotion)
        {
            return imageDict.ContainsKey(emotion) ? imageDict[emotion] : null;
        }
    }
}
