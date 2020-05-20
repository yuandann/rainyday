using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Transform WaterLevel;
    public Transform Camera;
    public GameObject Rain;
    public GameObject Cloudboi;
    public GameObject shadow;
    public Transform justcloud;
    public GameObject Evaporate;
    private GameObject rainclone;
    private GameObject evapclone;
    public GameObject poof;
    public GameObject[] stuff;
    public GameObject Bird;
    public GameObject Fire;
    public GameObject Cloud;
    public TMP_Text ScoreDisplay;
    public GameObject gameover;
    public int score;
    private int addspeed;
    public float timermax = 5f;
    private float timer;
    public float scrollspeed = 1f;
    public bool isRaining = false;
    public bool isCollect = false;
    public bool canCollect = false;
    public bool isDead = false;
    [SerializeField]
    private float[] instantiatetimers = new float[3];
    [SerializeField]
    private float[] Intervals = new float [3];

    // Start is called before the first frame update
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance !=this)
            Destroy(gameObject);
    }
    void Start()
    {
        score = 0;
        ScoreDisplay.text = score.ToString("00000");
        timer = timermax;
        instantiatetimers[0] = 0;
        instantiatetimers[1] = 0;
        instantiatetimers[2] = 0;
        Intervals[0] = 0;
        Intervals[1] = Random.Range(3f,5f);
        Intervals[2] = Random.Range(5f, 8f);
        addspeed = score;
    }

    // Update is called once per frame
    void Update()
    {
        timer--;
        if (timer <= 0 && !isDead)
        {
            timer = timermax;
            score++;
        }
        
        ScoreDisplay.text = score.ToString("00000");

        if (!isDead)
        {
            var waterlevel = WaterLevel.localScale;
            var cloudsize = justcloud.localScale;
            if (isRaining)
            {
                if (waterlevel.y > 0)
                    waterlevel.y -= 0.01f;
                if(cloudsize.x > 0.6f)
                    cloudsize -= new Vector3(0.001f, 0.001f, 0.001f);
            }

            if (isCollect )
            {
                if(waterlevel.y < 8.1f )
                    waterlevel.y += 0.01f;
                if(cloudsize.x < 1.25f)
                 cloudsize += new Vector3(0.001f, 0.001f, 0.001f);
            }
            
            if (!isRaining | !isCollect)
            {
                waterlevel.y -= 0f;
                cloudsize -= Vector3.zero;
            }

            justcloud.localScale = cloudsize;
            WaterLevel.localScale = waterlevel;
            if (waterlevel.y <= 0)
            {
                isDead = true;
                Death();
            }
            print(canCollect);
        }


        //instantiating clouds
        if (Time.time - instantiatetimers[0]>Intervals[0])
        {
            Vector3 cloudpos = new Vector3(Camera.position.x + 45,Random.Range(-18.5f,4.5f),0);
            var cloudclone = Instantiate(Cloud, cloudpos, Quaternion.identity);
            Intervals[0] = Random.Range(1f, 3f);
            instantiatetimers[0] = Time.time + Intervals[0];
        }
        
        //instantiating houses, trees, ponds
        if (Time.time - instantiatetimers[1]>Intervals[1])
        {
            int index = Random.Range(0, stuff.Length);
            GameObject instanobject = stuff[index];
            var prefabpos = instanobject.transform.position;
            Vector3 objectpos = new Vector3(Camera.position.x + 45,prefabpos.y,prefabpos.z);
            Instantiate(instanobject, objectpos, instanobject.transform.rotation);
            Intervals[1] = Random.Range(3f, 5f);
            instantiatetimers[1] = Time.time + Intervals[1];
        }
        
        //instantiating birds
        if (Time.time - instantiatetimers[2]>Intervals[2])
        {
            Vector3 birdpos = new Vector3(Camera.position.x + 45,Random.Range(-25f, -6.3f),Bird.transform.position.z);
            var birdclone = Instantiate(Bird, birdpos, Quaternion.identity);
            Intervals[0] = Random.Range(5f, 8f);
            instantiatetimers[2] = Time.time + Intervals[2];
        }
        
        if (addspeed == score)
        {
            addspeed = score + 100;
            scrollspeed += 1f;
        }
        
    }

    public void Raining()
    {
        if (!isRaining)
        {
            rainclone = Instantiate(Rain, justcloud);
            isRaining = true;
            AudioManager.instance.PlayClip("rain");
        }

    }

    public void StopRaining()
    {
        rainclone.GetComponent<ParticleSystem>().Stop();
        Destroy(rainclone, 1f);
        isRaining = false;
    }

    public void Collect()
    {
        if (canCollect && !isCollect)
        {
            isCollect = true;
            //evapclone = Instantiate(Evaporate, justcloud);
            AudioManager.instance.PlayClip("collect");
        }
        else if (!canCollect)
            StopCollect();
    }

    public void StopCollect()
    {
        canCollect = false;
        isCollect = false;
        //evapclone.GetComponent<ParticleSystem>().Stop();
        //Destroy(evapclone, 3f);
    }

    public void Death()
    {
        if (isDead)
        {
            var cloudpos = Cloudboi.transform.position;
            AudioManager.instance.PlayClip("pop");
            var poofclone = Instantiate(poof,cloudpos,poof.transform.rotation);
            Destroy(poofclone, 7f);
            gameover.SetActive(true);
            Destroy(Cloudboi.gameObject);
            Destroy(shadow);
        }
    }
        
}
