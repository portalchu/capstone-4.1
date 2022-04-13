using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 범위에 적이 있는지 확인하는 코드
public class EnemyDetect : MonoBehaviour
{
    public List<GameObject> enemyList; // 적 리스트
    public Tower parentTower; // 부모의 타워
    void Start()
    {
        Renderer renderer;
        renderer = GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.a = 0.3f;
        renderer.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 적이 범위에 들어오면 정보 추가
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other enter tag : " + other.tag);

        if (other.tag == "Monster")
        {
            enemyList.Add(other.gameObject);
        }
    }

    // 적이 범위에서 나가면 정보 제거
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("other exit tag : " + other.tag);

        if (other.tag == "Monster")
        {
            enemyList.Remove(other.gameObject);
        }
    }

    // 제일 먼저 들어온 적 정보 가져오기
    public GameObject GetTarget()
    {
        return FindEnemyClosestToTower();
    }

    // 적이 있는지 확인
    public bool EnemyDetectCheck()
    {

        if (enemyList.Count <= 0)
        {
            return false;
        }

        return true;
    }

    // 타워와 가장 가까운 적 찾기
    public GameObject FindEnemyClosestToTower()
    {
        GameObject target = null;
        float minDir = -1;

        foreach (GameObject enemy in enemyList)
        {
            // 아이템이 들어있는 리스트에서 아이템과 플레이어의 거리를 계산
            float dir = Vector3.Distance(transform.position, enemy.transform.position);

            // 첫 계산 또는 최소 거리보다 가까우면 해당 아이템으로 변경
            if (minDir > dir || minDir == -1)
            {
                minDir = dir;
                target = enemy;
            }
        }

        return target;
    }
}
