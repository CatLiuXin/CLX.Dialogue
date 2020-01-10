using UnityEngine;
using UnityEngine.UI;

namespace CLX.Dialogue.Sample {
    public class RoleImageComponent : BaseDialogueChildComponent
    {
        protected override void RegisterControllerEvent(EventDialogueController controller)
        {
            var image = GetComponent<Image>();
            controller.OnDialogueClipEnterEvent += (clip) =>
            {
                /// 展示角色图片并按照图片的比例调整Scale
                var sprite = DialogueMgr.Instance.GetSpriteByRoleEmotion(clip.roleName, clip.roleEmotion);
                var scale = sprite.rect.height / sprite.rect.width;
                image.sprite = sprite;
                image.transform.localScale = new Vector3(1, scale, 1);
            };
        }
    }
}