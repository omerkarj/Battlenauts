using UnityEngine;
using System.Collections;

public class WeaponStats {

    public string name; 
    public float force;
    public float speed;
    public float fireRate;
    public float damage;
    public int ammo;
}

public class LaserGun : WeaponStats {
    
    public LaserGun() {
        this.name = "Laser Pistol";
        this.force = .12f;
        this.speed = 10f;
        this.fireRate = 0.3f;
        this.damage = 50;
    }
}

public class AlienWeapon : WeaponStats
{

    public AlienWeapon()
    {
        this.name = "Laser Rifle";
        this.force = .3f;
        this.speed = 20f;
        this.fireRate = 0.1f;
        this.damage = 35;
        this.ammo = 50;
    }
}

public class GravityGun : WeaponStats {

    public GravityGun()
    {
        this.name = "Gravity bomb";
        this.force = .3f;
        this.speed = 0.8f;
        this.fireRate = 0.1f;
        this.damage = 0;
        this.ammo = 2;
    }
}

public class DummyGun : WeaponStats
{

    public DummyGun()
    {
        this.name = "Dummy bomb";
        this.force = 1.2f;
        this.speed = 1f;
        this.fireRate = 1.5f;
        this.damage = 100;
        this.ammo = 1;
    }
}