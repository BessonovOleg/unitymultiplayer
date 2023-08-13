using System;
using UnityEngine;

public class PlayerGun : Gun
{

    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootDelay;
    private float _lastShootTime;
    public bool TryShoot(out ShootInfo info)
    {
        info = new ShootInfo();
        if(Time.time - _lastShootTime < _shootDelay) return false;
        Vector3 position = _bulletPoint.position;
        Vector3 velocity = _bulletPoint.forward * _bulletSpeed;
        _lastShootTime = Time.time;
        Instantiate(_bulletPrefab,position,_bulletPoint.rotation).Init(velocity);
        shoot?.Invoke();
        
        info.px = position.x;
        info.py = position.y;
        info.pz = position.z;
        info.dx = velocity.x;
        info.dy = velocity.y;
        info.dz = velocity.z;
        
        return true;
    }
}
