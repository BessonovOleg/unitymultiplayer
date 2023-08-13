using System.Collections.Generic;
using Multiplayer;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private PlayerGun _gun;
    [SerializeField] private float _mouseSensetivity = 2f;
    private MultiplayerManager _multiplayerManager;
    private void Start()
    {
        _multiplayerManager = MultiplayerManager.Instance;
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        bool isShoot = Input.GetMouseButton(0);
        bool space = Input.GetKeyDown(KeyCode.Space);
        bool isSitDown = Input.GetKey(KeyCode.LeftAlt);
        
        _player.SetInput(h,v,mouseX * _mouseSensetivity);
        _player.RotateX(-mouseY * _mouseSensetivity);
        if(space) _player.Jump();

        if (isShoot && _gun.TryShoot(out ShootInfo shootInfo))
        {
            SendShoot(ref shootInfo);
        }

        _player.SitDown(isSitDown);
        SendMove();
    }

    private void SendShoot(ref ShootInfo shootInfo)
    {
        shootInfo.key = _multiplayerManager.GetSessionID();
        string json = JsonUtility.ToJson(shootInfo);
        _multiplayerManager.SendMessage("shoot",json);
    }

    private void SendMove()
    {
        _player.GetMoveInfo(out Vector3 position, out Vector3 velocity,out float rotateX, out float rotateY, out float scaleY);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            {"pX",position.x},
            {"pY",position.y},
            {"pZ",position.z},
            {"vX",velocity.x},
            {"vY",velocity.y},
            {"vZ",velocity.z},
            {"rX",rotateX},
            {"rY",rotateY},
            {"sY",scaleY}
        };
        _multiplayerManager.SendMessage("move",data);
    }
}

[System.Serializable]
public struct ShootInfo
{
    public string key;
    public float px;
    public float py;
    public float pz;
    public float dx;
    public float dy;
    public float dz;
}