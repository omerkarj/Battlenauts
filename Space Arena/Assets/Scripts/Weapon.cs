using UnityEngine;
using System.Collections;

public class WeaponStats {

    public string name; 
    public float force;
    public float damage;
}

public class LaserGun : WeaponStats {
    
    public LaserGun() {
        this.name = "Laser Gun";
        this.force = .5f;
        this.damage = 50;
    }
}

public class AlienWeapon : WeaponStats
{

    public AlienWeapon()
    {
        this.name = "Alien Weapons";
        this.force = .3f;
        this.damage = 35;
    }
}

public class RocketLauncher : WeaponStats
{

    public RocketLauncher()
    {
        this.name = "Rocket Laucher";
        this.force = 1.2f;
        this.damage = 100;
    }
}