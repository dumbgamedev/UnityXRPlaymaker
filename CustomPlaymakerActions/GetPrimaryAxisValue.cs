// Custom Action by DumbGameDev
// www.dumbgamedev.com

//using UnityEngine;

using HutongGames.PlayMaker;

namespace DGD
{
    [ActionCategory("Unity XR Input")]
    [Tooltip("Get Primary axis value between -1 to 1")]
    public class GetPrimaryAxisValue : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(XRControllerInput))]
        [Tooltip("Gameobject with the XR Controller Input script")]
        public FsmOwnerDefault inputGameObject;

        [ActionSection("Output")]
        [Tooltip("Primary axis value between -1 to 1 as a vector 2")]
        [UIHint(UIHint.Variable)]
        public FsmVector2 axisValue;

        [Tooltip("X axis value between -1 to 1")]
        [UIHint(UIHint.Variable)]
        public FsmFloat xValue;

        [Tooltip("Y axis value between -1 to 1")]
        [UIHint(UIHint.Variable)]
        public FsmFloat yValue;

        [ActionSection("Options")]
        public FsmBool everyFrame;

        XRControllerInput input;

        public override void Reset()
        {
            axisValue = null;
            inputGameObject = null;
            everyFrame = false;
            yValue = null;
            xValue = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(inputGameObject);
            input = go.GetComponent<XRControllerInput>();
            if (go == null || input == null)
            {
                return;
            }

            GetValue();

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                GetValue();
            }
        }

        void GetValue()
        {
            var go = Fsm.GetOwnerDefaultTarget(inputGameObject);
            if (go == null || input == null)
            {
                return;
            }

            axisValue.Value = input.primary2DAxisValue;
            xValue.Value = input.primary2DAxisValue.x;
            yValue.Value = input.primary2DAxisValue.y;
        }
    }
}