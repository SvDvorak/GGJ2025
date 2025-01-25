using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Touch() { }
    public virtual void Ping() { }
}