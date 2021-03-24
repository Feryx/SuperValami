using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class valami : MonoBehaviour
{
    public float A = 1.56f;
    public float B = 1.06f;
    public float C = 1.06f;
    public float M = 1.42f;
    public float N1 = 1.7f;
    public float N2 = 4.6f;
    public float N3 = 1.02f;
    public float Lat = 3f;
    public float SLong = 4.13f;
    public float phi = 1f;
    public float Sx = 1f;
    public float Sy = 1f;
    public float Sz = 1f;
    public float ZSzorzo = 1f;
    public GameObject me;
    public int numSelectors = 2048;
    private GameObject[] selectorArr;
    void Start()
    {

        selectorArr = new GameObject[numSelectors];
        for (int i = 0; i < numSelectors; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(0, 0.0f, 0);
            go.transform.parent = me.transform;
            selectorArr[i] = go;
        }
    }
    void Update()
    {
        //ezt szedd ki és manuálisan játszhatsz vele.
        N3 += 0.015f;  ZSzorzo += 0.0001f;

        for (int i = 0; i < numSelectors; i++)
        {
            phi = i * Lat / SLong;

            var t1 = Mathf.Cos(M * phi / 4) / A;
             t1 = Mathf.Abs(t1);
             t1 = Mathf.Pow(t1, N2);

            var t2 = Mathf.Sin(M * phi / 4) / B;
             t2 = Mathf.Abs(t2);
             t2 = Mathf.Pow(t2, N3);

            var t3 = Mathf.Tan((M*0.00000001f) * phi / 40) / C;
             t3 = Mathf.Abs(t2);
             t3 = Mathf.Pow(t2, N1);

            var r = Mathf.Pow(t1 + t2, 1 / N1);
            if (Mathf.Abs(r) == 0)
            {
                Sx = 0;
                Sy = 0;
                Sz = 0;
            }
            else
            {
                r = 1 / r;
                Sx = r * Mathf.Cos(phi);
                Sy = r * Mathf.Sin(phi);
                Sz = r * Mathf.Sin(phi*ZSzorzo);
            }
            selectorArr[i].GetComponent<Renderer>().material.color = new Color(Mathf.Sin(Sy) * 1f, Mathf.Cos(Sx) * 1f, Mathf.Tan(Sx) * 1f);
            selectorArr[i].transform.localPosition = new Vector3(Sx,Sy,Sz);
            selectorArr[i].transform.Rotate(Mathf.Sin(Sy)*1f, Mathf.Cos(Sx)*1f, Sx*Sy, Space.World);
        }
    }
}
