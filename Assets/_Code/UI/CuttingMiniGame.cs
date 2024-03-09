using DG.Tweening;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingMiniGame : MiniGamePanel
{
    [SerializeField]
    public Cutter cutter;

    [SerializeField]
    public GameObject nodePrefab;

    [SerializeField]
    public Transform nodeSpawnPoint;

    [SerializeField]
    public Transform nodeEndPoint;

    [SerializeField]
    public float speed;

    [SerializeField]
    public int count;

    [SerializeField]
    public float delay;
    

    //public void Start()
    //{
    //    StartGame();
    //}

    public override void StartGame()
    {
        StartCoroutine(SpawnNodes());
    }

    public void OnCutButtonPressed()
    {
        if (cutter.CollisionList.Count == 1)
        {
            cutter.CollisionList[0].transform.DOKill();
            Destroy(cutter.CollisionList[0]);
        }
    }

    IEnumerator SpawnNodes()
    {
        for (int i = 0; i < count; i++)
        {
            var node = Instantiate(nodePrefab, nodeSpawnPoint.transform.position, Quaternion.identity, transform);
            node.transform.DOMoveX(nodeEndPoint.transform.position.x, speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() => _destroyNode(node));
            yield return new WaitForSeconds(delay);
        }
    }

    private void _destroyNode(GameObject obj)
    {
        Debug.Log("fail");
        Destroy(obj);
    }
}