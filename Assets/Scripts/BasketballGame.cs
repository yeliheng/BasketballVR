using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
public class BasketballGame : MonoBehaviour
{
    private Rigidbody rb;//刚体

    //SteamVR_TrackedObject trackedObject;//初始化steamVR控制器

    public Text score;//分数显示栏

    public Text ball ;//剩余球数显示栏

    public int totalBall = 10;//设置初始总球数

    private int scoreCount;//总分统计

    private int distanceScore;//距离得分

    public int basicScore = 1;//基础得分

    private bool ballCircle;//球是否完成落地循环周期
    //重新开始模块prefab
    public GameObject GameOverUI;

    private GameObject restartUI;

    private bool isGameOver;

    VRTK_InteractGrab grabObj;//将要抓取的对象

   // private GameObject grabbedObj;//已抓取的对象

    //初始化游戏信息显示
    private void init()
    {
        score.text = "0";
        totalBall = 10;
        ball.text = totalBall.ToString();
        ballCircle = false;
        isGameOver = false;
        
    }

    private void Start()
    {
        init();
        //trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    //触发器被触发
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter触发！Tag是" + other.tag);
        if (other.CompareTag("trigger") && !isGameOver)
        {
            //Debug.Log("球进了！");
            //距离计算
            Debug.Log(distanceCal(other));
            //总分统计 总分=基础分+距离四舍五入分
            scoreCount += basicScore + (int)Math.Round(distanceCal(other), 0);
            //刷新显示
            score.text = scoreCount.ToString();
        }

        if (isGameOver)
        {
            RestartGame();
        }
        
    }
    
    /*//碰撞事件触发
    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;//确定碰撞目标
        Debug.Log("OnCollisionEnter()被触发 Tag是" + target.tag);
        if (target.CompareTag("restart"))
        {
            RestartGame();
            Destroy(restartUI);
        }
    }*/

    //计算出玩家与碰撞体之间的距离，用于统计分数
    private float distanceCal(Collider other)
    {
        Vector3 v_player = Camera.main.transform.position;
        Vector3 v_collider = other.transform.position;
        float distance = Vector3.Distance(v_player, v_collider);
        return distance;
    }

    private void onGameOver()
    {
        isGameOver = true;
        Debug.Log("游戏结束");
        //生成游戏结束UI
        restartUI = Instantiate(GameOverUI);
        rb = restartUI.GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(0, 2, 0);//让篮球旋转，提升体验
        restartUI.transform.position = new Vector3(-4, 4, 3);
       
    }

    //Update中计算剩余球数，判断游戏是否结束
    private void Update()
    {
        if (isGameOver)
           return;
         
        rb = GetComponent<Rigidbody>();
        if(transform.position.y >= 2.5f && !ballCircle)
        {
            totalBall--;
            ball.text = totalBall.ToString();
            ballCircle = true;
        }
        //Debug.Log("速度: x: " + rb.velocity.x + " y: " + rb.velocity.y + " z: " + rb.velocity.z);
        if (rb.velocity.y > -1.0f && rb.velocity.y < 1.0f && transform.position.y < 2f)
        {
            ballCircle = false;
        }
        //剩余球数为0时掉出游戏结束UI
        if(totalBall == 0)
        {
            Invoke("onGameOver", 3);
        }
       
        
        
        /*if (isGameOver)
        {
            int index = (int)trackedObject.index;
            SteamVR_Controller.Device device = SteamVR_Controller.Input(index);
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("Trigger Up!");
                RestartGame();
            }
        }*/

    }

    //重新开始游戏
    public void RestartGame()
    {
        Debug.Log("游戏将在1秒后重新开始...");
        Destroy(restartUI);
        //延时调用初始化函数
        Invoke("init", 1);
    }

    //ControllerGrabInteractableObject


}
