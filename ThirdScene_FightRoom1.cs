using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdScene_FightRoom1 : MonoBehaviour
{
    public GameObject TreasureBox;
    //玩家进入房间即生成门
    public GameObject Door;
    public GameObject Door_Horizontal;
    public Transform Door1;
    public Transform Door2;
    GameObject Door1_real;
    GameObject Door2_real;
    bool isCreated;
    public bool alreadyDestroy;

    //按照波次生成怪物
    public GameObject Gobulin_shoot,Gobulin_spear,WildPig;

    List<Gobulin_shoot> Monster_1 = new List<Gobulin_shoot>();
    List<Gobulin_spear> Monster_2 = new List<Gobulin_spear>();
    List<WildPig> Monster_3 = new List<WildPig>();

    public bool Finished;//用来判断这一波次是否结束
    public int count_time;//用于记录属于第几波次
    public int MonstersNumber;//每个波次生成多少怪物


    //记录这个房间的宽度高度和中心坐标
    public float Height;
    public float Width;
    public float center_x;
    public float center_y;
    //加入木箱的预制体，并且在随机位置制作木箱
    public Transform MovePos;
    public GameObject Box;
    int Box_Block_Number;//生成多少块
    int Box_Type_Number;//生成哪些类型的木箱块
    public LayerMask BoxLayer;//用于防止在同一地方生成木箱的层

    // Start is called before the first frame updates
    void Start()
    {
    //生成木箱
    //获取生成多少块木箱
        Box_Block_Number = Random.Range(10,15);
        
    //生成木箱块
        for(int i = 1;i <= Box_Block_Number;i ++)
        {
            //do
            //{
                //给出木箱生成的位置
                MovePos.localPosition = new Vector3(  Random.Range(center_x - Width , center_x + Width + 1) , Random.Range(center_y - Height , center_y + Height + 1) , 0) ;

                Box_Type_Number = Random.Range(1,4);//生成类型

                if(!Physics2D.OverlapCircle( new Vector2( MovePos.localPosition.x , MovePos.localPosition.y) , 0.5f , BoxLayer) ) CreatetheBox(Box_Type_Number);//如果没有检测到

            //}while(Physics2D.OverlapCircle(new Vector2( MovePos.localPosition.x , MovePos.localPosition.y) , 0.5f , BoxLayer) ) ;
        }

        //进行生成怪物的初始化
        Finished = false;
        count_time = 1;
        for(int i = 0;i < MonstersNumber;i ++)
        {
            Monster_1.Add( Instantiate(Gobulin_shoot,new Vector3(center_x,center_y,0),Quaternion.identity).GetComponent<Gobulin_shoot>() );
            Monster_1[i].width = Width;
            Monster_1[i].height = Height;
            Monster_1[i].theCenterPos = new Vector3(center_x,center_y,0);
        }

        //对门的生成的初始化
        isCreated = false;
        alreadyDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Finished == true)
        {
            count_time ++;
            if(count_time == 2)
            {
                 for(int i = 0;i < MonstersNumber;i ++)
                {
                    Monster_2.Add(Instantiate(Gobulin_spear,new Vector3(center_x,center_y,0),Quaternion.identity).GetComponent<Gobulin_spear>() );
                    Monster_2[i].width = Width;
                    Monster_2[i].height = Height;
                    Monster_2[i].theCenterPos = new Vector3(center_x,center_y,0);
                }
            }

            if(count_time == 3)
            {
                for(int i = 0;i < MonstersNumber;i ++)
                {
                    Monster_3.Add(Instantiate(WildPig,new Vector3(center_x,center_y,0),Quaternion.identity).GetComponent<WildPig>() );
                    Monster_3[i].width = Width;
                    Monster_3[i].height = Height;
                    Monster_3[i].theCenterPos = new Vector3(center_x,center_y,0);
                }
            }

            Finished = false;
        }
        else
        {
            bool is_Finished = true;//用于检验是否完成这一波次
            if(count_time == 1)
            {
                for(int i = 0;i < MonstersNumber;i ++)
                {
                    if(Monster_1[i].health > 0)is_Finished = false;
                }
            }
            if(count_time == 2)
            {
                 for(int i = 0;i < MonstersNumber;i ++)
                {
                    if(Monster_2[i].health > 0)is_Finished = false;
                }
            }
            if(count_time == 3)
            {
                 for(int i = 0;i < MonstersNumber;i ++)
                {
                    if(Monster_3[i].health > 0)is_Finished = false;
                }
                if(is_Finished == true && alreadyDestroy == false)//在第三波之后
                {
                    Destroy(Door1_real);
                    Destroy(Door2_real);
                    MovePos.localPosition = new Vector3(MovePos.localPosition.x - 0.5f,MovePos.localPosition.y,0);
                    Instantiate(TreasureBox,MovePos.localPosition,Quaternion.identity);
                    alreadyDestroy = true;
                }
            }
            if(is_Finished == true)Finished = true;
        }


       
    }


    public void CreatetheBox(int theType)
    {
        if(theType == 1)
        {
            float y = MovePos.localPosition.y;
            for(float x = MovePos.localPosition.x - 0.5f; x <= MovePos.localPosition.x + 0.5f; x += 0.5f)Instantiate(Box , new Vector3(x,y,0) , Quaternion.identity);
        }
        if(theType == 2)
        {
            float y = MovePos.localPosition.y;
            float x = MovePos.localPosition.x;
            Instantiate(Box, MovePos.localPosition , Quaternion.identity);
            y -= 0.5f;
            Instantiate(Box, new Vector3(x , y , 0),Quaternion.identity);
        }
        if(theType == 3)
        {
            
           
            for( float x = MovePos.localPosition.x - 0.5f; x <= MovePos.localPosition.x + 0.5f; x += 0.5f)
            {
                for( float y = MovePos.localPosition.y - 0.5f;y <= MovePos.localPosition.y + 0.5f; y += 0.5f )
                     Instantiate(Box, new Vector3(x , y , 0),Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if( other.CompareTag("Player") && isCreated == false)
        {
            Door1_real = Instantiate(Door,Door1.position,Quaternion.identity);
            Door2_real = Instantiate( Door_Horizontal,Door2.position,Quaternion.identity);
            isCreated = true;
        }
    }
}
