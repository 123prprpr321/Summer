using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public float launchForce;//�Թ���ʩ����
    public Transform shotPoint;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    Vector2 direction;
    private void Start()
    {
        points = new GameObject[numberOfPoints];
        for(int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }
    //void Update()
    //{
    //    //��ת
    //    Vector2 bowPosition = transform.position;
    //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 direction = mousePosition - bowPosition;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //    //����
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        Shoot();
    //    }

    //    for(int i = 0; i < numberOfPoints; i++)
    //    {
    //        points[i].transform.position = PointPosition(i * spaceBetweenPoints);
    //    }
    //}
    void Update()
    {
        // ��ת
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition; // �޸Ĵ˴���ȥ��Vector2
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // ����
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }
    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }
    //���ƹ켣
    //Vector2 PointPosition(float t)
    //{
    //    Vector2 position = (Vector2)shotPoint.position + (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
    //    return position;
    //}
    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shotPoint.position + (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }
}
