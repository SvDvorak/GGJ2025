using UnityEngine;
using UnityEngine.Serialization;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [FormerlySerializedAs("starterAssetsInputs")] [Header("Output")]
        public Inputs inputs;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            inputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            inputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
        }
        
    }

}
