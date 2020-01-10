using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    [CreateAssetMenu(menuName="CLX/Dialogue/Create Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public List<DialogueClip> dialogueClips;
    }
}
