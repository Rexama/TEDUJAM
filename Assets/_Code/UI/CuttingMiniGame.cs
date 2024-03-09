using DG.Tweening;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingMiniGame : MiniGamePanel
{
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

    public void Start()
    {
        StartGame();
    }

    public override void StartGame()
    {
        StartCoroutine(SpawnNodes());
    }

    public void OnCutButtonPressed()
    {
        Debug.Log("asd");
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