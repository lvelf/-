using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gobulin_shoot : Enemy
{
    //进行掉落
    public GameObject Gold;
    public GameObject Effect;
    bool isDropped;
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
    public Transform playerTransform;//玩家的transform
    private Vector2 shootDirection;//射击的方向
    public GameObject arrow;//弓箭
    public float cd;//射击的cd
    public float shootWaitTime;//现在已经等待了的时间
    public float radius;//检测半径

    //使gobulin死亡之后不再移动
    Vector3 diedPos;

    // Start is called before the first frame update
    public void Start()
    {
        isDropped = false;
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
             if(isDropped == false)
            {
                float a = Random.Range(0,2);
                if(a == 1)
                {   
                    Instantiate(Gold,transform.position,Quaternion.identity);
                }
                else
                {
                    Instantiate(Effect,transform.position,Quaternion.identity);
                }
                isDropped = true;
            }
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
