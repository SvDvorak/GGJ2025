using UnityEngine;
using UnityEngine.Serialization;

public class PingToShowPickup : Interactable
{
    [FormerlySerializedAs("targetPickup")] public GameObject targetExpander;
    public AudioClip pingSound;
    
    public override void Ping()
    {
        PlaySound(pingSound);
        targetExpander.SetActive(true);
    }
}