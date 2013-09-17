using System;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
//    private TileMap _tileMap;

    public void Start()
    {
//        _tileMap = new TileMap(100);
//        var tileFactory = this.AddComponent<TileFactory>();
//        tileFactory.CreateGameObjects(_tileMap);
//        Camera.main.gameObject.AddComponent<TileMapCameraWatcher>().TileMap = _tileMap;
//        CreatePlane("CustomPlane", 250);
        var plane = CreatePlane("CustomPlane", 1);
        var tileObserver = Camera.main.gameObject.AddComponent<CameraTileObserver>();
        tileObserver.TargetTransform = plane.transform;
    }

    private GameObject CreatePlane(string goName, int size)
    {
        var resolution = size + 1;
        var count = resolution*resolution;
        if (count >= 65000)
            throw new Exception("Cannot create a plane mesh with 65000+ vertices. Reduce size.");
        var go = new GameObject(goName);
        var filter = go.AddComponent<MeshFilter>();
        var mesh = filter.mesh;
        mesh.Clear();

        #region Vertices

        var colors = new Color32[count];
        var vertices = new Vector3[count];
        for (var z = 0; z < resolution; z++)
        {
            // [ -length / 2, length / 2 ]
            var zPos = ((float) z/(resolution - 1) - .5f)*size;
            for (var x = 0; x < resolution; x++)
            {
                // [ -width / 2, width / 2 ]
                var xPos = ((float) x/(resolution - 1) - .5f)*size;
                vertices[x + z*resolution] = new Vector3(xPos, 0f, zPos);
                colors[x + z*resolution] = Utils.RandomColor32();
            }
        }

        #endregion

        #region Normals

        var normals = new Vector3[vertices.Length];
        for (var n = 0; n < normals.Length; n++)
            normals[n] = Vector3.up;

        #endregion

        #region UVs

        var uvs = new Vector2[vertices.Length];
        for (var v = 0; v < resolution; v++)
        {
            for (var u = 0; u < resolution; u++)
                uvs[u + v*resolution] = new Vector2((float) u/(resolution - 1), (float) v/(resolution - 1));
        }

        #endregion

        #region Triangles

        var nbFaces = (resolution - 1)*(resolution - 1);
        var triangles = new int[nbFaces*6];
        var t = 0;
        for (var face = 0; face < nbFaces; face++)
        {
            // Retrieve lower left corner from face ind
            var i = face%(resolution - 1) + (face/(resolution - 1)*resolution);
            triangles[t++] = i + resolution;
            triangles[t++] = i + 1;
            triangles[t++] = i;
            triangles[t++] = i + resolution;
            triangles[t++] = i + resolution + 1;
            triangles[t++] = i + 1;
        }

        #endregion

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.colors32 = colors;
        mesh.RecalculateBounds();
        mesh.Optimize();
        var mr = go.AddComponent<MeshRenderer>();
        //mr.material = new Material(Shader.Find("Diffuse"));
        mr.material = Resources.Load("VertexColorUnlit") as Material;

        return go;
    }
}