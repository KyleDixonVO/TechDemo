using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



    public class AdvancedMovement : MonoBehaviour
    {
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerController;
        private TMP_Text dashCounter;
        public float dashCooldown = 2.0f;
        private float countdown;
        public AudioSource dash;
        // Start is called before the first frame update
        void Start()
        {
            playerController = this.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            dashCounter = GameObject.Find("DashCounter").GetComponent<TMP_Text>();
            playerController.m_DashMagnitude = 300;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && playerController.m_currentDashes > 0)
            {
                playerController.m_isDashing = true;
                countdown = Time.time;
                dash.Play();
            }

            if (playerController.m_currentDashes < playerController.m_maxDashes && (Time.time >= countdown + dashCooldown))
            {
                playerController.m_currentDashes++;
                countdown = Time.time;
            }

            dashCounter.text = ("Press Control to dash. " +
            "Dashes Available: " + playerController.m_currentDashes);
        }

        void FixedUpdate()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                Physics.autoSyncTransforms = true;
                playerController.m_UseHeadBob = false;
                playerController.m_OnMovingPlatform = true;
                this.gameObject.transform.parent = other.gameObject.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                this.gameObject.transform.parent = null;
                playerController.m_UseHeadBob = true;
                playerController.m_OnMovingPlatform = false;
                Physics.autoSyncTransforms = false;
            } 
        }
    }


