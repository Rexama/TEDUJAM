using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CrushingMiniGame : MiniGamePanel
{
    [SerializeField]
    public Station station;

    public override Station Station => station;

    [SerializeField]
    private GameObject startPrefab;

    [SerializeField]
    private GameObject endPrefab;

    private GameObject curStart;
    private GameObject curEnd;

    [SerializeField]
    private int howMany;

    [SerializeField]
    private float timer;

    private float curTime;

    private int curCount;

    private bool shouldIncreaseTimer;

    [SerializeField]
    private TextMeshProUGUI counttext;
    [SerializeField]
    private TextMeshProUGUI timertext;

    public override void StartGame()
    {
        curCount = 0;
        curTime = 0;
        shouldIncreaseTimer = false;
        SpawnPoints();
    }

    // Function to get the world rect of a RectTransform
    Rect GetWorldRect(RectTransform rectTransform)
    {
        // Get the corners of the rectTransform in world space
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        // Find the minimum and maximum coordinates
        float minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        float minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
        float maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        float maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);

        // Create a Rect from the found coordinates
        var pad = 50.0f;
        Rect worldRect = new Rect(minX + pad, minY + pad, maxX - minX - pad, maxY - minY - pad);

        return worldRect;
    }
    Vector2 GetRandomPointInRect(Rect rect)
    {
        float randomX = Random.Range(rect.xMin, rect.xMax);
        float randomY = Random.Range(rect.yMin, rect.yMax);

        return new Vector2(randomX, randomY);
    }

    private void SpawnPoints()
    {
        var rectT = transform as RectTransform;
        var worldRect = GetWorldRect(rectT);
        var firstPos = GetRandomPointInRect(worldRect);
        curStart = Instantiate(startPrefab, firstPos, Quaternion.identity, transform);
        var mindist = 250.0f;
        var maxdist = 800.0f;

        var p2 = Vector2.zero;
        while (!worldRect.Contains(p2))
        {
            p2 = firstPos + (Random.insideUnitCircle.normalized * Random.Range(mindist, maxdist));
        }

        curEnd = Instantiate(endPrefab, p2, Quaternion.identity, transform);

        curStart.transform.up = -curEnd.transform.position + curStart.transform.position;

        var drag = curStart.GetComponent<CrushDrag>();
        drag.onDragEndEvent.AddListener(OnEnd);
        drag.onDragStartEvent.AddListener(OnStart);
    }

    private void OnStart()
    {
        shouldIncreaseTimer = true;
    }

    private void OnEnd()
    {
        Destroy(curStart);
        Destroy(curEnd);
        if (Vector2.Distance(curStart.transform.position, curEnd.transform.position) < 100.0f)
        {
            if (curCount < howMany)
            {
                curCount++;
                curTime = 0;
                shouldIncreaseTimer = false;
                SpawnPoints();
            }
            else
            {
                WinMiniGame();
            }
        }
        else
        {
            LoseMiniGame();
        }
    }

    private void Update()
    {
        if (shouldIncreaseTimer)
        curTime += Time.deltaTime;
        if (curTime >= timer)
        {
            LoseMiniGame();
        }
        timertext.text = (timer - curTime).ToString("0.00") + "s";
        counttext.text = (howMany - curCount + 1).ToString();
    }
}