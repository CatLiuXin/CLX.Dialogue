using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CLX.Dialogue.Sample
{
    public class DialogueBackground : BaseDialogueChildComponent
    {
        protected override void RegisterControllerEvent(EventDialogueController controller)
        {
            var image = GetComponent<Image>();
            controller.RegisterSpecialClipEnterAction(MaskTool.ChangeBackground,clip =>
            {
                gameObject.SetActive(true);
                image.sprite = (Sprite)clip.GetDialogueObjectByMaskBit(MaskTool.ChangeBackground);
            });
            gameObject.SetActive(false);
        }
    }
}