using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houses : MonoBehaviour
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
        speed = GameManager.instance.scrollspeed;
        if (GameManager.instance.isRaining == false && GameManager.instance.isCollect== false)
            transform.Translate(Time.deltaTime * speed * Vector2.right);
        else if (GameManager.instance.isRaining || GameManager.instance.isCollect)
            transform.Translate(Vector2.zero);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            GameManager.instance.isDead = true;
            GameManager.instance.Death();
        }
    }
}
