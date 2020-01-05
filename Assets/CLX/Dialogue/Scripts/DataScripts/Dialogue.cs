using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    [System.Serializable, CreateAssetMenu(menuName="CLX/Dialogue/Create Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public List<DialogueClip> dialogueClips;
    }
}
