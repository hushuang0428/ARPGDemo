using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;


public enum CharactersName
{
    Ghost,Gobin
}

/// <summary>
///  对接各种数据
/// </summary>


public class PlayerMainDataMgr : SingletonMono<PlayerMainDataMgr>
{
    
    /// <summary>
    /// 读取已用角色 还未完成 不是new
    /// </summary>
    [SerializeField]public List<CharacterData> characters = new List<CharacterData>();
    private Dictionary<CharactersName, CharacterSpawner> spawners = new Dictionary<CharactersName, CharacterSpawner>();
    [SerializeField]public List<CharactersName> mycharacters = new List<CharactersName>();


    [SerializeField]public List<WeaponData> weaponBag = new List<WeaponData>();
    private Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();

    
    

    

    MyTransform mytransform;

    public  GameObject activeCharacter;

    private void Awake()
    {
        base.Awake();

        OnLoad();
        

        
        //初始化所有生成器
        foreach (var character in characters)
        {

            GameObject newCharacter = new CharacterSpawner(this.transform, character).Spawner();

            newCharacter.name=newCharacter.name.Replace("(Clone)", string.Empty);

            newCharacter.SetActive(false);

            
            /*
            if (!spawners.ContainsKey(character.name))
            {
                
                spawners.Add(character.name, new CharacterSpawner(transform, character));
            }
           */
        }

        /*
        if(mytransform.active!=null) 
            activeCharacter = ObjectPool.Instance.GetObject(spawners[mytransform.active],"Player");
        else 
            activeCharacter = ObjectPool.Instance.GetObject(spawners[CharactersName.Ghost], "Player");
        
        */
        
        if (mytransform.active != null)
            activeCharacter = transform.Find(mytransform.active.ToString()).gameObject;
        else
            activeCharacter = transform.Find(CharactersName.Ghost.ToString()).gameObject;
        
        activeCharacter.SetActive(true);
        activeCharacter.transform.position = mytransform.postion;
        activeCharacter.GetComponent<Shoulder>().Yaw = mytransform.Yaw;


       

    }

    private void Start()
    {
        CameraMgr.Instance.SetLookAndFllow(activeCharacter.transform.Find("LookAt"));
        
    }

    [Obsolete]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            ChangeCharacter(CharactersName.Gobin.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCharacter(CharactersName.Ghost.ToString());
        }

                
    }

    /// <summary>
    /// 静态加载试验方法 我的评价是有点拉
    /// </summary>
    [Obsolete]
    public void ChangeCharacter(string name)
    {
        GameObject newCharacter= transform.Find(name).gameObject;

        newCharacter.transform.position = activeCharacter.transform.position;
        newCharacter.GetComponent<BaseCharacter>().Yaw= activeCharacter.GetComponent<BaseCharacter>().Yaw;

        activeCharacter.SetActive(false);
        activeCharacter = newCharacter;
        activeCharacter.SetActive(true);

        GameManager.Instance.player = activeCharacter.GetComponent<BaseCharacter>();
        CameraMgr.Instance.SetLookAndFllow(activeCharacter.transform.Find("LookAt"));
        #region
        /*
        GameObject obj = ObjectPool.Instance.GetObject(spawner,"Player");

        obj.transform.position = activeCharacter.transform.position;

        //Quaternion rotation = activeCharacter.transform.rotation;

        
        obj.GetComponent<BaseCharacter>().Yaw = activeCharacter.GetComponent<BaseCharacter>().Yaw;

        //obj.transform.Rotate(rotation.x, rotation.y, rotation.z);

        ObjectPool.Instance.PushObject(activeCharacter);

        activeCharacter = obj;

        CameraMgr.Instance.SetLookAndFllow(activeCharacter.transform.Find("LookAt"));

        activeCharacter.GetComponent<BaseCharacter>().enabled = true;

        GameManager.Instance.player = activeCharacter.GetComponent<BaseCharacter>();

        //activeCharacter.SetActive(true);
        */
        #endregion
    }


    public void AddItem(BaseItem item)
    {
        Debug.Log(weaponBag.Count);
        item.ID = weaponBag.Count;

        if (item.type == ItemType.Weapon)
        {
            (item as Weapon).data.ID = item.ID;
            weaponBag.Add((item as Weapon).data);
        }
       
    }


    public void OnLoad()
    {        
        
        //加载已存在的角色
        mycharacters=JsonMgr.Instance.LoadJson<Serialization<CharactersName>>(ConstPath.CHARACTER_DATA, "myCharacter.json").ToList();

        //weaponBag.Add(JsonMgr.Instance.LoadJson<WeaponData>(ConstPath.PLAYER_BAG_DATA+"/Weapon" , "0.json"));
        

        foreach (var character in mycharacters)
        {
            
            characters.Add(JsonMgr.Instance.LoadJson<CharacterData>(ConstPath.CHARACTER_DATA, character.ToString() + ".json"));
        }

        //加载位置信息
        mytransform = JsonMgr.Instance.LoadJson<MyTransform>(ConstPath.GAME_DATA, "PauseTransform.json");
        weaponBag = JsonMgr.Instance.LoadJson<Serialization<WeaponData>>(ConstPath.PLAYER_BAG_DATA + "/Weapon", "Weapon.json").ToList();
    }


    public void OnSave()
    {
        BaseCharacter player = activeCharacter.GetComponent<BaseCharacter>();
        JsonMgr.Instance.SaveByJson<MyTransform>(ConstPath.GAME_DATA, "PauseTransform.json", new MyTransform(player.data.name, player.transform.position, player.Yaw));



        foreach (var character in characters)
        {
            JsonMgr.Instance.SaveByJson<CharacterData>(ConstPath.CHARACTER_DATA, character.name.ToString() + ".json", character);

        }

        /*
        foreach (var weapon in weaponBag)
        {
            JsonMgr.Instance.SaveByJson<WeaponData>(ConstPath.PLAYER_BAG_DATA+"/Weapon", weapon.ID + ".json", weapon );
        }*/


        JsonMgr.Instance.SaveByJson<Serialization<WeaponData>>(ConstPath.PLAYER_BAG_DATA+"/Weapon","Weapon.json",new Serialization<WeaponData>(weaponBag));

        JsonMgr.Instance.SaveByJson<Serialization<CharactersName>>(ConstPath.CHARACTER_DATA, "myCharacter.json",new Serialization<CharactersName>(mycharacters));
    }

    

}




