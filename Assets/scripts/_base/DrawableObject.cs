﻿using UnityEngine;

public class DrawableObject : ProceduralObject,
                              IDrawable,
                              ICombinable
{
    /*************** FIELDS ***************/

    public string name;

    public Material material;

    public MeshFilter meshFilter;

    public Mesh mesh;

    public Vector3 meshOrigin;

    public Vector3[] boundaries;

    public Vector3[] vertices;

    public int[] triangles;

    /*************** CONSTRUCTORS ***************/

    public DrawableObject(string name = "drawableObject", string materialName = null)
    {
        this.name = name;
        if (materialName != null)
            this.material = MaterialManager.Instance.Get(materialName);
    }

    /*************** METHODS ***************/

    /// <summary>
    /// Calculates the intersection point of the lines formed by each two points in 3D space.
    /// Points a1 and a2 form the first line and points b1 and b2 form the second.
    /// Maths found here: http://mathforum.org/library/drmath/view/62814.html
    /// </summary>
    public virtual void FindMeshOrigin(Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2)
    {
        var a_dir = a1 - a2; // the direction vector of the first line
        var b_dir = b1 - b2; // the direction vector of the second line
        var dir_cross = Vector3.Cross(a_dir, b_dir);
        var tmp_cross = Vector3.Cross(b1 - a1, b_dir);

        float c1;
        if (dir_cross.x != 0f)
            c1 = tmp_cross.x / dir_cross.x;
        else if (dir_cross.y != 0f)
            c1 = tmp_cross.y / dir_cross.y;
        else if (dir_cross.z != 0f)
            c1 = tmp_cross.z / dir_cross.z;
        else
            throw new System.DivideByZeroException("Result of cross product is Vector3(0,0,0)!");

        meshOrigin = a1 + c1 * a_dir;
    }

    public virtual void FindVertices()
    {
        throw new System.NotImplementedException();
    }

    public virtual void FindTriangles()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Draw()
    {
        gameObject = new GameObject(name);
        meshFilter = gameObject.AddComponent<MeshFilter>();
        var renderer = gameObject.AddComponent<MeshRenderer>();

        gameObject.SetActive(false);
        gameObject.isStatic = true;
        if (material != null)
            renderer.sharedMaterial = material;

        mesh = new Mesh();
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = new Vector2[mesh.vertices.Length];
        mesh.RecalculateNormals();
        ;
        meshFilter.sharedMesh = mesh;
    }

    public virtual void Destroy()
    {
        GameObject.DestroyImmediate(mesh);
        GameObject.DestroyImmediate(gameObject);
    }

    /*************** INTERFACE EXPLICIT IMPLEMENTATION ***************/

    Vector3[] IDrawable.vertices
    {
        get { return vertices; }
    }

    int[] IDrawable.triangles
    {
        get { return triangles; }
    }

    GameObject ICombinable.gameObject
    {
        get { return gameObject; }
    }

    MeshFilter ICombinable.meshFilter
    {
        get { return meshFilter; }
    }

    Material ICombinable.material
    {
        get { return material; }
    }

    Mesh ICombinable.mesh
    {
        get { return mesh; }
    }
}

