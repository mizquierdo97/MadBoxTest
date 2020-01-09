using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    // Start is called before the first frame update

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for(int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawSphere(points[i], 1);
        }
    }
}
