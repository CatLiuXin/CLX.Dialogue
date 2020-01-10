using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CLX.Dialogue.Sample
{
    public class ButNextComponent : BaseDialogueChildComponent
    {
        protected override void RegisterControllerEvent(EventDialogueController controller)
        {
            var btn = GetComponent<Button>();
            var isEnd = false;

            controller.RegisterSpecialClipEnterAction(MaskTool.BranchClip,
                (clip) =>
                {
                    btn.interactable = false;
                });

            controller.RegisterSpecialClipEndAction(MaskTool.BranchClip,
                (clip) =>
                {
                    btn.interactable = true;
                });

            controller.RegisterSpecialClipEnterAction(MaskTool.DialogueEndClip, (clip) =>
            {
                isEnd = true;
            });

            controller.RegisterSpecialClipEndAction(MaskTool.DialogueEndClip, (clip) =>
            {
                isEnd = false;
            });

            btn.onClick.AddListener(() =>
            {
                if (isEnd) controller.ClipSwitchTo(-1);
                else controller.ShowNextClip();
            });
        }
    }
}