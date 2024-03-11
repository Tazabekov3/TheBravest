using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private float smoothness;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position;
        smoothness = player.GetComponent<PlayerController>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + new Vector3(0, 0, -5);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness);
            transform.position = smoothedPosition;
        }
    }
}
