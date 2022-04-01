using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    public BaseCharacter player;

    public bool IsPause;

    //public Dictionary<string, BaseSpawner> spawners = new Dictionary<string, BaseSpawner>();

   



    private void Awake()
    {
        base.Awake();
        player = FindObjectOfType<BaseCharacter>();

        ObjectPool.Instance.GetObject(new WeaponSpawner(WeaponName.狼末));

    }

    private void Start()
    {
        
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

       

        PlayerMainDataMgr.Instance.OnSave();
        

    }


    public void LoadConfig()
    {

    }


    public void InitSpawners()
    {

    }


 }
