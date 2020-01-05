using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    [CreateAssetMenu(menuName = "CLX/Dialogue/Create Image Set")]
    public class DialogueImageSet : ScriptableObject
    {
        public List<DialogueImage> images;
    }
}
