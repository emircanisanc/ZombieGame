using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="GunData")]
public class DataGun : ScriptableObject
{
    [SerializeField] private Int ammoSize;
    public int AmmoSize => ammoSize.Value;

    public float reloadTime = 1.5f;

    [SerializeField] private Float fireRate;
    public float FireRate => fireRate.Value;

    [SerializeField] private Int bulletPerShoot;
    public int BulletPerShoot => bulletPerShoot.Value;

    [SerializeField] private Float damage;
    public float Damage => damage.Value;

    [SerializeField] private Float range;
    public float Range => range.Value;
    
    public int maxHitNumber = 1;
    public AudioClip fireSound;
    public LayerMask whatIsHitable;
    public Color fireColor;
}
