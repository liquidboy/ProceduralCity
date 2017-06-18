using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IDrawable : ICombinable
{
    UnityEngine.Vector3[] vertices { get; }

    int[] triangles { get; }

    void FindVertices();

    void FindTriangles();

    void Draw();

    void Destroy();
}
