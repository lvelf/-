using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gobulin_spear : Enemy
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
    public float waitTime;//目前还剩多少等待时间
    public float movespeed;//移动速度


    //向玩家进行追击
    public Transform playerTransform;//玩家的transform
    public float radius;//检测半径


    //使gobulin死亡之后不再移动
    Vector3 diedPos;

    // Start is called before the first frame update
    void Start()
    {
        isDropped = false;
         base.Start();//父类的开始
        GameObject a = Instantiate(theNextMovePos_object);
        theNextMovePos = a.transform;

    //对等待时间进行赋值和获得初始的移动坐标
        waitTime = startWaitTime;
        theNextMovePos.localPosition = GetRandomPos();
        transform.position = theNextMovePos.position;
    
    playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
         base.Update();//父类的更新

         if(health > 0)//活着就是活着
         {
             float distance = (transform.position - playerTransform.position).sqrMagnitude;//玩家和目标的距离


    //AI巡逻功能,在没有目标的情况下
           if(distance >= radius)
           {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition , theNextMovePos.localPosition , movespeed * Time.deltaTime);//逐渐向目标移动

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
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position,playerTransform.position,movespeed * Time.deltaTime);
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

     public Vector3 GetRandomPos()
    {
        Vector3 rndPos;
        float x = Random.Range(theCenterPos.x-width , theCenterPos.x+width+1);
        float y = Random.Range(theCenterPos.y-height , theCenterPos.y+height+1);
        rndPos = new Vector3 (x , y , 0);
        return rndPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);
            Debug.Log("AlreadyHit");
        }
    }
}
