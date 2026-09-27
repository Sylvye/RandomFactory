using System.Drawing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Main;
    private PlayerAbilityManager _pam;
    private PlayerMovementManager _pmm;
    private Rigidbody2D _rb;
    private Collider2D _col;

    void Awake()
    {
        Main = this;
        _pam = GetComponent<PlayerAbilityManager>();
        _pmm = GetComponent<PlayerMovementManager>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    public Rigidbody2D GetRB()
    {
        return _rb;
    }

    public Collider2D GetCol()
    {
        return _col;
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
