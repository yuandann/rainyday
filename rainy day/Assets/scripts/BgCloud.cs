using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgCloud : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.instance.scrollspeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X))
            speed = GameManager.instance.scrollspeed;
        else 
            speed = GameManager.instance.scrollspeed + 1.5f;
        transform.Translate(Time.deltaTime * speed * Vector2.left);
    }
}
