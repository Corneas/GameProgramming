using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraHandler : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float zoomAmount = 2f;
    private float minOrthorgraphicSize = 10f;
    private float maxOrthorgraphicSize = 30f;
    private float orthographicSize;
    private float targetOrthographicSize;
    private float zoomSpeed = 5f;

    private void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    private void Update()
    {
        Move();
        HandleMovement();
    }

    private void Move()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = mousePos;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(h, v).normalized;

        transform.Translate(moveDir * Time.deltaTime * speed);
    }


    private void HandleMovement()
    {
        zoomAmount = 2f;
        targetOrthographicSize -= Input.mouseScrollDelta.y * zoomAmount;

        minOrthorgraphicSize = 10;
        maxOrthorgraphicSize = 30;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthorgraphicSize, maxOrthorgraphicSize);

        zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }
}
