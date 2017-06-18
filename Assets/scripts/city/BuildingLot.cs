﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public sealed class BuildingLot : DrawableObject
{
    // edges[0] is _always_ adjacent to a street
    public readonly List<Edge> edges = new List<Edge>();

    // contains the indexes of the faces that are not
    // in contact with other walls or covered.
    public readonly List<int> freeEdges = new List<int>();

    // key -> index of edge
    // value -> percentage of length that is occupied
    public Dictionary<int, float> occupied;

    // key -> index of edge
    // value -> list that contains all the points on that edge
    public Dictionary<int, List<Vector3>> pointsInEdge;

    public BuildingLot(Block parent)
    {
        var edgeList = new List<Edge>();
        for (int i = 0; i < 4; ++i)
            edgeList.Add(new Edge(parent.lotVerts[i], parent.lotVerts[(i + 1) % 4]));

        int big = edgeList.FindIndex(delegate (Edge e) {
            return e.length == edgeList.Max(t => t.length);
        });
        for (int i = 0; i < 4; ++i)
            edges.Add(edgeList[(big + i) % 4]);

        occupied = new Dictionary<int, float>();
        occupied.Add(0, 0f);
        occupied.Add(1, 0f);
        occupied.Add(2, 0f);
        occupied.Add(3, 0f);

        pointsInEdge = new Dictionary<int, List<Vector3>>();
        pointsInEdge.Add(0, new List<Vector3>());
        pointsInEdge.Add(1, new List<Vector3>());
        pointsInEdge.Add(2, new List<Vector3>());
        pointsInEdge.Add(3, new List<Vector3>());
    }

    public BuildingLot(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        edges.Add(new Edge(p1, p2));
        edges.Add(new Edge(p2, p3));
        edges.Add(new Edge(p3, p4));
        edges.Add(new Edge(p4, p1));
    }

    public bool isFinal()
    {
        float min = edges.Min(e => e.length);

        if (edges[0].length >= 2.5f * min)
            return false;

        if (edges[0].length < 16f && min < 16f)
            return true;

        return false;
    }

    public override void FindVertices()
    {
        vertices = new Vector3[4];
        for (int i = 0; i < 4; ++i)
            vertices[i] = edges[i].start - 0.01f * Vector3.up;
    }

    public override void FindTriangles()
    {
        triangles = new int[6];
        int i = 0;

        triangles[i++] = 0;
        triangles[i++] = 1;
        triangles[i++] = 2;

        triangles[i++] = 0;
        triangles[i++] = 2;
        triangles[i++] = 3;
    }
}
