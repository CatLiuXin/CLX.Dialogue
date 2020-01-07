using UnityEditor;
using UnityEngine;
using System.Linq;
using CLX.Extensions.Generic;
using System.Collections.Generic;
using System.Text;

namespace CLX.Dialogue
{
    public class DialogueEditor : EditorWindow
    {
        DialogueSetting mSetting;
        Dialogue mDialogue;
        DialogueClip mNowClip;
        DialogueClip mCopyClip = new DialogueClip();

        Dictionary<Sprite, Texture2D> texCache = new Dictionary<Sprite, Texture2D>();
        Dictionary<string, int> roleNameDict;
        Dictionary<string, int> roleEmotionDict;
        string[] roleNameList;
        string[] roleEmotionList;

        bool dialogueLegal = false;
        bool touchBound = false;
        int nowDialogueClipNumber = 0;
        string missingName;

        public DialogueSetting MSetting {
            get => mSetting;
            set 
            {
                if (mSetting == value) return;
                mSetting = value;
                roleNameDict = new Dictionary< string,int>();
                roleNameList = new string[mSetting.roles.Count];
                for(int i = 0; i < mSetting.roles.Count; i++)
                {
                    roleNameDict[mSetting.roles[i].roleName] = i;
                    roleNameList[i] = (mSetting.roles[i].roleName);
                }
            }
        }
        public Dialogue MDialogue { get => mDialogue;
            set {
                if (mDialogue == value) return;
                mDialogue = value;
                if(mDialogue.dialogueClips.Count == 0)
                {
                    AddClip(0);
                }
                nowDialogueClipNumber = 0;
                if (mDialogue != null)
                {
                    dialogueLegal = CheckDialogueLegal();
                }
            }
        }

        public DialogueClip MNowClip { get => mNowClip;
            set {
                if (MNowClip == value) return;
                mNowClip = value;
                mCopyClip.CopyBy(value);
            }
        }


        [MenuItem("CLX/Show Dialogue Design Panel")]
        static void Init()
        {
            var window = (DialogueEditor)EditorWindow.GetWindow(typeof(DialogueEditor));
            window.titleContent = new GUIContent(" CLX.Dialogue可视化编辑器 ");
            window.minSize = new Vector2(500, 500);
            window.maxSize = new Vector2(600, 650);
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUIUtility.labelWidth = 80f;
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
            MNowClip = nowClip;

            /// 绘制拷贝的clip 以防止修改信息的时候直接作用于文件
            nowClip = mCopyClip;
            Role nowRole = MSetting.GetRoleByName(nowClip.roleName);
            RoleImage nowImage = nowRole.GetImageByEmotion(nowClip.roleEmotion);

            #region 验证表情是否在Setting文件中
            if (nowImage == null)
            {
                EditorGUILayout.HelpBox(string.Concat("您所选择的Setting文件中的",nowRole.roleName,
                    "角色并不包含",nowClip.roleEmotion,"表情"), MessageType.Warning);
                return;
            }
            #endregion

            #region 主体绘制
            EditorGUILayout.BeginHorizontal();
            var sprite = nowRole.GetImageByEmotion(nowClip.roleEmotion).sprite;
            Texture2D targetTex = null;
            /// 如果在缓存中能找到需要的 texture 则直接使用
            if (texCache.ContainsKey(sprite))
            {
                targetTex = texCache[sprite];
            }
            else
            {
                targetTex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                var pixels = sprite.texture.GetPixels(
                    (int)sprite.textureRect.x,
                    (int)sprite.textureRect.y,
                    (int)sprite.textureRect.width,
                    (int)sprite.textureRect.height);
                targetTex.SetPixels(pixels);
                targetTex.Apply();
                texCache[sprite] = targetTex;
            }

            /// 绘制角色图像
            GUILayout.Space(10);
            float scale = Mathf.Min(sprite.textureRect.height / sprite.textureRect.width, 4);
            GUI.DrawTexture(GUILayoutUtility.GetRect((position.width / 2), 0,
                GUILayout.MaxHeight(160*scale), GUILayout.MaxWidth(160)), targetTex);

            /// 绘制对白片段的信息
            EditorGUILayout.BeginVertical();

            var tmpName = nowClip.roleName;
            
            var nameId = EditorGUILayout.Popup("角色名", roleNameDict[tmpName], roleNameList,GUILayout.MaxWidth(400));
            tmpName = roleNameList[nameId];


            /// 如果更改了角色的话，就让他的表情表示为默认的
            if (tmpName != nowClip.roleName)
            {
                nowClip.roleName = tmpName;
                nowRole = MSetting.GetRoleByName(nowClip.roleName);
                nowClip.roleEmotion = nowRole.images[0].emotion;
                EmotionLoad(nowRole);
            }

            if (roleEmotionList == null)
            {
                EmotionLoad(nowRole);
            }

            var emotionId = EditorGUILayout.Popup("表情", roleEmotionDict[nowClip.roleEmotion], roleEmotionList, GUILayout.MaxWidth(400));
            nowClip.roleEmotion = roleEmotionList[emotionId];

            nowClip.clipContext = EditorGUILayout.TextField("对白内容", nowClip.clipContext, GUILayout.MaxWidth(400), GUILayout.MaxHeight(80));
            #endregion

            #region 按钮绘制

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(" 应用修改 ", GUILayout.MaxWidth(200)))
            {
                MNowClip.CopyBy(nowClip);
            }

            if (GUILayout.Button(" 重置 ", GUILayout.MaxWidth(200)))
            {
                nowClip.CopyBy(MNowClip);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            /// 记录切换前的片段的角色名
            /// 若切换片段后角色名改变，则要重新加载它的表情包
            var nameBeforeChangeClip = nowRole.roleName;
            
            if (GUILayout.Button(" 上一个片段 ", GUILayout.MaxWidth(200)))
            {
                if (nowDialogueClipNumber == 0)
                {
                    touchBound = true;
                }
                else
                {
                    touchBound = false;
                    nowDialogueClipNumber--;
                }
            }

            if (GUILayout.Button(" 下一个片段 ", GUILayout.MaxWidth(200)))
            {
                if (nowDialogueClipNumber == MDialogue.dialogueClips.Count - 1)
                {
                    touchBound = true;
                }
                else
                {
                    touchBound = false;
                    nowDialogueClipNumber++;
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(" 加入新片段 ", GUILayout.MaxWidth(200)))
            {
                AddClip(nowDialogueClipNumber+1);
            }

            if (GUILayout.Button(" 删除该片段 ", GUILayout.MaxWidth(200)))
            {
                RemoveClip(nowDialogueClipNumber);
            }

            if(nameBeforeChangeClip != MDialogue.dialogueClips[nowDialogueClipNumber].roleName)
            {
                EmotionLoad(MSetting.GetRoleByName(MDialogue.dialogueClips[nowDialogueClipNumber].roleName));
            }

            EditorGUILayout.EndHorizontal();

            if (touchBound)
            {
                EditorGUILayout.HelpBox("向前或向后已经不存在片段了！", MessageType.Warning);
            }

            EditorGUILayout.EndVertical();

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


            EditorGUIUtility.labelWidth = 0;
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

        void AddClip(int index)
        {
            MDialogue.dialogueClips.Insert(index, new DialogueClip());
            MDialogue.dialogueClips[index].roleName = MSetting.roles[0].roleName;
            MDialogue.dialogueClips[index].roleEmotion = MSetting.roles[0].images[0].emotion;
            nowDialogueClipNumber = index;
            MNowClip = MDialogue.dialogueClips[nowDialogueClipNumber];
            touchBound = false;
        }

        void RemoveClip(int index)
        {
            MDialogue.dialogueClips.RemoveAt(index);
            if (MDialogue.dialogueClips.Count == 0)
            {
                AddClip(0);
            }
            nowDialogueClipNumber = Mathf.Max(0, index - 1);
            MNowClip = MDialogue.dialogueClips[nowDialogueClipNumber];
        }

        void EmotionLoad(Role nowRole)
        {
            /// 获得emotion的index to string 映射
            roleEmotionList = new string[nowRole.images.Count];
            roleEmotionDict = new Dictionary<string, int>();
            for (int i = 0; i < nowRole.images.Count; i++)
            {
                roleEmotionList[i] = nowRole.images[i].emotion;
                roleEmotionDict[nowRole.images[i].emotion] = i;
            }
        }
    }

}