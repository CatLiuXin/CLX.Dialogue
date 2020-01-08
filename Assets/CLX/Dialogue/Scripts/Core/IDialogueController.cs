using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    public interface IDialogueController
    {
        void OnDialogueEnter(Dialogue dialogue);
        void OnDialogueClipEnter(DialogueClip clip);
        void OnDialogueClipEnd(DialogueClip clip);
        void OnDialogueEnd(Dialogue dialogue);

    }
}
