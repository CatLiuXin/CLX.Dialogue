using CLX.Extensions.Generic;
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
        /// 若勾选removeAll则清空所有Role
        /// 否则将位于selectedRoles内的role取消注册
        /// </summary>
        public void RemoveRoles(bool removeAll=true,params Role[] selectedRoles)
        {
            if (removeAll)
            {
                roles.Clear();
            }
            else
            {
                selectedRoles.ForEach(role => roles.Remove(role.name));
            }
        }

        /// <summary>
        /// 将选中role取消注册
        /// </summary>
        public void RemoveRole(Role role)
        {
            roles.Remove(role.name);
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
        /// 若勾选removeAll则清空所有Helper
        /// 否则将位于selectedHelpers内的helper取消注册
        /// </summary>
        public void RemoveHelpers(bool removeAll=true,params IDialogueMgrHelper[] selectedHelpers)
        {
            if (removeAll)
            {
                helpers.Clear();
            }
            else
            {
                selectedHelpers.ForEach(helper => helpers.Remove(helper));
            }
        }

        /// <summary>
        /// 将选中helper取消注册
        /// </summary>
        public void RemoveHelper(IDialogueMgrHelper helper)
        {
            helpers.Remove(helper);
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
