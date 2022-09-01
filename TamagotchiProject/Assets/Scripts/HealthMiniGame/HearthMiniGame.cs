using System.Collections;
using UI;
using UnityEngine;


namespace HearthMiniGame
{
    public class HearthMiniGame : MonoBehaviour
    {
        [SerializeField] private GameObject hearthSpawnerPrefab;
        [SerializeField] private GameObject pieMenuGameObject;

        private PieMenu pieMenu;


        private void Start()
        {
            pieMenu = pieMenuGameObject.GetComponent<PieMenu>();

        }

        public void StartHearthMiniGame()
        {
            Hearth.GhostCounter = 5;

            Instantiate(hearthSpawnerPrefab, transform);
        }

        public void StopHearthMiniGame()
        {
            pieMenu.DisableActionMode();

        }




    }
}

