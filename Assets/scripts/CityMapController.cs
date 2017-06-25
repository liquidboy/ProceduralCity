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
        if (Input.GetKeyUp(KeyCode.R))
        {
            block = new Block();
            block.Bisect();
        }
    }

    void OnPostRender()
    {
        //draw FIRST BLOCK
        MaterialManager.Instance.Get("line_block").SetPass(0);
        GL.Begin(GL.LINES);
        foreach (Edge e in block.edges)
        {
            GL.Vertex(e.start);
            GL.Vertex(e.end);
        }
        GL.End();

        //draw BLOCKS
        MaterialManager.Instance.Get("line_block").SetPass(0);
        GL.Begin(GL.LINES);
        foreach (Block b in CityMapManager.Instance.blocks)
            foreach (Edge e in b.edges)
            {
                GL.Vertex(e.start);
                GL.Vertex(e.end);
            }
        GL.End();

        //draw SIDEWALKS
        MaterialManager.Instance.Get("line_sidewalk").SetPass(0);
        GL.Begin(GL.LINES);
        foreach (Sidewalk s in CityMapManager.Instance.sidewalks)
            foreach (Edge e in s.edges)
            {
                GL.Vertex(e.start);
                GL.Vertex(e.end);
            }
        GL.End();

        //draw FIRST LOT
        MaterialManager.Instance.Get("line_lot").SetPass(0);
        GL.Begin(GL.LINES);
        foreach (Block b in CityMapManager.Instance.blocks)
            foreach (Edge e in b.initialLot.edges)
            {
                GL.Vertex(e.start);
                GL.Vertex(e.end);
            }
        GL.End();

        //draw LOTS
        MaterialManager.Instance.Get("line_lot").SetPass(0);
        GL.Begin(GL.LINES);
        foreach (Block b in CityMapManager.Instance.blocks)
            foreach (BuildingLot l in b.finalLots)
                foreach (Edge e in l.edges)
                {
                    GL.Vertex(e.start);
                    GL.Vertex(e.end);
                }
        GL.End();
    }
}