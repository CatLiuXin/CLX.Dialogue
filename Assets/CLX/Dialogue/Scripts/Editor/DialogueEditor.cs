using UnityEditor;
using UnityEngine;
using System.Linq;
using CLX.Extensions.Generic;

namespace CLX.Dialogue
{
    public class DialogueEditor : EditorWindow
    {
        DialogueSetting mSetting;
        Dialogue mDialogue;

        bool dialogueLegal = false;
        string missingName;

        public DialogueSetting MSetting {
            get => mSetting;
            set 
            {
                mSetting = value;
            }
        }
        public Dialogue MDialogue { get => mDialogue;
            set {
                if (mDialogue == value) return;
                mDialogue = value;
                if (mDialogue != null)
                {
                    dialogueLegal = CheckDialogueLegal();
                }
            }
        }

        [MenuItem("CLX/Show Dialogue Design Panel")]
        static void Init()
        {
            var window = (DialogueEditor)EditorWindow.GetWindow(typeof(DialogueEditor));
            window.Show();
        }

        private void OnGUI()
        {
            // Title
            var _labelStyle1 = new GUIStyle(EditorStyles.label);
            _labelStyle1.fontSize = 24;
            EditorGUILayout.LabelField("CLX.Dialogue可视化编辑器", _labelStyle1);
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            #region 验证MSetting与MDialogue的合理性

            MSetting = (DialogueSetting)EditorGUILayout.ObjectField("对白设置", MSetting, typeof(DialogueSetting), false);
            MDialogue = (Dialogue)EditorGUILayout.ObjectField("对白", MDialogue, typeof(Dialogue), false);

            var _labelStyle = new GUIStyle(EditorStyles.label);
            _labelStyle.fontSize = 12;
            _labelStyle.fixedHeight = 30;
            _labelStyle.normal.textColor = new Color(0.8f, 0, 0);
            EditorGUILayout.LabelField("注意请不要在使用此编辑器编辑时自行更改DialogueSetting或者Dialogue文件！", _labelStyle);

            if (MSetting == null) {
                EditorGUILayout.HelpBox("请先选择对白设置文件", MessageType.Warning);
                return;
            }
            if(MDialogue == null)
            {
                EditorGUILayout.HelpBox("请先选择对白文件", MessageType.Warning);
                return;
            }
            if (dialogueLegal == false)
            {
                EditorGUILayout.HelpBox("对白文件出现的"+missingName+"角色不全在设置中，请仔细检查！", MessageType.Warning);
                return;
            }

            #endregion


        }

        /// <summary>
        /// 验证mDialogue里出现的所有的角色都在setting里
        /// </summary>
        bool CheckDialogueLegal()
        {
            var roleNames = MDialogue.dialogueClips.Select(clip => clip.roleName).Distinct();
            foreach(var roleName in roleNames)
            {
                if (!MSetting.roles.Exists(role => role.roleName == roleName))
                {
                    missingName = roleName;
                    return false;
                }
            }
            missingName = null;
            return true;
        }
    }

}