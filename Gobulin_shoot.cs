using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gobulin_shoot : Enemy
{
    //进行AI巡逻
    public  float width;
    public  float height;
    public  Vector3 theCenterPos;//房间中心的坐标
    public Transform theNextMovePos;
    public float startWaitTime;//等待时间的全部
    public float waitTime;//目前还剩多少等待时间
    public float movespeed;//移动速度
    
    //向玩家发射弓箭
    public Transform playerTransform;//玩家的transform
    private Vector2 shootDirection;//射击的方向
    public GameObject arrow;//弓箭
    public float cd;//射击的cd
    public float shootWaitTime;//现在已经等待了的时间
    public float radius;//检测半径

    //使gobulin死亡之后不再移动
    Vector3 diedPos;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();//父类的开始

    //对等待时间进行赋值和获得初始的移动坐标
        waitTime = startWaitTime;
        theNextMovePos.localPosition = GetRandomPos();
    //设置shoot的waitime
        shootWaitTime = cd;
    playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();//父类的更新

    }

    void FixedUpdate()
    {
         if(health > 0)//活着就是活着
       {
    //AI巡逻功能
           transform.localPosition = Vector2.MoveTowards(transform.localPosition , theNextMovePos.localPosition , movespeed * Time.fixedDeltaTime);//逐渐向目标移动

            if(Vector2.Distance(transform.localPosition , theNextMovePos.localPosition ) < 0.05f )//如果怪物接近了目标
            {
                if(waitTime <= 0 )//已经没有需要在那里等待的时间了
                {
                    theNextMovePos.position = GetRandomPos();
                    waitTime = startWaitTime;
                }
                else 
                {
                    waitTime -= Time.deltaTime;
                }
            }


     //对玩家进行射击功能,在玩家在一定距离的时候
        float distance = (transform.position - playerTransform.position).sqrMagnitude;
        if(distance < radius)
        {
                if(shootWaitTime >= cd)
            {
                shootDirection = (playerTransform.position - transform.position).normalized;
                float angle = Mathf.Atan2(shootDirection.y,shootDirection.x) * Mathf.Rad2Deg;//找到玩家与gobulin之间的角度差
    
                Instantiate(arrow,transform.position,Quaternion.Euler(new Vector3(0,0,angle) ));
                shootWaitTime = 0;
            }
            else 
            {
                shootWaitTime += Time.deltaTime;
            }
        }

        diedPos = transform.position;

       }
        else
        {
            transform.position = diedPos;
        }
    }

    public Vector3 GetRandomPos()
    {
        Vector3 rndPos;
        float x = Random.Range(theCenterPos.x-width , theCenterPos.x+width+1);
        float y = Random.Range(theCenterPos.y-height , theCenterPos.y+height+1);
        rndPos = new Vector3 (x , y , 0);
        return rndPos;
    }

}
