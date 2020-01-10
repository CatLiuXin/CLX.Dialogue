using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue.Sample
{
    /// <summary>
    /// 该组件用于控制 Dialogue 面板的出现与退出
    /// 可以拓展动画功能
    /// </summary>
    public class DialoguePanelComponent : BaseDialogueChildComponent
    {
        protected override void RegisterControllerEvent(EventDialogueController controller)
        {
            controller.OnDialogueEnterEvent += (dialogue) =>
            {
                transform.parent.gameObject.SetActive(true);
            };

            controller.OnDialogueEndEvent += (dialogue) =>
            {
                transform.parent.gameObject.SetActive(false);
            };
        }

        private void Start()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}