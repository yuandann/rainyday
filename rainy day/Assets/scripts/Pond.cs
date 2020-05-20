using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour
{
    private bool shrinking = false;
    public GameObject evaporate;
    private float speed;

    public Transform pondsize;
    // Start is called before the first frame update
     void Start()
    {
        speed = GameManager.instance.scrollspeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.canCollect = true;
        }
    }

    private void Update()
    {
        speed = GameManager.instance.scrollspeed;
        if (GameManager.instance.isRaining == false && GameManager.instance.isCollect== false)
            transform.Translate(Time.deltaTime * speed * Vector2.left);
        else if (GameManager.instance.isRaining || GameManager.instance.isCollect)
            transform.Translate(Vector2.zero);
        
        if (GameManager.instance.isCollect)
        {
            shrinking = true;
            evaporate.SetActive(true);
        }
        else
        {
            shrinking = false;
            evaporate.GetComponent<ParticleSystem>().Stop();
        }

        if (shrinking && pondsize.localScale.x > 0 && pondsize.localScale.y > 0)
        {
            pondsize.localScale -= new Vector3(0.01f, 0.01f, 0);
            GameManager.instance.score++;
        }
        else if (pondsize.localScale.x <= 0 || pondsize.localScale.y <= 0)
        {
            shrinking = false;
            GameManager.instance.canCollect = false;
        }

    }
}
