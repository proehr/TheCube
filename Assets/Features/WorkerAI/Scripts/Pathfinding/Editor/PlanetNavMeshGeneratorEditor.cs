#if PATHFINDING
using System;
using Pathfinding;
using UnityEditor;
using UnityEngine;

namespace Features.WorkerAI.Scripts.Pathfinding
{
    [CustomGraphEditor(typeof(PlanetNavMeshGraph), "Planet Navmesh Graph")]
    public class PlanetNavMeshGraphEditor : GraphEditor
    {
        public override void OnInspectorGUI(NavGraph target)
        {
            if (!(target is PlanetNavMeshGraph graph)) return;

            // graph.planetCubes = ObjectField("Source Mesh", graph.planetCubes, typeof(PlanetCubes_SO), false, false) as PlanetCubes_SO;

            graph.verticesPerEdge =
                Math.Max(2, EditorGUILayout.IntField(
                    new GUIContent(
                        "Vertices per Edge",
                        "Into how many sections will a cube edge be seperated?"),
                    graph.verticesPerEdge));
            EditorGUILayout.LabelField("Triangles per Face: " +
                                       Mathf.RoundToInt((float) (2 * Math.Pow(graph.verticesPerEdge - 1, 2))));
            EditorGUILayout.HelpBox("Number of resulting triangles per face is: 2 * (n - 1) ^ 2\n" +
                                    "This impacts the generation performance accordingly.\n" +
                                    "Higher values --> worse performance, but smoother movement.", MessageType.Info);
            GUILayout.BeginHorizontal();
            GUILayout.Space(18);
            graph.showMeshSurface = GUILayout.Toggle(graph.showMeshSurface,
                new GUIContent("Show surface", "Toggles gizmos for drawing the surface of the mesh"),
                EditorStyles.miniButtonLeft);
            graph.showMeshOutline = GUILayout.Toggle(graph.showMeshOutline,
                new GUIContent("Show outline", "Toggles gizmos for drawing an outline of the nodes"),
                EditorStyles.miniButtonMid);
            graph.showNodeConnections = GUILayout.Toggle(graph.showNodeConnections,
                new GUIContent("Show connections", "Toggles gizmos for drawing node connections"),
                EditorStyles.miniButtonRight);
            GUILayout.EndHorizontal();
        }
    }
}
#endif
