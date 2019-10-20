using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] BoxCollider2D viewBox;

    GameObject player;
    Camera _camera;    
    void Start () {
        player = GameObject.FindWithTag("Player");
        _camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate () {
        FollowPlayer();
	} 
    private void FollowPlayer()
    {
        var min = viewBox.bounds.min;
        var max = viewBox.bounds.max;
        var x = player.transform.position.x;
        var y = player.transform.position.y;


        var cameraHalfX = _camera.orthographicSize * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, min.x + cameraHalfX, max.x - cameraHalfX);
        y = Mathf.Clamp(y, min.y + _camera.orthographicSize, max.y - _camera.orthographicSize);

        gameObject.transform.position = new Vector3(x, y, transform.position.z);
        
    }
}
