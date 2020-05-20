using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollCam : MonoBehaviour
{
    public float ScrollSpeed = 1f;

    public float timermax = 20f;

    private float timer;

    private int addspeed;

    private int score;
    // Start is called before the first frame update
    void Start()
    {
        ScrollSpeed = 1f;
        timer = timermax;
        score = GameManager.instance.score;
        addspeed = score + 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isRaining == false && GameManager.instance.isCollect== false)
            transform.Translate(Time.deltaTime * ScrollSpeed * Vector2.right);
        else if (GameManager.instance.isRaining || GameManager.instance.isCollect)
            transform.Translate(Vector2.zero);
        score = GameManager.instance.score;
        if (addspeed == score)
        {
            addspeed = score + 100;
            ScrollSpeed += 1f;
        }
    }
}
