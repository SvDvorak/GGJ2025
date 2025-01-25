using UnityEngine;

public class PingToShowPickup : Interactable
{
    public GameObject targetPickup;
    
    public override void Ping()
    {
        targetPickup.SetActive(true);
    }
}