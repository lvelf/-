using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //怪物的基本信息
    public int damage;
    public int health;
    //获取一下哥布林动画
    private Animator anim;
    // Start is called before the first frame update
    public void Start()
    {
        //获取动画
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(health <= 0)//死了
        {
            anim.SetBool("Die",true);
        }
    }

    public void TakeDamage(int hurtdamage)
    {
        health -= hurtdamage;
    }
}
