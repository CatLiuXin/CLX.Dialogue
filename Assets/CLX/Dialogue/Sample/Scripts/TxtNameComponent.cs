using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CLX.Dialogue.Sample
{
    public class TxtNameComponent:BaseDialogueChildComponent
    {

        protected override void RegisterControllerEvent(EventDialogueController controller)
        {
            var txt = GetComponent<Text>();
            controller.OnDialogueClipEnterEvent += (clip) =>
            {
                txt.text = clip.roleName;
            };
        }
    }
}