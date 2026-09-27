using System.Drawing;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public static PlayerController Main;
    private PlayerAbilityManager _pam;
    private PlayerMovementManager _pmm;

    private void Awake()
    {
        Main = this;
        _pam = GetComponent<PlayerAbilityManager>();
        _pmm = GetComponent<PlayerMovementManager>();
    }
    
    public override void ApplyForce(Vector2 force)
    {
        base.ApplyForce(force);
        _pam.ApplyForceToDragged(force);
    }

    public PlayerAbilityManager GetPAM()
    {
        return _pam;
    }

    public PlayerMovementManager GetPMM()
    {
        return _pmm;
    }
}
