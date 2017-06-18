using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ICombinable
{
    UnityEngine.GameObject gameObject { get; }

    UnityEngine.MeshFilter meshFilter { get; }

    UnityEngine.Material material { get; }

    UnityEngine.Mesh mesh { get; }
}
