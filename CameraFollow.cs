using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //获取玩家位置和平滑因子
    public Transform target;
    public float smoothing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        //如果玩家还存在的话
        if(target != null)
        {
            if(target.position != transform.position)//如果发生了移动
            {
                Vector3 targetPos = target.position;
                transform.position = Vector3.Lerp(transform.position,targetPos,smoothing);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
