﻿using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour
{
	public float speed = 10;
	public int damage;
	public GameObject target;
	public Vector3 startPosition;
	public Vector3 targetPosition;

	private float distace;
	private float startTime;

	private GameManagerBehavior gameManager;

	void Start ()
	{
		startTime = Time.time;
		distace = Vector3.Distance( startPosition, targetPosition );
		GameObject gm = GameObject.Find( "GameManager" );
		gameManager = gm.GetComponent<GameManagerBehavior>();
	}
	
	void Update ()
	{
		float timeInterval = Time.time - startTime;
		gameObject.transform.position = Vector3.Lerp( startPosition, targetPosition, timeInterval * speed / distace );

		if (gameObject.transform.position.Equals( targetPosition ))
		{
			if(target != null)
			{
				Transform healthBarTransform = target.transform.FindChild( "HealthBar" );
				HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
				healthBar.currentHealth -= Mathf.Max( damage, 0 );
				if(healthBar.currentHealth <= 0)
				{
					Destroy( target );
					AudioSource audioSource = target.GetComponent<AudioSource>();
					AudioSource.PlayClipAtPoint( audioSource.clip, transform.position );

					gameManager.Gold += 50;
				}
			}
			Destroy( gameObject );
		}
	}
}
