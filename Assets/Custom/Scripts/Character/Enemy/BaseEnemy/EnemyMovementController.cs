using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovementController : MovementController {
	[Header("Pathing")]
	[SerializeField] private NavMeshPath path;
	[SerializeField] private Transform target;
	[SerializeField] private Transform playerTransform;
	[SerializeField] private float waypointDistanceThreshold;
	[SerializeField] public bool hasPath = false;
	[SerializeField] public bool hasFinishedPath = false;

	private int currentWaypoint = 0;

	public override void Start() {
		if (playerTransform == null) {
			playerTransform = PlayerController.Instance.transform;
		}
		if (target == null) {
			target = playerTransform;
		}
		path = new NavMeshPath();
		CalculatePathToPlayer();
		base.Start();
	}

	public bool CalculatePathToTransform(Vector3 targetPosition) {
		if (NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path)) {
			currentWaypoint = 0;
			hasPath = path.corners.Length > 0;
			hasFinishedPath = !hasPath;
		}
		else {
			hasPath = false;
		}
		return hasPath;
	}

	public bool CalculatePathToPlayer() {
		return CalculatePathToTransform(playerTransform.position);
	}

	public void MoveAlongPath() {
		Vector3 targetWaypoint = path.corners[currentWaypoint];
		Vector3 direction = (targetWaypoint - transform.position).normalized;
		Vector2 flatDirection = new Vector2(direction.x, direction.z);
		float distance = Vector3.Distance(transform.position, targetWaypoint);
		MoveCharacter(flatDirection);
		if (distance < waypointDistanceThreshold) {
			currentWaypoint++;
		}
		if (currentWaypoint == path.corners.Length) {
			hasFinishedPath = true;
		}
	}
}
