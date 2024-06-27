using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    private GameObject player;

    float cameraMoveSpeed = 5f;
    float height;
    float width;

    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (���� / ����)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;


        height = Camera.main.orthographicSize;//ī�޶��� ���α��� ���ϱ�
        width = height * Screen.width / Screen.height;//ī�޶��� ���α��� ���ϱ�

    }

    private void FixedUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea()
    {

        transform.position = Vector3.Lerp(transform.position,
                                          player.transform.position,
                                          Time.deltaTime * cameraMoveSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx, lx);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly, ly);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
