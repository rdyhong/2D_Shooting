using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayType
{
    PC,
    Mobile
}

public enum CharacterState
{
    Idle,
    Walk,
    Run,
    
    Searching,
}

public enum GunState
{ 
    Ready,
    Reloading,
}

public enum ObjectMaterial
{
    Human,
    Wood,
    Metal,

}

public enum GunType
{ 
    SMG = 0,
    ShotGun,
    AssultRifle,
    DMR,
    SniperRifle,

    Max
}

public enum AmmoType
{
    Light = 0,
    Middle,
    Heavy,

    Max
}

public enum MagType
{
    Small = 0,
    Middle,
    Big,

    max
}
