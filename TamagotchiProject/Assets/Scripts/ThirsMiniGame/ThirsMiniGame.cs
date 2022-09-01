using System.Collections;
using UI;
using UnityEngine;


namespace ThirsMiniGame
{
    public class ThirsMiniGame : MonoBehaviour
    {
        [SerializeField] private GameObject thirsSpawnerPrefab;
        [SerializeField] private GameObject pieMenuGameObject;

        private PieMenu pieMenu;


        private void Start()
        {
            pieMenu = pieMenuGameObject.GetComponent<PieMenu>();

        }

        public void StartThirsMiniGame()
        {
            Thirs.GhostCounter = 5;

            Instantiate(thirsSpawnerPrefab, transform);
        }

        public void StopThirsMiniGame()
        {
            pieMenu.DisableActionMode();

        }




    }
}

