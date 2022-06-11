#if PATHFINDING
using System;
using System.Collections.Generic;
using System.Linq;
using Features.Planet.Resources.Scripts;
using Pathfinding;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Progress = Pathfinding.Progress;

namespace Features.WorkerAI.Scripts.Pathfinding
{
    [JsonOptIn]
    [Preserve]
    public class PlanetNavMeshGraph : NavmeshBase
    {

        // private PlanetCubes_SO planetCubes;

        // /// <summary>Rotation of the graph in degrees</summary>
        // // TODO ???
        // private Vector3 rotation = Vector3.zero;

        [JsonMember]
        public int verticesPerEdge = 2;

        /// <summary>
        /// Center of the bounding box.
        /// Scanning will only be done inside the bounding box
        /// </summary>
        // TODO not set statically but match actual planet
        // private Vector3 forcedBoundsCenter = new Vector3(45, 0, -35);
        // private Vector3 forcedBoundsCenter = new Vector3(45 + 35, 0 + 5, -35 + 35);
        // TODO Actual outcome position: 10 / -5 / -70
        public override GraphTransform CalculateTransform()
        {
            // return new GraphTransform(Matrix4x4.TRS(forcedBoundsCenter, Quaternion.Euler(rotation), Vector3.one) * Matrix4x4.TRS(-forcedBoundsSize * 0.5f, Quaternion.identity, Vector3.one));
            return new GraphTransform(Matrix4x4.TRS(new Vector3(45 + 35, 0 + 5, -35 + 35), Quaternion.Euler(Vector3.zero), Vector3.one) * Matrix4x4.TRS(-forcedBoundsSize * 0.5f, Quaternion.identity, Vector3.one));
        }

        public override float TileWorldSizeX => forcedBoundsSize.x;

        public override float TileWorldSizeZ => forcedBoundsSize.z;

        // Tiles are not supported, so this is irrelevant
        protected override float MaxTileConnectionEdgeDistance => 0f;
        // We do not want to our normals to be flipped/normalized --> worker can walk on walls & south of the planet
        protected override bool RecalculateNormals => false;

        // TRYOUT create verts on my own
        // private TriangleMeshNode[] nodes;
        //
        // /// <summary>Graph vertices</summary>
        // public Int3[] verts;
        //
        // /// <summary>Graph vertices in graph space</summary>
        // public Int3[] vertsInGraphSpace;

        // TODO set forcedBoundsCenter & forcedBoundsSize

        protected override IEnumerable<Progress> ScanInternal(bool async)
        {
            transform = CalculateTransform();
            tileZCount = tileXCount = 1;
            tiles = new NavmeshTile[tileZCount * tileXCount];
            TriangleMeshNode.SetNavmeshHolder(AstarPath.active.data.GetGraphIndex(this), this);

            // TODO
            yield return new Progress(0.0f, "Scanning planet cubes");

            var planetCubes = AssetDatabase.LoadAssetAtPath<PlanetCubes_SO>("Assets/Features/Planet/Resources/Data/PlanetCubes.asset");

            var vertices = new List<Vertex>();
            var faces = new List<Face>();
            int size = planetCubes.Size + 1;
            // var verticesInSpace = new Vertex[size][][];
            var verticesInSpace = new Dictionary<Vertex.Key, Vertex>();
            // for (int x = 0; x < size; x++)
            // {
            //     verticesInSpace[x] = new Vertex[size][];
            //     for (int y = 0; y < size; y++)
            //     {
            //         verticesInSpace[x][y] = new Vertex[size];
            //     }
            // }

            planetCubes.GetCubes(((cube, x, y, z) => {
                if (!planetCubes.HasCubeAt(x - 1, y, z))
                {
                    var face = new Face {
                        vertices = {
                            [Face.BOTTOM_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y, z),
                            [Face.BOTTOM_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y, z + 1),
                            [Face.TOP_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y + 1, z + 1),
                            [Face.TOP_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y + 1, z)
                        }
                    };

                    face.ConnectVertices();
                    faces.Add(face);
                }

                if (!planetCubes.HasCubeAt(x + 1, y, z))
                {
                    var face = new Face {
                        vertices = {
                            [Face.BOTTOM_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y, z),
                            [Face.BOTTOM_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y, z + 1),
                            [Face.TOP_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y + 1, z + 1),
                            [Face.TOP_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y + 1, z)
                        }
                    };

                    face.ConnectVertices();
                    faces.Add(face);
                }

                if (!planetCubes.HasCubeAt(x, y - 1, z))
                {
                    var face = new Face {
                        vertices = {
                            [Face.BOTTOM_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y, z),
                            [Face.BOTTOM_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y, z + 1),
                            [Face.TOP_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y, z + 1),
                            [Face.TOP_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y, z)
                        }
                    };

                    face.ConnectVertices();
                    faces.Add(face);
                }

                if (!planetCubes.HasCubeAt(x, y + 1, z))
                {
                    var face = new Face {
                        vertices = {
                            [Face.BOTTOM_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y + 1, z),
                            [Face.BOTTOM_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y + 1, z + 1),
                            [Face.TOP_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y + 1, z + 1),
                            [Face.TOP_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y + 1, z)
                        }
                    };

                    face.ConnectVertices();
                    faces.Add(face);
                }

                if (!planetCubes.HasCubeAt(x, y, z - 1))
                {
                    var face = new Face {
                        vertices = {
                            [Face.BOTTOM_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y, z),
                            [Face.BOTTOM_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y + 1, z),
                            [Face.TOP_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y + 1, z),
                            [Face.TOP_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y, z)
                        }
                    };

                    face.ConnectVertices();
                    faces.Add(face);
                }

                if (!planetCubes.HasCubeAt(x, y, z + 1))
                {
                    var face = new Face {
                        vertices = {
                            [Face.BOTTOM_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y, z + 1),
                            [Face.BOTTOM_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x, y + 1, z + 1),
                            [Face.TOP_RIGHT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y + 1, z + 1),
                            [Face.TOP_LEFT] =
                                GetOrCreateVertex(verticesInSpace, vertices, x + 1, y, z + 1)
                        }
                    };

                    face.ConnectVertices();
                    faces.Add(face);
                }
            }));

            // TODO not set statically but match actual planet
            forcedBoundsSize = new Vector3(30, 30, 30);

            // TODO generate sub-faces
            
            // var subFaces = new List<Face>(); // TODO apply capacity of 2 * (n-1)^2
            // foreach (var face in faces)
            // {
            //     var bottomLeftVertex = face.vertices[Face.BOTTOM_LEFT];
            //     var subFace = new Face {
            //         vertices = {
            //             [Face.BOTTOM_LEFT] =
            //                 GetOrCreateVertex(verticesInSpace, vertices, bottomLeftVertex.x, bottomLeftVertex.y, bottomLeftVertex.z),
            //             [Face.BOTTOM_RIGHT] =
            //                 GetOrCreateVertex(verticesInSpace, vertices, x, y + 1, z + 1),
            //             [Face.TOP_RIGHT] =
            //                 GetOrCreateVertex(verticesInSpace, vertices, x + 1, y + 1, z + 1),
            //             [Face.TOP_LEFT] =
            //                 GetOrCreateVertex(verticesInSpace, vertices, x + 1, y, z + 1)
            //         }
            //     };
            //
            //     face.ConnectVertices();
            //     faces.Add(face);
            // }
            
            foreach (var vertex in vertices)
            {
                // vertex.DebugDraw(forcedBoundsCenter - forcedBoundsSize / 2, 10.0f);
                vertex.DebugDraw(new Vector3(45, 0, -35) - forcedBoundsSize / 2, 10.0f);
            }

            // TRYOUT convert to raw verts & triangles and let NavmeshBase take care of the rest
            Int3[] rawVerts = new Int3[vertices.Count];
            int[] triangles = new int[6 * faces.Count];

            for (int i = 0; i < vertices.Count; i++)
            {
                var vertex = vertices[i];
                rawVerts[i] = vertex.ToInt3() * 10.0f;
                vertex.index = i;
            }

            var triangleIndex = 0;
            foreach (var face in faces)
            {
                // Each face becomes 2 triangles
                triangles[triangleIndex * 3 + 0] = face.vertices[Face.BOTTOM_LEFT].AddToTriangle(triangleIndex);
                triangles[triangleIndex * 3 + 1] = face.vertices[Face.BOTTOM_RIGHT].AddToTriangle(triangleIndex);
                triangles[triangleIndex * 3 + 2] = face.vertices[Face.TOP_LEFT].AddToTriangle(triangleIndex);
                triangleIndex++;

                triangles[triangleIndex * 3 + 0] = face.vertices[Face.TOP_RIGHT].AddToTriangle(triangleIndex);
                triangles[triangleIndex * 3 + 1] = face.vertices[Face.BOTTOM_RIGHT].AddToTriangle(triangleIndex);
                triangles[triangleIndex * 3 + 2] = face.vertices[Face.TOP_LEFT].AddToTriangle(triangleIndex);
                triangleIndex++;

                // Create new connection for later
                face.vertices[Face.BOTTOM_RIGHT].Connect(face.vertices[Face.TOP_LEFT]);

            }

            ReplaceTile(0, 0, rawVerts, triangles);

            // var visitedVertices = new HashSet<int>();
            // // TRYOUT fix connections
            // for (int nodeIndex = 0; nodeIndex < tiles[0].nodes.Length; nodeIndex++)
            // {
            //     var node = tiles[0].nodes[nodeIndex];
            //     // vertex.connectedTriangles contains triangleIndexes that are equivalent to node indexes
            //     // node.GetVertexIndex() equivalent to vertices[index] ??
            //     for (int a = 0; a < node.GetVertexCount(); a++)
            //     {
            //
            //     }
            // }

            // Important: each edge is defined by 2 vertex indexes, first the lower index, than the higher index
            var visitedEdges = new HashSet<Int2>();
            for (int vertexIndex = 0; vertexIndex < vertices.Count; vertexIndex++)
            {
                var vertex = vertices[vertexIndex];
                foreach (var other in vertex.connectedVertices)
                {
                    Int2 edge;
                    if (vertex.index > other.index)
                    {
                        edge = new Int2(other.index, vertex.index);
                    }
                    else
                    {
                        edge = new Int2(vertex.index, other.index);
                    }

                    if (visitedEdges.Contains(edge)) continue;
                    // else
                    // {
                    //     Debug.LogWarning("Found duplicated edge " + edge);
                    // }

                    // Find both Triangles sharing this edge
                    // --> check all connected triangles of both vertices
                    // --> the 2 triangles connected to both vertices are the ones we are looking for
                    var sharedTriangleNodes =
                        vertex.connectedTriangles
                            .Intersect(other.connectedTriangles)
                            .Select(triangleIndex => tiles[0].nodes[triangleIndex])
                            .ToArray();
                    Debug.Assert(sharedTriangleNodes.Length == 2, "Somehow there are " + sharedTriangleNodes.Length + " sharedTriangleIndices.");
                    // TODO optimize
                    var costMagnitude = (uint)(sharedTriangleNodes[0].position - sharedTriangleNodes[1].position).costMagnitude;
                    sharedTriangleNodes[0].AddConnection(sharedTriangleNodes[1], costMagnitude);
                    sharedTriangleNodes[1].AddConnection(sharedTriangleNodes[0], costMagnitude);
                    visitedEdges.Add(edge);

                }
            }

            // CreateNodeConnectionsTest(tiles[0].nodes);

            // TRYOUT create verts on my own
            // this.verts = new Int3[vertices.Count];
            // this.vertsInGraphSpace = new Int3[vertices.Count];
            // var offset = new Int3(forcedBoundsCenter - forcedBoundsSize / 2);
            // for (int i = 0; i < vertices.Count; i++)
            // {
            //     var vert = vertices[i].ToInt3();
            //     this.verts[i] = vert * 10.0f + offset;
            //     this.vertsInGraphSpace[i] = vert;
            // }
            //
            // // For now, each face will be resolved as 2 triangles
            // this.nodes = new TriangleMeshNode[faces.Count * 2];
            // var graphIndex = (uint)active.data.GetGraphIndex(this);
            //
            // var node = new TriangleMeshNode(active);
            // node.Walkable = true;
            // node.Tag = 0; // Not used
            // node.Penalty = initialPenalty;
            // node.GraphIndex = graphIndex;
            // // The vertices stored on the node are composed
            // // out of the triangle index and the tile index
            // node.v0 = tris[i*3+0] | tileIndex;
            // node.v1 = tris[i*3+1] | tileIndex;
            // node.v2 = tris[i*3+2] | tileIndex;
            // // This is equivalent to calling node.UpdatePositionFromVertices(), but that would require the tile to be attached to a graph, which it might not be at this stage.
            // node.position = (tile.GetVertex(node.v0) + tile.GetVertex(node.v1) + tile.GetVertex(node.v2)) * 0.333333f;
            //
            // set node connections: what triangle is the current triangle touching?

            Debug.Log("Found " + faces.Count + " Faces and " + vertices.Count + " Vertices.");
        }

        // TRYOUT create verts on my own
        // public override void GetNodes(System.Action<GraphNode> action)
        // {
        //     if (nodes == null)
        //     {
        //         Debug.Log("Planet Nav Mesh was never scanned --> skip node iteration.");
        //         return;
        //     }
        //
        //     foreach (var node in nodes)
        //     {
        //         action(node);
        //     }
        // }
        //
        // public new Int3 GetVertex(int i)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public new Int3 GetVertexInGraphSpace(int i)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public new int GetVertexArrayIndex(int index)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public new void GetTileCoordinates(int tileIndex, out int x, out int z)
        // {
        //     z = tileIndex;
        //     x = 0;
        // }

        public static string DebugConnection(TriangleMeshNode node, int first, int second)
        {
            Debug.DrawLine(((Vector3)node.GetVertex(first)), ((Vector3)node.GetVertex(second)), Color.red, 60.0f);

            return node.GetVertex(first) / 1000 + ' ' + node.GetVertex(second) / 1000;
        }

        private static void CreateNodeConnectionsTest(TriangleMeshNode[] nodes)
        {
            List<Connection> connections = ListPool<Connection>.Claim();

            var nodeRefs = ObjectPoolSimple<Dictionary<Int2, int>>.Claim();

            nodeRefs.Clear();

            // Build node neighbours
            for (int i = 0; i < nodes.Length; i++)
            {
                TriangleMeshNode node = nodes[i];

                int av = node.GetVertexCount();

                for (int a = 0; a < av; a++)
                {
                    // Recast can in some very special cases generate degenerate triangles which are simply lines
                    // In that case, duplicate keys might be added and thus an exception will be thrown
                    // It is safe to ignore the second edge though... I think (only found one case where this happens)
                    var key = new Int2(node.GetVertexIndex(a), node.GetVertexIndex((a + 1) % av));
                    if (!nodeRefs.ContainsKey(key))
                    {
                        nodeRefs.Add(key, i);
                    }
                    else
                    {
                        Debug.LogWarning("Recast edge case.");
                    }
                }
            }

            // TRYOUT
            var debugOffset1 = new Vector3(1, 1, 1);
            var debugOffset2 = new Vector3(-1, 1, 1);
            var debugOffset3 = new Vector3(1, -1, 1);

            for (int i = 0; i < nodes.Length; i++)
            {
                TriangleMeshNode node = nodes[i];

                connections.Clear();

                int av = node.GetVertexCount();

                for (int a = 0; a < av; a++)
                {
                    int first = node.GetVertexIndex(a);
                    int second = node.GetVertexIndex((a + 1) % av);
                    int connNode;

                    if (nodeRefs.TryGetValue(new Int2(second, first), out connNode))
                    {
                        TriangleMeshNode other = nodes[connNode];

                        int bv = other.GetVertexCount();

                        for (int b = 0; b < bv; b++)
                        {
                            /// <summary>TODO: This will fail on edges which are only partially shared</summary>
                            if (other.GetVertexIndex(b) == second && other.GetVertexIndex((b + 1) % bv) == first)
                            {
                                if (node.GetVertex(first) == node.GetVertex(second))
                                {
                                    Debug.DrawLine(((Vector3)node.GetVertex(first)) + debugOffset1, ((Vector3)node.GetVertex(second)) - debugOffset1, Color.magenta, 60.0f);
                                    Debug.DrawLine(((Vector3)node.GetVertex(first)) + debugOffset2, ((Vector3)node.GetVertex(second)) - debugOffset2, Color.magenta, 60.0f);
                                    Debug.DrawLine(((Vector3)node.GetVertex(first)) + debugOffset3, ((Vector3)node.GetVertex(second)) - debugOffset3, Color.magenta, 60.0f);
                                    Debug.Log("Found connection: first = " + first + " second = " + second + " a = " + a + " first pos = second pos");
                                }
                                else
                                {
                                    Debug.DrawLine(((Vector3)node.GetVertex(first)), ((Vector3)node.GetVertex(second)), Color.red, 60.0f);
                                    Debug.Log("Found connection: first = " + first + " second = " + second + " a = " + a);
                                }
                                connections.Add(new Connection(
                                    other,
                                    (uint)(node.position - other.position).costMagnitude,
                                    (byte)a
                                ));
                                break;
                            }
                        }
                    }
                }

                // TRYOUT don't override the actually gather connections - just debug them
                // node.connections = connections.ToArrayFromPool();
                // node.SetConnectivityDirty();
            }

            nodeRefs.Clear();
            ObjectPoolSimple<Dictionary<Int2, int>>.Release(ref nodeRefs);
            ListPool<Connection>.Release(ref connections);
        }

        private static Vertex GetOrCreateVertex(Dictionary<Vertex.Key, Vertex> verticesInSpace, List<Vertex> vertices,
            int x, int y, int z)
        {
            return GetOrCreateVertex(verticesInSpace, vertices, x, 0, y, 0, z, 0);
        }


        private static Vertex GetOrCreateVertex(Dictionary<Vertex.Key, Vertex> verticesInSpace, List<Vertex> vertices, int x, int xSub, int y, int ySub, int z, int zSub)
        {
            var key = new Vertex.Key(x, xSub, y, ySub, z, zSub);
            // if (verticesInSpace[x][y][z] != null)
            // {
            //     return verticesInSpace[x][y][z];
            // }
            Vertex vertex;
            if (verticesInSpace.TryGetValue(key, out vertex))
            {
                return vertex;
            }

            vertex = new Vertex(key);
            verticesInSpace.Add(key, vertex);
            // verticesInSpace[x][y][z] = vertex;
            vertices.Add(vertex);
            return vertex;
        }

        class Vertex
        {
            
            public class Key : IEquatable<Key>
            {
                // subs represent the sub section where the vertex is located between x and x+1
                // e.g. an edge is seperated into 4 sections, the subs are 0, 1, 2.
                // theoretical 3 would be 0 of x+1

               public readonly int x;
               public readonly int xSub;
               public readonly int y;
               public readonly int ySub;
               public readonly int z;
               public readonly int zSub;

                public Key(int x, int xSub, int y, int ySub, int z, int zSub)
                {
                    this.x = x;
                    this.xSub = xSub;
                    this.y = y;
                    this.ySub = ySub;
                    this.z = z;
                    this.zSub = zSub;
                }

                public bool Equals(Key other)
                {
                    if (ReferenceEquals(null, other)) return false;
                    if (ReferenceEquals(this, other)) return true;
                    return x == other.x && xSub == other.xSub && y == other.y && ySub == other.ySub && z == other.z && zSub == other.zSub;
                }

                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj)) return false;
                    if (ReferenceEquals(this, obj)) return true;
                    if (obj.GetType() != typeof(Key)) return false;
                    return Equals((Key) obj);
                }

                public override int GetHashCode()
                {
                    unchecked
                    {
                        var hashCode = x;
                        hashCode = (hashCode * 397) ^ xSub;
                        hashCode = (hashCode * 397) ^ y;
                        hashCode = (hashCode * 397) ^ ySub;
                        hashCode = (hashCode * 397) ^ z;
                        hashCode = (hashCode * 397) ^ zSub;
                        return hashCode;
                    }
                }

                public static bool operator ==(Key left, Key right)
                {
                    return Equals(left, right);
                }

                public static bool operator !=(Key left, Key right)
                {
                    return !Equals(left, right);
                }
            }

            private Key position;

            public List<Vertex> connectedVertices = new List<Vertex>();
            public int index;
            public List<int> connectedTriangles = new List<int>(6);

            public int x => position.x;
            public int y => position.y;
            public int z => position.z;

            // public Vertex(int x, int y, int z) :
            //     this(x, 0, y, 0, z, 0)
            // {
            // }

            public Vertex(Key position)
            {
                this.position = position;
            }

            // public Vertex(int x, int xSub, int y, int ySub, int z, int zSub)
            // {
            //     this.position = new Key(x, xSub, y, ySub, z, zSub);
            // }

            public void Connect(Vertex connectedVertex)
            {
                this.connectedVertices.Add(connectedVertex);
                // connectedVertex.connectedVertices.Add(this);
            }

            public int AddToTriangle(int triangleIndex)
            {
                this.connectedTriangles.Add(triangleIndex);
                return index;
            }

            public Int3 ToInt3()
            {
                return new Int3(x * Int3.Precision, y * Int3.Precision, z * Int3.Precision);
            }

            public Vector3 ToVector3()
            {
                return new Vector3(x, y, z);
            }

            public void DebugDraw(Vector3 offset, float scale)
            {
                foreach (var connectedVertex in connectedVertices)
                {
                    var primary = this.position.xSub == 0 &&
                                  this.position.ySub == 0 &&
                                  this.position.zSub == 0 &&
                                  connectedVertex.position.xSub == 0 &&
                                  connectedVertex.position.ySub == 0 &&
                                  connectedVertex.position.zSub == 0;
                    Debug.DrawLine(
                        this.ToVector3() * scale + offset,
                        connectedVertex.ToVector3() * scale + offset,
                        primary ? Color.blue : Color.green,
                        10.0f
                    );
                }
            }
        }

        class Face
        {
            public const int BOTTOM_LEFT = 0;
            public const int BOTTOM_RIGHT = 1;
            public const int TOP_RIGHT = 2;
            public const int TOP_LEFT = 3;

            /// <summary>
            /// 0 - bottom left (x = 0, y = 0)
            /// 1 - bottom right (x = 1, y = 0)
            /// 2 - top right (x = 1, y = 1)
            /// 3 - top left (x = 0, y = 1)
            /// </summary>
            public Vertex[] vertices = new Vertex[4];

            public void ConnectVertices()
            {
                vertices[BOTTOM_LEFT].Connect(vertices[BOTTOM_RIGHT]);
                vertices[BOTTOM_LEFT].Connect(vertices[TOP_LEFT]);
                vertices[TOP_RIGHT].Connect(vertices[BOTTOM_RIGHT]);
                vertices[TOP_RIGHT].Connect(vertices[TOP_LEFT]);
            }
        }
    }

}
#endif
