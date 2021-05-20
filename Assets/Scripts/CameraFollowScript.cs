using System;
using UnityEditor.UI;
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
        var prev_x = transform.position.x;
        var prev_y = transform.position.y;
        
        transform.position = new Vector3(
            (-prev_x + target.position.x) / 7 + prev_x,
            (-prev_y + target.position.y) / 7 + prev_y,
            -10);
    }
}
