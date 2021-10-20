using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gobulin_Witch : Enemy
{
   //进行AI巡逻
    public  float width;
    public  float height;
    public  Vector3 theCenterPos;//房间中心的坐标
    public GameObject theNextMovePos_object;
    private Transform theNextMovePos;
    public float startWaitTime;//等待时间的全部
    private float waitTime;//目前还剩多少等待时间
    public float movespeed;//移动速度
    
    //向玩家发射弓箭
    public Transform playerTransform;
    private Vector2 shootDirection_1,shootDirection_2,shootDirection_3,shootDirection_4,shootDirection_5;//射击的方向
    public GameObject Boss_Bullet;//弓箭
    public float cd;//射击的cd
    public float shootWaitTime;//现在已经等待了的时间
    public float radius;//检测半径
    public Transform Muzzle_1;//三个射击方向
    public Transform Muzzle_2;
    public Transform Muzzle_3;
    public Transform Muzzle_4;
    public Transform Muzzle_5;

    //使gobulin死亡之后不再移动
    Vector3 diedPos;

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();//父类的开始
        GameObject a = Instantiate(theNextMovePos_object);
        theNextMovePos = a.transform;
    //对等待时间进行赋值和获得初始的移动坐标
        waitTime = startWaitTime;
        theNextMovePos.position = GetRandomPos();
        transform.position = theNextMovePos.position;
    //设置shoot的waitime
    shootWaitTime = cd;
    playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();//父类的更新

       if(health > 0)//活着就是活着
       {
    //AI巡逻功能
            transform.position = Vector2.MoveTowards(transform.position , theNextMovePos.position , movespeed * Time.deltaTime);//逐渐向目标移动
       
            if(Vector2.Distance(transform.position , theNextMovePos.position ) < 0.1f )//如果怪物接近了目标
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

            //Debug.Log(theNextMovePos.position.x);


     //对玩家进行射击功能,在玩家在一定距离的时候
        float distance = (transform.position - playerTransform.position).sqrMagnitude;
        if(distance < radius)
        {
                if(shootWaitTime >= cd)
            {
                shootDirection_1 = (Muzzle_1.position - transform.position).normalized;
                shootDirection_2 = (Muzzle_2.position - transform.position).normalized;
                shootDirection_3 = (Muzzle_3.position - transform.position).normalized;
                shootDirection_4 = (Muzzle_4.position - transform.position).normalized;
                shootDirection_5 = (Muzzle_5.position - transform.position).normalized;
                float angle_1 = Mathf.Atan2(shootDirection_1.y,shootDirection_1.x) * Mathf.Rad2Deg;//找到玩家与gobulin之间的角度差
                float angle_2 = Mathf.Atan2(shootDirection_2.y,shootDirection_2.x) * Mathf.Rad2Deg;
                float angle_3 = Mathf.Atan2(shootDirection_3.y,shootDirection_3.x) * Mathf.Rad2Deg;
                float angle_4 = Mathf.Atan2(shootDirection_4.y,shootDirection_4.x) * Mathf.Rad2Deg;
                float angle_5 = Mathf.Atan2(shootDirection_5.y,shootDirection_5.x) * Mathf.Rad2Deg;
    
                Instantiate(Boss_Bullet,transform.position,Quaternion.Euler(new Vector3(0,0,angle_1) ));
                Instantiate(Boss_Bullet,transform.position,Quaternion.Euler(new Vector3(0,0,angle_2) ));
                Instantiate(Boss_Bullet,transform.position,Quaternion.Euler(new Vector3(0,0,angle_3) ));
                Instantiate(Boss_Bullet,transform.position,Quaternion.Euler(new Vector3(0,0,angle_4) ));
                Instantiate(Boss_Bullet,transform.position,Quaternion.Euler(new Vector3(0,0,angle_5) ));
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

    Vector3 GetRandomPos()
    {
        Vector3 rndPos;
        float x = Random.Range(theCenterPos.x-width , theCenterPos.x+width+1);
        float y = Random.Range(theCenterPos.y-height , theCenterPos.y+height+1);
        rndPos = new Vector3 (x , y , 0);
        return rndPos;
    }
}
