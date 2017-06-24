using System.Collections.Generic;
using UnityEngine;

public sealed class CityMapManager {

    private const float _tolerance = 5f;

    private static readonly CityMapManager _instance = new CityMapManager();
    public static CityMapManager Instance
    {
        get { return _instance; }
    }

    private List<Block> _blocks;
    public IList<Block> blocks
    {
        get { return _blocks.AsReadOnly(); }
    }

    private List<Vector3> _nodes;
    public IList<Vector3> nodes
    {
        get { return _nodes.AsReadOnly(); }
    }

    private List<Sidewalk> _sidewalks;
    public IList<Sidewalk> sidewalks
    {
        get { return _sidewalks; }
    }

    private CityMapManager()
    {
        _blocks = new List<Block>();
        _nodes = new List<Vector3>();
        _sidewalks = new List<Sidewalk>();
    }

    public void Add(Block block)
    {
        _blocks.Add(block);
    }

    public int Add(Vector3 node)
    {
        var n = _nodes.FindIndex(0, delegate (Vector3 v) {
            return (Mathf.Abs(v.x - node.x) <= _tolerance &&
                    Mathf.Abs(v.z - node.z) <= _tolerance);
        });
        if (n == -1)
            _nodes.Add(node);
        return n;
    }

    public void AddSidewalk(Block block)
    {
        Sidewalk s = new Sidewalk(block);
        s.name = "sidewalk";
        s.material = MaterialManager.Instance.Get("mat_sidewalk");
        _sidewalks.Add(s);
    }

    public void DrawSidewalks()
    {
        foreach (Sidewalk s in _sidewalks)
        {
            s.FindVertices();
            s.FindTriangles();
            s.Draw();
            s.gameObject.SetActive(true);
        }
    }
    
    public void DestroySidewalks()
    {
        foreach (Sidewalk s in _sidewalks)
            s.Destroy();
    }

    public void Clear()
    {
        _blocks.Clear();
        _nodes.Clear();

        DestroySidewalks();
        _sidewalks.Clear();
    }
    
}
