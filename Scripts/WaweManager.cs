using System.Collections;
using System.Linq;
using UnityEngine;

public class WaweManager : MonoBehaviour
{
    public PoolObject[] enemys;
    public Transform waweStartPoint;
    public GameObject wawePanel;

    private int enemiesSizeInWawe;
    private float lastEnemySpawnTime;
    private float waweStartTime, waweTime;
    private float spawnTimeBetweenEnemies;
    
    [HideInInspector]
    public int waweCount;

    private static WaweManager _instance;
    public static WaweManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<WaweManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        spawnTimeBetweenEnemies = 1.5f;
        waweCount = 1;
        waweTime = 1f;
        waweStartTime = Time.time;
        enemiesSizeInWawe = 0;
    }


    private void Update()
    {
        if((Time.time- waweStartTime) > waweTime)
        {
            WaweStarter();
        }
        if (enemiesSizeInWawe > 0)
        {
            if(Time.time - lastEnemySpawnTime > spawnTimeBetweenEnemies)
            {
                PoolManager.Instance.UsePoolObject(enemys[Random.Range(0, enemys.Length)], waweStartPoint.position, Quaternion.identity);
                lastEnemySpawnTime = Time.time;
                enemiesSizeInWawe--;
            }
        }

    }

    public void WaweStarter()
    {
        StartCoroutine(WaweShower());
        waweStartTime = Time.time;
        enemiesSizeInWawe = 3 + waweCount / 5;
        waweTime = 12 + enemiesSizeInWawe * spawnTimeBetweenEnemies;
        lastEnemySpawnTime = Time.time;
        waweCount++;
    }

    public IEnumerator WaweShower() 
    {
        wawePanel.SetActive(true);
        wawePanel.GetComponent<WawePanelScript>().SetWaweText(waweCount.ToString());
        yield return new WaitForSeconds(1.5f);
        wawePanel.SetActive(false);
    }

}
