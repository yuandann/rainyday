using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBoi : MonoBehaviour
{
    public float fallspeed = 1f;
    public float risespeed = 2f;
    public Transform shadow;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var shadowsize = shadow.localScale;
        var pos = transform.localPosition;
        Vector3 playerpos = new Vector3(pos.x,Mathf.Clamp(pos.y, -9.4f, 12.5f),57.34f);
        transform.localPosition = playerpos;
        
        if (Input.GetAxis("Jump") > 0)
        {
            transform.Translate(Time.deltaTime*risespeed*Vector2.up);
            if (pos.y < 12.5f && pos.y > -9.4f)
                shadowsize.x -= 0.012f;
           
        }
        else 
        {
            transform.Translate(Time.deltaTime*fallspeed*Vector2.down);
            if(pos.y < 12.5f && pos.y > -9.4f)
                shadowsize.x += 0.005f;
           
        }
        shadow.localScale = shadowsize;

//        if (shadowsize.x > 9.5f)
//            shadowsize.x = 9.5f;
//        if (shadowsize.x < 4f)
//            shadowsize.x = 4f;

        if (Input.GetKey(KeyCode.Z))
        {
            //print("rain on");
            GameManager.instance.Raining();
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            //print("rain off");
            GameManager.instance.StopRaining();
        }


        if (Input.GetKey(KeyCode.X) && GameManager.instance.canCollect)
        {
           // print("collecting");
            GameManager.instance.Collect();
        }
        else if (Input.GetKeyUp(KeyCode.X) || !GameManager.instance.canCollect)
        {
            //print("stopped");
            GameManager.instance.StopCollect();
        }
        
    }

//    private void OnCollisionEnter(Collision other)
//    {
//        if (other.gameObject.CompareTag("Bird") || other.gameObject.CompareTag("Obstacle"))
//        {
//            GameManager.instance.Death();
//        }
//
//        if (other.gameObject.CompareTag("Pond"))
//        {
//            GameManager.instance.canCollect = true;
//        }
//    }
//
//    private void OnCollisionExit(Collision other)
//    {
//        if (other.gameObject.CompareTag("Pond"))
//            GameManager.instance.canCollect = false;
//    }
}
