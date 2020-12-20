
using UnityEngine;
using UnityEditor;

[SerializeField]
[ExecuteAlways]
public class HeatMapRenderer : MonoBehaviour
{
    MapCube[,] heat_matrix = null;
    Vector2 size = new Vector2(100, 100);
    Mesh plane;
    private void Start()
    {
        GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane = tmp.GetComponent<Mesh>();
        Destroy(tmp);
    }
    private void Update()
    {
        if (heat_matrix != null)
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Matrix4x4 transform = Matrix4x4.identity;
                    transform.SetTRS(heat_matrix[i, j].position, Quaternion.identity, Vector3.one);
                    Graphics.DrawMeshNow(plane, transform);

                    Gizmos.DrawCube(heat_matrix[i, j].position, Vector3.one / 2);
                }
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    if (heat_matrix != null)
    //    {
    //        for (int i = 0; i < size.x; i++)
    //        {
    //            for (int j = 0; j < size.y; j++)
    //            {
    //                Matrix4x4 transform = Matrix4x4.identity;
    //                transform.SetTRS(heat_matrix[i, j].position, Quaternion.identity, Vector3.one);
    //                Graphics.DrawMeshNow(plane, transform);

    //                Gizmos.DrawCube(heat_matrix[i, j].position, Vector3.one/2);
    //            }
    //        }
    //    }
    //}
    public void OnPostRender()
    {
        //if (heat_matrix != null)
        //{
        //    for (int i = 0; i < size.x; i++)
        //    {
        //        for (int j = 0; j < size.y; j++)
        //        {
        //            Matrix4x4 transform = Matrix4x4.identity;
        //            transform.SetTRS(heat_matrix[i, j].position, Quaternion.identity, Vector3.one);
        //            Graphics.DrawMeshNow(plane, transform);

        //            //Gizmos.DrawCube(heat_matrix[i, j].position, Vector3.one / 2);
        //        }
        //    }
        //}
    }

    public void Draw(MapCube[,] heat_matrix)
    {
        this.heat_matrix = heat_matrix;

    }


}
