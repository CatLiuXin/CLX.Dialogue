using CLX.Extensions.Generic;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    public class DialogueMgr : Singleton<DialogueMgr>
    {
        List<IDialogueMgrRegister> registers = new List<IDialogueMgrRegister>();
        Dictionary<string, Role> roles = new Dictionary<string, Role>();

        DialogueMgr() { }

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
        /// 注册一个IDialogueMgrRegister
        /// </summary>
        public void AddRegister(IDialogueMgrRegister helper)
        {
            registers.Add(helper);
        }

        /// <summary>
        /// 若勾选removeAll则清空所有Register
        /// 否则将位于selectedHelpers内的Register取消注册
        /// </summary>
        public void RemoveRegisters(bool removeAll=true,params IDialogueMgrRegister[] selectedRegisters)
        {
            if (removeAll)
            {
                registers.Clear();
            }
            else
            {
                selectedRegisters.ForEach(register => registers.Remove(register));
            }
        }

        /// <summary>
        /// 将选中Register取消注册
        /// </summary>
        public void RemoveRegister(IDialogueMgrRegister helper)
        {
            registers.Remove(helper);
        }

        /// <summary>
        /// 开启一段对白
        /// </summary>
        public void DoDialogue(Dialogue dialogue)
        {
            registers.ForEach(helper => helper.OnDialogueStart(dialogue));
        }

        /// <summary>
        /// 根据roleName和emotion找到对应的Sprite，找不到则返回null
        /// </summary>
        public Sprite GetSpriteByRoleEmotion(string roleName,string emotion)
        {
            var role = GetRoleByName(roleName);
            return (role == null) ? null : role.GetImageByEmotion(emotion).sprite;
        }
    }
}
