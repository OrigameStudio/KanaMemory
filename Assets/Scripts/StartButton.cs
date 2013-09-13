
using UnityEngine;
using System.Collections;


public class StartButton : SplashScreenCollapsable{

	public GameType gameType;

	private bool wasHit = false;
	private bool isCollapsed = false;

	public override void Hit(Vector3? hitPoint){

		MemoryGame game = MemoryGame.GetInstance();

		if(!this.isCollapsed){

			if(game != null){

				game.type = this.gameType;
			}

			this.sound.Play();

			this.wasHit = true;
		}
	}

	public override void Collapse(Vector3? hitPoint, float explosionForce, float explosionRadius){

		this.isCollapsed = true;

		this.IsKinematic(this.gameObject, false);

		if(this.wasHit){

			this.UseGravity(this.gameObject, true);
		}

		if(this.wasHit && hitPoint.HasValue && explosionForce > 0){

			this.Explode(this.gameObject, hitPoint.Value, explosionForce, explosionRadius);
		}

		Object.Destroy(this.gameObject.animation);
		//Object.Destroy(this.gameObject.collider);
		Object.Destroy(this);
	}
}
