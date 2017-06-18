using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMapController : MonoBehaviour {

    private Block block;

    void Start () {
        Console.WriteLine("CityMapController.Start");

        block = new Block();
        block.Bisect();

    }

	void Update () {
        
    }

    void OnPostRender()
    {
        MaterialManager.Instance.Get("line_block").SetPass(0);
        GL.Begin(GL.LINES);
        foreach (Edge e in block.edges)
        {
            GL.Vertex(e.start);
            GL.Vertex(e.end);
        }
        GL.End();
    }
}
