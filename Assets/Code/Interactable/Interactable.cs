using System;
using UnityEngine;

public enum InteractionType
{
    CAPSTAN,
    DEPOSIT,
    ITEM
}

public class Interactable : MonoBehaviour
{
    public InteractionType interactionType;
    public bool IsInteracting { get; protected set; }
    public bool interactionFinished;
    public event Action onFinishInteraction;

    public virtual void StartInteraction()
    {
        IsInteracting = true;
    }

    public virtual void StopInteraction()
    {
        IsInteracting = false;
    }

    public void FinishInteraction()
    {
        interactionFinished = true;
        onFinishInteraction?.Invoke();
    }
}
