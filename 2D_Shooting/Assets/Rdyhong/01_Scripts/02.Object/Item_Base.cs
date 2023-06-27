using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemFieldState
{
    Ground = 0,
    Player,
}

public class Item_Base : MonoBehaviour
{
    public int idx = 0;
    public bool ownerExist = false;

    public ItemFieldState fieldState = ItemFieldState.Ground;

    protected virtual void Awake()
    {
    }

    public virtual void Equip(CharacterController _c, Transform _parent)
    {
        ownerExist = true;

        fieldState = ItemFieldState.Player;

        transform.SetParent(_parent);
    }

    public virtual void Drop(Vector3 _dropDir)
    {
        ownerExist = false;

        fieldState = ItemFieldState.Ground;

        transform.SetParent(null);
    }

    public virtual void Use()
    {

    }
}
