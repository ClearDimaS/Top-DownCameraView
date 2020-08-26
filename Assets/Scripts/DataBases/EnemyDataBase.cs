using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/EnemyDatabase", fileName = "EnemyDatabase")]
public class EnemyDataBase : BaseDB<EnemyData>
{

}

[System.Serializable]
public class EnemyData : AliveObject
{
    
}
