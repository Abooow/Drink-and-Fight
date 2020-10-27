using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : MonoBehaviour, IWeapon
{
    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public int OrderIndex { get => _orderIndex; set => _orderIndex = value; }
    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public bool CanAttack { get => _canAttack; set => _canAttack = value; }

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _orderIndex;
    [SerializeField] private float _fireRate;
    [SerializeField] private bool _canAttack;


    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
    }

    public void HandleAddWeapon(IWeapon other)
    {
        
    }
}
