﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class UnitController : MonoBehaviour {

    [SerializeField] private MapData mapdata;
    [SerializeField] private CharacterParameter chara;
    [SerializeField] private GameObject playerManager;
    
    /// <summary>
    /// キャラクターの管理
    /// </summary>
    public const int playerCount = 0;
    public List<PlayerController> playerController = new List<PlayerController>();
    public List<EnemyController> enemyController = new List<EnemyController>();
    public List<PlayerSet> entryPlayer = new List<PlayerSet>();
    public List<EnemySet> entryEnemy = new List<EnemySet>();
    public GameObject[] playerObj;
    public GameObject[] enemyObj;
    public int selectUnit = 99;
    public int selectEnemy = 99;
    public bool isUnit = false;
    public int stayCount = 0;

    public string save;

    public class MapUnit
    {
        // public int height = 1; // 高さ
        // public int blockNum = 0; // ブロックの種類
        public bool movable = false; // 移動可能フラグ 
    }

    public MapUnit[,] mapUnit = new MapUnit[20, 20];

    // Use this for initialization
    void Start () {
        for (int i = 0;i < mapdata.entryPlayer.Count; i++)
        {
            Instantiate(Resources.Load("PlayerObj"));
        }
        playerObj = GameObject.FindGameObjectsWithTag("Player");
        enemyObj = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0;i < mapdata.entryPlayer.Count; i++)
        {
            playerObj[i].transform.parent = playerManager.transform;
        }
        // プレイヤー
        for (int i = 0; i < 2; i++)
        {
            playerController.Add(playerObj[i].GetComponent<PlayerController>());
            entryPlayer.Add(mapdata.entryPlayer[i]);
            playerController[i].charaID = chara.entryPlayer[i].playerID;
            playerController[i].name = chara.entryPlayer[i].charaName;
            playerObj[i].name = playerController[i].name;
            playerController[i].icon = chara.entryPlayer[i].icon;
            playerController[i].unitType = (int)chara.entryPlayer[i].unit;
            playerController[i].level = chara.entryPlayer[i].level;
            playerController[i].hp = chara.entryPlayer[i].hp;
            playerController[i].attack = chara.entryPlayer[i].attack;
            playerController[i].deffence = chara.entryPlayer[i].deffence;
            playerController[i].hit = chara.entryPlayer[i].hit;
            playerController[i].speed = chara.entryPlayer[i].speed;
            playerController[i].lucky = chara.entryPlayer[i].lucky;
            playerController[i].moveCost = chara.entryPlayer[i].moveCost;
            playerController[i].unitPos = mapdata.entryPlayer[i].playerPos;
            playerController[i].weapon = chara.entryPlayer[i].weapon;
        }
        // エネミー
        for (int i = 0; i < enemyObj.Length; i++)
        {
            enemyController.Add(enemyObj[i].GetComponent<EnemyController>());
            entryEnemy.Add(mapdata.entryEnemy[i]);
            enemyController[i].enemyID = i;
            enemyController[i].name = mapdata.entryEnemy[i].enemyName;
            enemyController[i].level = mapdata.entryEnemy[i].level;
            enemyController[i].hp = mapdata.entryEnemy[i].hp;
            enemyController[i].attack = mapdata.entryEnemy[i].attack;
            enemyController[i].deffence = mapdata.entryEnemy[i].deffence;
            enemyController[i].hit = mapdata.entryEnemy[i].hit;
            enemyController[i].speed = mapdata.entryEnemy[i].speed;
            enemyController[i].lucky = mapdata.entryEnemy[i].lucky;
            enemyController[i].moveCost = mapdata.entryEnemy[i].moveCost;
            enemyController[i].enemyPos = mapdata.entryEnemy[i].enemyPos;
        }
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                mapUnit[x, y] = new MapUnit();
            }
        }
        SaveCharaParam();
        Debug.Log(mapdata.entryPlayer);
    }

    public void UnitMovable()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                mapUnit[x, y].movable = true;
            }
        }
        // プレイヤー
        /*for (int i = 0; i < playerObj.Length; i++)
        {
            mapUnit[Mathf.FloorToInt(playerController[i].unitPos.x), Mathf.FloorToInt(playerController[i].unitPos.y)].movable = false;
        }*/
        // エネミー
        for (int i = 0; i < enemyObj.Length; i++)
        {
            mapUnit[Mathf.FloorToInt(enemyController[i].enemyPos.x), Mathf.FloorToInt(enemyController[i].enemyPos.y)].movable = false;
        }
    }
    

    public void SaveCharaParam()
    {
    }
}
