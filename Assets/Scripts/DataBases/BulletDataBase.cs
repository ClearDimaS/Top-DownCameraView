using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/BulletDatabase", fileName = "BulletDatabase")]
public class BulletDataBase : BaseDB<BulletData>
{

}


[System.Serializable]
public class BulletData
{
    [Tooltip("Main Sprite")]
    [SerializeField] private Material mainMaterial;
    public Material MainMaterial
    {
        get { return mainMaterial; }
        set { mainMaterial = value; }
    }

    [Tooltip("Bullet speed")]
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
