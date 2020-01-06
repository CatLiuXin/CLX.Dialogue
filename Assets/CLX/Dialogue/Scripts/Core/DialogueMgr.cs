using System.Collections.Generic;

namespace CLX.Dialogue
{
    public class DialogueMgr : Singleton<DialogueMgr>
    {
        List<IDialogueMgrHelper> helpers = new List<IDialogueMgrHelper>();
        Dictionary<string, Role> roles = new Dictionary<string, Role>();

        /// <summary>
        /// 注册一个新Role
        /// </summary>
        public void AddRole(Role role)
        {
            roles.Add(role.name, role);
        }

        /// <summary>
        /// 根据name返回对应的Role 不存在则返回null
        /// </summary>
        public Role GetRoleByName(string name)
        {
            if (roles.ContainsKey(name))
            {
                return roles[name];
            }
            return null;
        }

        /// <summary>
        /// 注册一个IDialogueMgrHelper
        /// </summary>
        public void AddHelper(IDialogueMgrHelper helper)
        {
            helpers.Add(helper);
        }

        /// <summary>
        /// 开启一段对白
        /// </summary>
        public void DoDialogue(Dialogue dialogue)
        {
            helpers.ForEach(helper => helper.OnDialogueStart(dialogue));
        }
    }
}
