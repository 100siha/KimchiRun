using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("스폰 설정")]
    public float initialSpawnDelay = 1.5f; // 첫 생성 시작 지연 시간 (빌딩은 약간 더 길게 늘려주면 겹치지 않아요!)
    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 3.0f;

    [Header("생성할 건물")]
    public GameObject[] buildingPrefabs;

    private void OnEnable()
    {
        // 고정으로 1.5초를 쓰지 않고, 설정된 initialSpawnDelay 값을 사용하도록 변경!
        Invoke("Spawn", initialSpawnDelay);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Spawn()
    {
        MakeInstance();
        float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
        
        // [자동 보정] 생성하려는 프리팹 이름에 영어로 'build'가 들어가 있다면, 스크립트가 알아서 간격을 확 벌려버립니다!
        if (buildingPrefabs != null && buildingPrefabs.Length > 0 && buildingPrefabs[0] != null)
        {
            if (buildingPrefabs[0].name.ToLower().Contains("build"))
            {
                randomTime = Random.Range(2.5f, 4.5f); // 플레이어 피드백 반영: 2.5 ~ 4.5초로 약간 좁힘
            }
        }

        Invoke("Spawn", randomTime);
    }

    void MakeInstance()
    {
        GameObject randomBuilding = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
        Instantiate(randomBuilding, transform.position, Quaternion.identity);
    }
}
