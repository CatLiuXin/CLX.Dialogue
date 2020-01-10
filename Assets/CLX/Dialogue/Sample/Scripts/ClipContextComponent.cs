using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CLX.Dialogue.Sample
{
    public class ClipContextComponent : BaseDialogueChildComponent
    {
        protected override void RegisterControllerEvent(EventDialogueController controller)
        {
            /// 用于显示对白片段的内容
            var txt = GetComponent<Text>();
            controller.OnDialogueClipEnterEvent += (clip) =>
            {
                txt.text = clip.clipContext;
            };
            controller.RegisterSpecialClipEnterAction(MaskTool.BranchClip, (clip) =>
            {
                txt.text = clip.clipContext.Split('*')[0];
            });
        }
    }
}