using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class OutAutoGrab : MonoBehaviour
{
    private Vector3 pos;
    // private VRTK_InteractGrab autoGrab;
    // private Camera currentCamera;
    //private GameObject basketball;
    private Rigidbody rb;
   // bool isForceNeed = false;
    void Update()
    {
        if(transform.position.y <= -1)
        {
            //isForceNeed = true;
            Debug.Log("球出界了");
            //Destroy(gameObject);
            rb = GetComponent<Rigidbody>();

            //给刚体设置一个速度 确保物体落地后不再受弹力影像
            rb.velocity = new Vector3(0, 0, 0);
            pos = Camera.main.transform.position;
            pos.y = 0;

            //将物体移动到摄影机下方，方便抓取
            transform.position = pos;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
