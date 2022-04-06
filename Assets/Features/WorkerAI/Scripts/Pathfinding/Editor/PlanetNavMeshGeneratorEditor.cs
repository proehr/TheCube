using Pathfinding;
using UnityEditor;
using UnityEngine;

namespace Features.WorkerAI.Scripts.Pathfinding {
	[CustomGraphEditor(typeof(PlanetNavMeshGraph), "Planet Navmesh Graph")]
	public class PlanetNavMeshGraphEditor : GraphEditor {
		public override void OnInspectorGUI (NavGraph target) {

			if (!(target is PlanetNavMeshGraph graph)) return;

			// graph.planetCubes = ObjectField("Source Mesh", graph.planetCubes, typeof(PlanetCubes_SO), false, false) as PlanetCubes_SO;

			GUILayout.BeginHorizontal();
			GUILayout.Space(18);
			graph.showMeshSurface = GUILayout.Toggle(graph.showMeshSurface, new GUIContent("Show surface", "Toggles gizmos for drawing the surface of the mesh"), EditorStyles.miniButtonLeft);
			graph.showMeshOutline = GUILayout.Toggle(graph.showMeshOutline, new GUIContent("Show outline", "Toggles gizmos for drawing an outline of the nodes"), EditorStyles.miniButtonMid);
			graph.showNodeConnections = GUILayout.Toggle(graph.showNodeConnections, new GUIContent("Show connections", "Toggles gizmos for drawing node connections"), EditorStyles.miniButtonRight);
			GUILayout.EndHorizontal();
		}
	}
}
