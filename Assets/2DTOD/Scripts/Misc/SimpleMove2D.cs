using UnityEngine;
using System.Collections;

public class SimpleMove2D : MonoBehaviour {

	private Animator animator;
	public float speed;

	void Start() {
		animator = GetComponent<Animator>();
	}

	void Update() {
		if(Input.GetKey(KeyCode.A)) {
			transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
			animator.Play( Animator.StringToHash( "Base Layer.Walking" ) );
		}
		else if(Input.GetKey(KeyCode.D)) {
			transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
			animator.Play( Animator.StringToHash( "Base Layer.Walking" ) );
		}
		else {
			animator.Play( Animator.StringToHash( "Base Layer.Idle" ) );
		}
	}
}
