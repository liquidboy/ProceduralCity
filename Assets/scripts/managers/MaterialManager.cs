using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class MaterialManager {

    private static readonly MaterialManager _instance = new MaterialManager();
    public static MaterialManager Instance
    {
        get { return _instance; }
    }

    private static bool _isInitialized;

    private Dictionary<string, Material> _materials;

    private MaterialManager()
    {
        _isInitialized = false;
        _materials = new Dictionary<string, Material>();
    }

    public void Init()
    {
        Console.WriteLine("MaterialManager.Init");

        if (!_isInitialized)
        {
            Material mat;

            // lines
            mat = new Material(Shader.Find("VertexLit"));
            mat.name = "line_block";
            mat.SetColor("_Color", Color.green);
            mat.SetColor("_SpecColor", Color.green);
            mat.SetColor("_Emission", Color.green);
            Add(mat.name, mat);

            mat = new Material(Shader.Find("VertexLit"));
            mat.name = "line_sidewalk";
            mat.SetColor("_Color", Color.red);
            mat.SetColor("_SpecColor", Color.red);
            mat.SetColor("_Emission", Color.red);
            Add(mat.name, mat);

            _isInitialized = true;
        }
    }

    public void Add(string name, Material material)
    {
        if (!_materials.ContainsKey(name))
        {
            material.name = name;
            _materials.Add(name, material);
        }
    }

    public Material Get(string name)
    {
        return _materials.ContainsKey(name) ? _materials[name] : null;
    }

    public void Unload()
    {
        foreach (Material m in _materials.Values)
            Object.DestroyImmediate(m, true);
        _materials.Clear();

        _isInitialized = false;
    }
}
