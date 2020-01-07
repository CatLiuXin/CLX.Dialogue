using UnityEditor;
using UnityEngine;
using System.Linq;
using CLX.Extensions.Generic;
using System.Text;

namespace CLX.Dialogue
{
    public class DialogueEditor : EditorWindow
    {
        DialogueSetting mSetting;
        Dialogue mDialogue;

        bool dialogueLegal = false;
        int nowDialogueClipNumber = 0;
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
                nowDialogueClipNumber = 0;
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
            window.titleContent = new GUIContent(" CLX.Dialogue可视化编辑器 ");
            window.minSize = new Vector2(400, 400);
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(" CLX.Dialogue可视化编辑器 ");
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

            /// 获取所有对白片段
            var clips = MDialogue.dialogueClips;
            var nowClip = clips[nowDialogueClipNumber];

            #region 验证表情
            Role nowRole = MSetting.GetRoleByName(nowClip.roleName);
            RoleImage nowImage = nowRole.GetImageByEmotion(nowClip.roleEmotion);
            if(nowImage == null)
            {
                EditorGUILayout.HelpBox(string.Concat("您所选择的Setting文件中的",nowRole.roleName,
                    "角色并不包含",nowClip.roleEmotion,"表情"), MessageType.Warning);
                return;
            }
            #endregion

            #region 主体绘制
            EditorGUILayout.BeginHorizontal();
            var sprite = nowRole.GetImageByEmotion(nowClip.roleEmotion).sprite;
            var targetTex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            var pixels = sprite.texture.GetPixels(
                (int)sprite.textureRect.x,
                (int)sprite.textureRect.y,
                (int)sprite.textureRect.width,
                (int)sprite.textureRect.height);
            targetTex.SetPixels(pixels);
            targetTex.Apply();

            GUILayout.Space(10);
            float scale = Mathf.Min(sprite.textureRect.height / sprite.textureRect.width, 4);
            GUI.DrawTexture(GUILayoutUtility.GetRect((position.width / 2), 0,
                GUILayout.MaxHeight(200*scale), GUILayout.MaxWidth(200)), targetTex);

            EditorGUILayout.EndHorizontal();
            #endregion

            #region Event Mask 绘制
            _labelStyle.normal.textColor = new Color(0, 0, 0);
            EditorGUILayout.LabelField("Event Mask", _labelStyle);
            for (int i = 0; i < MSetting.maskNames.Length;)
            {
                EditorGUILayout.BeginHorizontal();
                for(int j = 0; j < MSetting.maskColumeCount; j++)
                {
                    if((i+j)>= MSetting.maskNames.Length)
                    {
                        break;
                    }
                    var trigger = EditorGUILayout.Toggle(MSetting.maskNames[i + j],
                        nowClip.HasMaskBit(1 << (i + j)));
                    if (trigger)
                    {
                        nowClip.AddMaskBit(1 << (i + j));
                    }
                    else
                    {
                        nowClip.RemoveMaskBit(1 << (i + j));
                    }
                }
                i += MSetting.maskColumeCount;
                EditorGUILayout.EndHorizontal();
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
                if (MSetting.GetRoleByName(roleName)==null)
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