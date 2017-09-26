/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
        private TrackableBehaviour mTrackableBehaviour;
        #endregion // PRIVATE_MEMBER_VARIABLES

        #region PUBLIC_MEMBER_VARIABLES
        //public GameObject canvasUI;
        #endregion // PUBLIC_MEMBER_VARIABLES

        private bool m_isTele = false;
        private bool m_isPull = false;
        private bool m_isPillar = false;

        #region UNTIY_MONOBEHAVIOUR_METHODS   
        void Start()
        {
            //canvasUI.SetActive(false);

            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }
        #endregion // UNTIY_MONOBEHAVIOUR_METHODS

        #region PUBLIC_METHODS
        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            Telekinesis telekinesis = (Telekinesis)GameObject.FindObjectOfType(typeof(Telekinesis));
            ForcePull forcePull = (ForcePull)GameObject.FindObjectOfType(typeof(ForcePull));
            CreatePillar createPillar = (CreatePillar)GameObject.FindObjectOfType(typeof(CreatePillar));


            foreach (Renderer component in rendererComponents)
            {
                if(component.tag == "Joker")
                {
                    telekinesis.FoundSprite();
                    m_isTele = true;
                }
                else if(component.tag == "Ace")
                {
                    forcePull.FoundSprite();
                    m_isPull = true;
                }
                else if (component.tag == "Queen")
                {
                    createPillar.FoundSprite();
                    m_isPillar = true;
                }
                else if (component.tag == "Back")
                {
                    createPillar.DestroyPillar();
                    m_isPillar = true;
                }
            }

            //if (telekinesis)
            //{
            //    telekinesis.FoundSprite();
            //}
            //if (forcePull)
            //{
            //    forcePull.FoundSprite();
            //}

            // Enable rendering:
            //foreach (Renderer component in rendererComponents)
            //{
            //    component.enabled = true;
            //}

            
            if(mTrackableBehaviour)
            {
                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            }
                
        }

        private void OnTrackingLost()
        {
            Telekinesis telekinesis = (Telekinesis)GameObject.FindObjectOfType(typeof(Telekinesis));
            ForcePull forcePull = (ForcePull)GameObject.FindObjectOfType(typeof(ForcePull));
            CreatePillar createPillar = (CreatePillar)GameObject.FindObjectOfType(typeof(CreatePillar));
            //canvasUI.SetActive(false);

            if (m_isTele)
            {
                telekinesis.LostSprite();
                m_isTele = false;
            }
            else if (m_isPull)
            {
                forcePull.LostSprite();
                m_isPull = false;
            }
            else if (m_isPillar)
            {
                createPillar.LostSprite();
                m_isPillar = false;
            }

            //if (telekinesis)
            //{
            //    telekinesis.LostSprite();
            //}
            // if (forcePull)
            //{
            //    forcePull.LostSprite();
            //}

            HideObjects();
        }

        private void HideObjects()
        { 
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }
            if (mTrackableBehaviour)
            {
                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            }              
        }
        #endregion // PRIVATE_METHODS
    }
}
