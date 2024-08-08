using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car Selection/Car Data")]
public class CarData : ScriptableObject
{
    public string carName;
    public Sprite carSprite;
}