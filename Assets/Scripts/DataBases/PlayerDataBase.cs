using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/PlayerDatabase", fileName = "PlayerDatabase")]
public class PlayerDataBase : BaseDB<PlayerData>
{

}


[System.Serializable]
public class PlayerData : AliveObject
{
    [Tooltip("Object speed")]
    [SerializeField] private float objectSpeed;
    public float ObjectSpeed
    {
        get { return objectSpeed; }
        protected set { }
    }
}
