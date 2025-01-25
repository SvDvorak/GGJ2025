using StarterAssets;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    public float pushbackStrength = 1;
    public float playerHeadRadius = 0.5f;
    
    void Update()
    {
        var headPosition = PlayerController.Instance.playerHead.position;
        var fromCenter = headPosition - transform.position;
        if(fromCenter.magnitude + playerHeadRadius > transform.localScale.x / 2)
        {
            fromCenter.y = 0;
            PlayerController.Instance.bounceVelocity = -fromCenter * pushbackStrength;
        }
    }
}
