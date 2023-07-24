using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "PlayerData/CreatePlayerData")]
public class PlayerScriptable : ScriptableObject
{
    public string playerName = "";
    public float moveSpeed;
    public float jumpForce;
    public float health;
    public float damage;
    public Sprite avatar;

    public PlayerScriptable(string playerName, float moveSpeed, float jumpForce, float health, float damage, Sprite avatar)
    {
        this.playerName = playerName;
        this.moveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
        this.health = health;
        this.damage = damage;
        this.avatar = avatar;
    }
    public void ShowInfomation()
    {
        Debug.Log(playerName);
        Debug.Log(moveSpeed);
        Debug.Log(jumpForce);
        Debug.Log(health);
        Debug.Log(damage);
    }
}
