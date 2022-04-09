using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerMgr :SingletonMono<SpawnerMgr>
{
    public Dictionary<CharactersName, CharacterSpawner> characterSpawners = new Dictionary<CharactersName, CharacterSpawner>();

    public Dictionary<MonsterName, MonsterSpawner> monsterSpawners = new Dictionary<MonsterName, MonsterSpawner>();

    public Dictionary<int, ItemSpawner> itemSpawners = new Dictionary<int, ItemSpawner>();

    public void Awake()
    {
        base.Awake();
        OnLoad();
    }

    

    /// <summary>
    /// 从config中读取初始配置数据
    /// </summary>
    public void OnLoad()
    {
        
 
        foreach(CharactersName characterName in Enum.GetValues(typeof(CharactersName))){
          
            CharacterData data= JsonMgr.Instance.LoadJson<CharacterData>(ConstPath.CHARACTER_DATA, characterName.ToString() + ".json");
            
            characterSpawners.Add(characterName,new CharacterSpawner(Transform.FindObjectOfType<PlayerMainDataMgr>().transform,data));
           
        }

        foreach(MonsterName monsterName in Enum.GetValues(typeof(MonsterName)))
        {
            MonsterData data = JsonMgr.Instance.LoadJson<MonsterData>(ConstPath.CONFIG_MONSTER_DATA,monsterName.ToString()+".json");
            monsterSpawners.Add(monsterName, new MonsterSpawner(data));
        }


        WeaponData wdata = JsonMgr.Instance.LoadJson<WeaponData>(ConstPath.CONFIG_WEAPON_DATA, "狼末.json");
        itemSpawners.Add(1, new WeaponSpawner(wdata));

        //GameObject obj = itemSpawners[1].Spawner();

        /*
        foreach(WeaponName weaponName in Enum.GetValues(typeof(WeaponName))){
            WeaponData data= JsonMgr.Instance.LoadJson<WeaponData>(ConstPath.CONFIG_WEAPON_DATA, weaponName.ToString()+".json");
            weaponSpanwers.Add(weaponName, new WeaponSpawner(data));
        }
        
        Debug.Log(weaponSpanwers[WeaponName.狼末]==null);

        */
        RewardMgr.Instance.OnInit();

       


    }




}
