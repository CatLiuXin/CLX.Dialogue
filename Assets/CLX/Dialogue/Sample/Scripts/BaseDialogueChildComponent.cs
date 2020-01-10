using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CLX.Dialogue;

namespace CLX.Dialogue.Sample
{
    public abstract class BaseDialogueChildComponent : MonoBehaviour
    {
        void Awake()
        {
            var controller = transform.parent.GetComponent<EventDialogueController>();
            RegisterControllerEvent(controller);
        }

        protected abstract void RegisterControllerEvent(EventDialogueController controller);
    }
}