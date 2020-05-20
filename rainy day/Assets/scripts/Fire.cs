using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Fire : MonoBehaviour
{
    private bool isPutout = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isOnFire())
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        var firescale = gameObject.transform.localScale;
        if (isPutout)
        {
            firescale -= new Vector3(0.05f, 0.05f, 0.05f);
            if(Input.GetKey(KeyCode.Z)) 
                GameManager.instance.score++;
        }
        else
            firescale = gameObject.transform.localScale;
        gameObject.transform.localScale = firescale;
        if(firescale.x <=0)
            Destroy(gameObject);
    }        

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rain"))
        {
            isPutout = true;
            print("rain");
        }
        if (other.gameObject.CompareTag("Player"))
        {

            GameManager.instance.isDead = true;
            GameManager.instance.Death();
        }
        
    }

    private bool isOnFire()
    {
        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }
    
}
