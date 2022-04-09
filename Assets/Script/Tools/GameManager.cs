using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    public BaseCharacter player;

    public bool IsPause;

    //public Dictionary<string, BaseSpawner> spawners = new Dictionary<string, BaseSpawner>();

    public GameObject obj;

    

    private void Awake()
    {
        base.Awake();
        player = FindObjectOfType<BaseCharacter>();
        
        
        
    }

    private void Start()
    {
       
        SpawnerMgr.Instance.monsterSpawners[MonsterName.QQR].UpDataSpanwer(1,transform);
        //SpawnerMgr.Instance.monsterSpawners[MonsterName.QQR].Spawner();
        ObjectPool.Instance.GetObject(SpawnerMgr.Instance.monsterSpawners[MonsterName.QQR]);
        
        
        //GameObject obj= ObjectPool.Instance.GetObject(SpawnerMgr.Instance.itemSpawners[1]);
        //GameObject obj = ObjectPool.Instance.GetObject(new WeaponSpawner(JsonMgr.Instance.LoadJson<WeaponData>(ConstPath.CONFIG_WEAPON_DATA, "狼末.json")));


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.data.GetExp(50);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TaskMgr.Instance.ReceiveTask(10001);
            TaskMgr.Instance.Reward(10001);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveGame();

        }   
    }

    private void OnApplicationQuit()
    {
        SaveGame();
        
    }


    public void SaveGame()
    {
        //测试生成存储
        //JsonMgr.Instance.SaveByJson<MonsterData>(ConstPath.CONFIG_MONSTER_DATA,"QQR.json", obj.GetComponent<BaseMonster>().data);

        PlayerMainDataMgr.Instance.OnSave();
        

    }


    public void LoadConfig()
    {

    }


    public void InitSpawners()
    {

    }


 }
