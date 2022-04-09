using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCharacter :MonoBehaviour
{

    

    public Rigidbody rigidbody;
    public BoxCollider coll;
    public FSM myFsm;


    /// <summary>
    /// 之后用资源控制器导入
    /// </summary>
    public Animator anim;
    

    /// <summary>
    /// 部分应来自设置系统，部分来自数据系统
    /// </summary>
    private Vector3 moveAmount;
    //动态变化 所以暂时先不做存储
    public float speed;
    public float sprintSpeed;
    public float mouseSensitivity = 5f;
    public float Yaw;
    public float yVelocity;
    public float xVelocity;
    public bool IsSprint;
    public bool IsMiss;
    public bool IsAttack;
    
    /// <summary>
    /// 测试用的等级
    /// </summary>
    

    public DamageCalcuation damageCalc;

    public CharacterData data;

    // Start is called before the first frame update

    private void OnEnable()
    {
        //damageCalc = new DamageCalcuation(SO_data);
        //在这之前应该导入武器信息
        
        speed = data.speed*0.1f;
        sprintSpeed = data.sprintSpeed*0.1f;

        
        data.InitData();
        
    }



    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        

        //TODO:等摄像机控制器做好后添加控制


        #region 注册状态
        myFsm = new FSM();
        myFsm.AddState(StateType.Idle, new IdleState(this));
        myFsm.AddState(StateType.Move, new MoveState(this));
        myFsm.AddState(StateType.Sprint, new SprintState(this));
        myFsm.AddState(StateType.Attack, new AttackState(this));
        myFsm.AddState(StateType.Miss, new MissState(this));
        myFsm.AddState(StateType.GetHit, new GetHitState(this));
        #endregion
        myFsm.TransitionState(StateType.Idle);
        
    }

    

    // Update is called once per frame
    protected virtual void Update()
    {
        InputController();
        anim.SetFloat("xVelocity", xVelocity);
        anim.SetFloat("yVelocity", yVelocity);
        anim.SetBool("IsSprint?", IsSprint);

        transform.rotation = Quaternion.Euler(0f, Yaw, 0f);

        myFsm?.OnTrick();
      
    }

    private  void FixedUpdate()
    {
        
        //transform.Translate( xVelocity* speed, 0f, yVelocity* speed);
    }

    private  void InputController()
    {

        xVelocity = Input.GetAxis("Horizontal");
        yVelocity = Input.GetAxis("Vertical");

        IsSprint = Input.GetKey(KeyCode.LeftShift);
        if(!IsAttack&&!IsMiss)
        Yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
    }

    

    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {

            Debug.Log("Attack Successed");
            BaseMonster enemy = other.gameObject.GetComponent<BaseMonster>();

            enemy.fsm.TransitionState(StateType.MGetHit);
            //伤害值传给UI
            int damage = DamageSystem.Instance.GetDamage(this.data, enemy.data, AttributeType.PhysicalA);
            enemy.GetDamage(damage);
        }

        if (other.CompareTag("Item"))
        {
    
            PlayerMainDataMgr.Instance.AddItem(other.GetComponent<BaseItem>());

            ObjectPool.Instance.PushObject(other.gameObject);
        }
    }



    #region 注册成就
    public event Action UpLevel;
    public event Action Test2;
    #endregion

}
