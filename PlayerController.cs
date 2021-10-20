using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //玩家的基本属性
    public int health;
    public int shield;//护盾
    public int Mp;
    public int Effect;
    public int gold;

    //进行上下左右八向移动
    [Header("移动")]
    Rigidbody2D rb;
    Vector2 movement;
    public float movespeed;

    //进行开启技能
    public float after_count;//自从使用完技能后过了多长的时间
    public float cd;//技能使用的cd
    public bool incold;//是否在冷却中
    public float used_count;//使用了多久的技能
    public float valid_time;//可以使用多久时间的技能
    public int openEffect;//开启技能需要的能量

    //进行玩家的动画播放
     private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //获取rigid body2d来进行移动
        rb = GetComponent<Rigidbody2D>();

        //开始的时候技能是开启的，即不在冷却中
        incold = false;

        //获取玩家动画组件
        anim = GetComponent<Animator>();

        //给玩家血条蓝条护盾条赋值
        HealthBar.HealthCurrent = health;
        HealthBar.HealthMax = health;
        ShieldBar.ShieldCurrent = shield;
        ShieldBar.ShieldMax = shield;
        EffetBar.EffectCurrent = 0;
        EffetBar.EffectMax = openEffect;
    }

    // Update is called once per frame
    void Update()
    {
    if(health > 0)//活着才能进行以下操作
    {
    //人物进行移动
    //获取移动方向
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //翻转人物,当x有方向的时候就向范围进行更改
        if(movement.x != 0)
        {
            transform.localScale = new Vector3(movement.x * 3,3,3);//记得设置好尺寸
        }
    //对技能开启进行限制和打开
        if(incold == true)
        {
            after_count += Time.deltaTime;//如果技能在冷却就加上这个时候的时间
            if(after_count >= cd)//如果冷却时间过了就允许开启技能，并且将计入了技能使用后的时间归零
            {
                incold = false;
                after_count = 0;
            }
        }

         EffetBar.EffectCurrent = Effect;
        if(Effect >= openEffect)Effect = openEffect;
        if(incold == false && Input.GetMouseButtonDown(1) && Gun.onSkill == false && Effect >= openEffect)//如果技能不在冷却时间并且按下了鼠标右键,并且防止重复开启技能
        {
            Gun.onSkill = true;//开启技能
            Effect -= openEffect;
        }

        if(Gun.onSkill == true)//如果开启了技能就进行计时
        {
            used_count += Time.deltaTime;
            if(used_count >= valid_time)//关闭技能且归零,并且进入cd时间
            {
                Gun.onSkill = false;
                used_count = 0;
                incold = true;
            }
        }
    }

    //如果人物死亡
        if(health <= 0)//死了
        {
           
            anim.SetBool("Die",true);
        }

        //实时赋值给血量蓝量护甲量
        HealthBar.HealthCurrent = health;
        ShieldBar.ShieldCurrent = shield;  

        //实时更改金币数量
        gokd.goldCurrent = gold;
    }

    void FixedUpdate()
    {
        //在fixed update中更新位置保证人物移动的帧率
        if(health > 0)rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
    }

     //void OnTriggerEnter2D(Collider2D other)
   // {
    //    if(other.gameObject.CompareTag("Enemy"))
      //  {
      //      other.GetComponent<Enemy>().TakeDamage(damage);
        //}
    //}


//受到伤害的时候
    public void TakeDamage(int hurtdamage)
    {
        if(shield > 0)
        {
            shield -= hurtdamage;
            if(shield <= 0)shield = 0;
        }
        else 
        {
            health -= hurtdamage;
            if(health <= 0 )health = 0;
        } 
    }


}
