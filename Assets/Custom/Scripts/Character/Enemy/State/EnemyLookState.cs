using System.Collections;
using UnityEngine;


public class EnemyLookState : EnemyStateBase {
	public override void Do() {
		movementController.RotateTowards(targetTransform.position - transform.position);
	}
}
