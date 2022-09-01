﻿using UI;
using UnityEngine;

namespace AmusemenMiniGame
{
    public class AmusemenMiniGame : MonoBehaviour
    {
        [SerializeField] private GameObject fruitSpawnerPrefab;
        [SerializeField] private GameObject bladePrefab;
        [SerializeField] private GameObject pieMenuGameObject;

        private PieMenu pieMenu;
        private GameObject bladeGameObject;

        private void Start()
        {
            pieMenu = pieMenuGameObject.GetComponent<PieMenu>();
        }

        public void StartAmusemenMiniGame()
        {
            bladeGameObject = Instantiate(bladePrefab, transform);
            Instantiate(fruitSpawnerPrefab, transform);
        }

        public void StopAmusemenMiniGame()
        {
            Destroy(bladeGameObject);
            pieMenu.DisableActionMode();
        }
    }
}