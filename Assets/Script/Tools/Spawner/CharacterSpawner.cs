using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner:BaseSpawner
{
    // Start is called before the first frame update


    private Transform parent;
    private CharacterData data;

    public CharacterSpawner(Transform parent,CharacterData character)
    {
        this.parent = parent;
        this.data = character;
        
        name = character.name.ToString();
        
        
    }


    public override GameObject Spawner()
    {
       
        GameObject NewCharacter = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(data.PrefabPath));

       
        BaseCharacter characterScript = NewCharacter.GetComponent<BaseCharacter>();

        characterScript.data = this.data;



        GameObject LookAt = new GameObject("LookAt");
        LookAt.transform.SetParent(NewCharacter.transform);
        LookAt.transform.localPosition = new Vector3(0f, 1f, 0f);

        NewCharacter.transform.SetParent(parent);
        NewCharacter.layer = LayerMask.NameToLayer("Player");

        return NewCharacter;
    }

    
}
