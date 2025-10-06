using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Clase80/PlayerSettings")]
public class PlayerDataSo : ScriptableObject
{
    public KeyCode keyCodeJump = KeyCode.Space;
    public KeyCode keyCodeLeft = KeyCode.LeftArrow;
    public KeyCode keyCodeRight = KeyCode.RightArrow;
    public Bullet bulletPrefab;
}