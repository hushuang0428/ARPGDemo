using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponSpawner :ItemSpawner
{
    public Weapon weapon; 

    
    public WeaponSpawner(WeaponData data)
    {
        weapon = new Weapon();
        weapon.data = data;
        name = data.name.ToString();
    }
    

    
   


    public override GameObject Spawner()
    {
        
        GameObject newWeapon =GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(weapon.data.PrefabPath));

        ///TODO:将config中的值初始化给新武器；
        Weapon newweapom=newWeapon.AddComponent<Weapon>();
        newweapom.data = weapon.data;

        newWeapon.AddComponent<Rigidbody>().useGravity = true;

        return newWeapon;
    }
   
   
}
