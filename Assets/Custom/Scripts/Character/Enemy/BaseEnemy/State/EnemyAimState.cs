using System.Collections;
using UnityEngine;


public class EnemyAimState : EnemyBaseState {

	public override void Do() {
		movementController.RotateTowards(targetTransform.position - transform.position);
	}
}
