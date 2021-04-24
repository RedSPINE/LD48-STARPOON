using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/EnnemyData")]
public class EnnemyData : ScriptableObject
{
    public float speed;
    public float detectionRange = 5;
}
