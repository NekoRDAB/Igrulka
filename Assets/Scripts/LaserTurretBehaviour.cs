using System;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretBehaviour : MonoBehaviour, ITurret
{
    public static int Damage { get; private set; }
    public static float Speed { get; private set; }
    private int level;
    public static GameObject ownShip;
    private PlayerShipBehaviour shipBehaviour;

    private readonly Dictionary<int, string> DescriptionDict = new()
    {
        {0, "������ �����, ���������� ������ ������� � ������������� ������"},
        {1, "�������� �������� ������ ������������� �� 30%"},
        {2, "���� �� ������ ������������� �� 20"},
        {3, "�������� �������� ������ ������������� �� 30%"},
        {4, "���� �� ������ ������������� �� 25"},
        {5, "�������� �������� ������ ������������� �� 40%"},
        {6, "���� �� ������ ������������� �� 30"}
    };

    private readonly Dictionary<int, Action> LevelUpDict = new()
    {
        { 1, () => Speed *= 1.3f},
        { 2, () => Damage += 20 },
        { 3, () => Speed *= 1.3f },
        { 4, () => Damage += 25 },
        { 5, () => Speed *= 1.4f},
        { 6, () => Damage += 30}
    };

    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        Damage = 40;
        Speed = 20f;
    }
    
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0,transform.rotation.eulerAngles.z + Speed * Time.deltaTime);
    }

    public Sprite GetSprite()
    {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public string GetDescription()
    {
        return $"�����. ������� {level}\n{DescriptionDict[level]}";
    }

    public int GetLevel()
    {
        return level;
    }

    public void LevelUp()
    {
        LevelUpDict[level].Invoke();
        level++;
    }

    public void Init()
    {
        Start();
        var turret = Instantiate(gameObject, ownShip.transform.position, ownShip.transform.rotation);
        turret.transform.SetParent(ownShip.transform);
        turret.transform.localPosition = shipBehaviour.positionsList[shipBehaviour.turretsCount];
        shipBehaviour.turretsCount++;
        level++;
    }
}
