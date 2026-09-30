using System.Drawing;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public static PlayerController Main;
    private PlayerAbilityManager _pam;
    private PlayerMovementManager _pmm;
    private PlayerInventoryManager _pim;

    private void Awake()
    {
        Main = this;
    }

    void Start()
    {
        _pam = PlayerAbilityManager.Main;
        _pmm = PlayerMovementManager.Main;
        _pim = PlayerInventoryManager.Main;
    }
    
    public override void ApplyForce(Vector2 force)
    {
        base.ApplyForce(force);
        _pam.ApplyForceToDragged(force);
    }
}
