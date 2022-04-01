using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponSpawner :BaseSpawner
{
    public Weapon weapon=new Weapon();

    public WeaponSpawner(WeaponName name)
    {
        weapon.data = JsonMgr.Instance.LoadJson<WeaponData>(ConstPath.CONFIG_WEAPON_DATA,name.ToString()+".json");
        
        this.name = name.ToString();
    }


    public override GameObject Spawner()
    {
        
        GameObject newWeapon =GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(weapon.data.PrefabPath));

        ///TODO:将config中的值初始化给新武器；
        Weapon newweapom=newWeapon.AddComponent<Weapon>();
        newweapom.data = weapon.data;
        return newWeapon;
    }
   
   
}
