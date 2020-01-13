using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue.Sample
{
    public class DialogueAudio : BaseDialogueChildComponent
    {
        protected override void RegisterControllerEvent(EventDialogueController controller)
        {
            var audioSource = GetComponent<AudioSource>();
            controller.RegisterSpecialClipEnterAction(MaskTool.ChangeBGM, clip =>
            {
                audioSource.clip = (AudioClip)clip.GetDialogueObjectByMaskBit(MaskTool.ChangeBGM);
                audioSource.Play();
            });
            controller.OnDialogueEndEvent += (dialogue) =>
            {
                audioSource.Stop();
            };
        }
    }
}