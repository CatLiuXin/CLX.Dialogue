using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace CLX.Dialogue.Sample
{
    public class ButChoice : BaseDialogueChildComponent
    {
        protected override void RegisterControllerEvent(EventDialogueController controller)
        {
            var but1 = transform.GetChild(0).GetComponent<Button>();
            var txt1 = transform.GetChild(0).GetComponentInChildren<Text>();
            var but2 = transform.GetChild(1).GetComponent<Button>();
            var txt2 = transform.GetChild(1).GetComponentInChildren<Text>();
            /// 通过闭包来改变nextClip的值
            /// 从而达到通过监听SpecialClipEnter事件改变切换的下一个片段号的作用
            int nextClip1=0;
            int nextClip2=0;

            but1.onClick.AddListener(() =>
            {
                controller.ClipSwitchTo(nextClip1);
            });

            but2.onClick.AddListener(() =>
            {
                controller.ClipSwitchTo(nextClip2);
            });

            controller.RegisterSpecialClipEnterAction(MaskTool.BranchClip, (clip) =>
            {
                gameObject.SetActive(true);
                try
                {
                    var parts = clip.clipContext.Split('*').Where(str => !string.IsNullOrEmpty(str)).Skip(1).ToArray();
                    var branchInfo1 = parts[0].Split(' ');
                    var branchInfo2 = parts[1].Split(' ');
                    nextClip1 = int.Parse(branchInfo1[0]);
                    nextClip2 = int.Parse(branchInfo2[0]);
                    txt1.text = branchInfo1[1];
                    txt2.text = branchInfo2[1];
                }
                catch
                {
                    throw new System.Exception("clip.clipContext{" + clip.clipContext + "}内容与格式不同");
                }
            });

            controller.RegisterSpecialClipEndAction(MaskTool.BranchClip, clip =>
            {
                gameObject.SetActive(false);
            });

            gameObject.SetActive(false);
        }
    }
}