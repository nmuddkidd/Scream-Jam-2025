using System.Diagnostics;
using System;
using UnityEngine;

public class GorillaSpawn : MonoBehaviour
{
    public GameObject gorillaPrefab;
    public float jumpForce = 1200f;
    public float xSpeed = 5f;
    public float jumpInterval = 2f;
    public float nextJumpTime;
    public float direction = 1f;
    public float leftBound = -7.5f;
    public float rightBound = 7.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnGorillaRoutine()
    {
        Vector2 spawnPosition = new Vector2(0, -4.2F);
        GameObject spawnedGorilla = Instantiate(gorillaPrefab, spawnPosition, transform.rotation);

        Gorilla movement = spawnedGorilla.AddComponent<Gorilla>();
        movement.leftBound = leftBound;
        movement.rightBound = rightBound;
        movement.xSpeed = xSpeed;
        movement.jumpForce = jumpForce;
        movement.jumpInterval = jumpInterval;

        UnityEngine.Debug.Log("Gorilla Spawned");
        Destroy(spawnedGorilla, 5f);
    }

}
