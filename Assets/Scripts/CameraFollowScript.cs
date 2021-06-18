using System;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform target;
    public Camera follower;
    
    public float minZoom = 5;
    public float maxZoom = 15;
    public float zoom;

    void Start()
    {
        zoom = (minZoom + maxZoom) / 2;
    }
    
    void Update()
    {
        float wheelDelta = -Input.mouseScrollDelta.y;

        zoom = Math.Max(Math.Min(zoom + wheelDelta, maxZoom), minZoom);
        follower.orthographicSize = zoom;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(
            target.position.x + follower.orthographicSize * 1/1.5f,
            target.position.y,
            -10);
    }
}
