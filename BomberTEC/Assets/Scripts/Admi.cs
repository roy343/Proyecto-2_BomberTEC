﻿using UnityEngine;
using EPoblacion;
using EBomberman;
using EListEnemy;
public class Admi : MonoBehaviour
{
    // Start is called before the first frame update
    private Poblacion foo;
    private ListEnemy PeopleEnemy;

    public EnemyScript enemy0;
    public EnemyScript enemy1;
    public EnemyScript enemy2;
    public EnemyScript enemy3;
    public EnemyScript enemy4;
    public EnemyScript enemy5;
    public EnemyScript enemy6;
    void MeterDatos()
    {
        for (int i = 0; i < foo.people.cont; i++)
        {
            Bomberman temp = foo.people.getData(i);
            PeopleEnemy.getData(i).hide = temp.getHide();
            PeopleEnemy.getData(i).putBomb = temp.getPutBomb();
            PeopleEnemy.getData(i).findEnemy = temp.getFindEnemy();
            PeopleEnemy.getData(i).findPowerUp = temp.getFindPowerUp();
        }
    }
    void doList()
    {
        PeopleEnemy.addData(enemy0);
        PeopleEnemy.addData(enemy1);
        PeopleEnemy.addData(enemy2);
        PeopleEnemy.addData(enemy3);
        PeopleEnemy.addData(enemy4);
        PeopleEnemy.addData(enemy5);
        PeopleEnemy.addData(enemy6);
    }
    void Awake()
    {
        PeopleEnemy = new ListEnemy();
        foo = new Poblacion();
    }
    void Start(){
        doList();
        MeterDatos();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
