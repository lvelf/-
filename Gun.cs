using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    //实现子弹从枪口发射
    public Transform muzzleTransform;//不开启技能时候的枪口的位置,即子弹生成的位置
    public GameObject Start_bullet;//子弹的预制体
    public static bool onSkill;//全局可以访问的变量，通过修改来判断是否开启技能
    public Transform muzzleTransform_first;//开启技能的时候双枪道
    public Transform muzzleTransform_second;

    //实现枪口跟着鼠标旋转
    public Camera cam;
    private Vector3 mousePos;
    private Vector2 gunDirection;
    // Start is called before the first frame update
    void Start()
    {
        onSkill = false;//技能一开始是关闭的
    }

    // Update is called once per frame
    void Update()
    {
    //枪口随着鼠标旋转功能
        //获取鼠标位置
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);//这里有bug，之后更改
        //得到枪口应该指向的方向
        gunDirection = (mousePos - transform.position).normalized;
        //找到这个angle，算出弧度然后算出角度
        float angle = Mathf.Atan2(gunDirection.y,gunDirection.x) * Mathf.Rad2Deg;
        //将这个角度赋值给gun
        transform.eulerAngles = new Vector3(0,0,angle);


    //发射子弹的功能
        if(Input.GetMouseButtonDown(0))//按下左键发射子弹
        {
            if(!onSkill)
            {
                Instantiate(Start_bullet , muzzleTransform.position , Quaternion.Euler(transform.eulerAngles) );//在枪口位置生成一颗子弹并且让其方向是刚刚算出来的角度的方向
            }
            else//如果开启了技能
            {
                Instantiate(Start_bullet , muzzleTransform_first.position , Quaternion.Euler(transform.eulerAngles) );//在双枪道上面生成子弹
                Instantiate(Start_bullet , muzzleTransform_second.position , Quaternion.Euler(transform.eulerAngles) );
            }
        }
    }
    
}
