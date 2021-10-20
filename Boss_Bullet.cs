using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
   //弓箭的基础功能
    //让子弹飞行
    public float movespeed;
    public Rigidbody2D rb2D;
    //让子弹飞行一段时间后摧毁
    public float interval;
    public float count;
    //子弹会造成伤害
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
    //让子弹飞行
        rb2D = GetComponent<Rigidbody2D>();//获取刚体组件
        rb2D.velocity = transform.right * movespeed;//给刚体赋值一个Vector3的速度向量
        
    }

    // Update is called once per frame
    void Update()
    {
    //计时，过了一定的时间后摧毁子弹
        count += Time.deltaTime;
        if(count >= interval )Destroy(gameObject);

    }

       void OnTriggerEnter2D(Collider2D other)//如果子弹和别的东西发生了碰撞
    {
    
        if(other.gameObject.CompareTag("Player"))//创造敌人的tag
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        //后面要写一个abstract的Enemy的类然后给所有敌人继承
    }


}
