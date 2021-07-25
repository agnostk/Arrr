using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capstan : Interactable
{
    private Quaternion initialRotation;
    public float rotationSpeed = 60f;
    public float targetRotation = 360f;
    [SerializeField] private float currentRotation = 0f;

    public Transform firstHandle;
    public Transform secondHandle;
    public Transform thirdHandle;
    public Transform fourthHandle;

    private bool firstHandleInUse;
    private bool secondHandleInUse;
    private bool thirdHandleInUse;
    private bool fourthHandleInUse;

    void Awake()
    {
        onFinishInteraction += () => GameManager.Instance.StartGame();
    }

    void Start()
    {
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        if (interactionFinished) return;

        if (IsInteracting && currentRotation <= targetRotation)
        {
            currentRotation += rotationSpeed * Time.deltaTime;
        }
        else if (!IsInteracting && currentRotation >= 0f)
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
        }

        if (currentRotation >= targetRotation)
        {
            FinishInteraction();
            transform.localRotation = Quaternion.Euler(0f, targetRotation, 0f);
        }
        else if (currentRotation >= 0f)
        {
            transform.localRotation = Quaternion.Euler(0f, currentRotation, 0f);
        }
        else
        {
            transform.localRotation = initialRotation;
        }
    }

    public Transform GetClosestHandle(Vector3 position)
    {
        float currentDistance = 0f;
        float minDistance = Mathf.Infinity;
        Transform closestHandle = null;

        currentDistance = Vector3.Distance(position, firstHandle.position);
        if (currentDistance <= minDistance)
        {
            minDistance = currentDistance;
            closestHandle = firstHandle;
        }

        currentDistance = Vector3.Distance(position, secondHandle.position);
        if (currentDistance <= minDistance)
        {
            minDistance = currentDistance;
            closestHandle = secondHandle;
        }

        currentDistance = Vector3.Distance(position, thirdHandle.position);
        if (currentDistance <= minDistance)
        {
            minDistance = currentDistance;
            closestHandle = thirdHandle;
        }

        currentDistance = Vector3.Distance(position, fourthHandle.position);
        if (currentDistance <= minDistance)
        {
            minDistance = currentDistance;
            closestHandle = fourthHandle;
        }

        return closestHandle;
    }

}
