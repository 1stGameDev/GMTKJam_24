using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform playerTransform;

    [SerializeField]
    private float CameraVerticalHeightOffset = 5.0f;
    [SerializeField]
    private float CameraHeightCatchupLerp = 0.1f;

    private float CurrentPlayerMinimumHeight = float.MaxValue;
    private bool JustLanded = false;

    public void OnLanded()
    {
        JustLanded = true;
        CurrentPlayerMinimumHeight = playerTransform.position.y + CameraVerticalHeightOffset;
    }

    private void Start()
    {
        CurrentPlayerMinimumHeight = playerTransform.position.y;
        transform.position = new Vector3(playerTransform.position.x, CurrentPlayerMinimumHeight, -10);
    }

    // Update is called once per frame
    private void Update()
    {
        float playerHeight = playerTransform.position.y + CameraVerticalHeightOffset;
        if (playerHeight <= CurrentPlayerMinimumHeight)
        {
            CurrentPlayerMinimumHeight = playerHeight;
        }

        float newHeight = CurrentPlayerMinimumHeight;
        if (JustLanded && Mathf.Abs(transform.position.y - CurrentPlayerMinimumHeight) > 0.01f)
        {
            newHeight = Mathf.Lerp(transform.position.y, CurrentPlayerMinimumHeight, CameraHeightCatchupLerp);
        }
        else
        {
            JustLanded = false;
        }

        gameObject.transform.position = new Vector3(playerTransform.position.x, newHeight, -10);
    }
}
