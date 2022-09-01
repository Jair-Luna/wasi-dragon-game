using System.Collections;
using Controllers;
using UnityEngine;
using World;


namespace ThirsMiniGame
{
    public class Thirs : MonoBehaviour
    {
        public static int GhostCounter = 5;

        [SerializeField] private ParticleSystem ghostTrail;
        [SerializeField] private ParticleSystem ghostRemains;
        [SerializeField] private GameObject nightBonusPrefab;
        [SerializeField] private GameObject minus10ThirstPrefab;
        [SerializeField] private GameObject plus10ThirstPrefab;
        [SerializeField] private GameObject plus20ThirstPrefab;

        private ThirsMiniGame thirsMiniGame;
        private StatsController statsController;
        private Transform targetTransform;
        private Horizon horizon;

        private CircleCollider2D ghostCollider;
        private Animator ghostAnimator;

        private const float Speed = 1.5f;

        private void Start()
        {
            var targetGameObject = GameObject.FindWithTag("Player");
            thirsMiniGame = GameObject.FindWithTag("ThirsMiniGame").GetComponent<ThirsMiniGame>();
            statsController = GameObject.FindWithTag("Stats").GetComponent<StatsController>();
            horizon = GameObject.FindWithTag("Horizon").GetComponent<Horizon>();
            targetTransform = targetGameObject.GetComponent<Transform>();
            ghostCollider = GetComponent<CircleCollider2D>();
            ghostAnimator = transform.GetChild(0).GetComponent<Animator>();
            ghostTrail.Play();
        }

        private void FixedUpdate()
        {
            FollowPlayer();
            FacePlayer();
        }

        private void FollowPlayer()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, targetTransform.position, Speed * Time.deltaTime);
        }

        private void FacePlayer()
        {
            var offset = targetTransform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                LoseThirst();
                ghostAnimator.SetTrigger("GhostAttack");
                StartCoroutine(DestroyGhost());
            }
        }

        private void OnMouseDown()
        {
            if (!GameController.GamePaused)
            {
                GainThirst();
                ghostAnimator.SetTrigger("GhostDeath");
                StartCoroutine(DestroyGhost());
                ghostRemains.Emit(Random.Range(5, 11));
            }
        }

        private void GainThirst()
        {
            var position = transform.position;

            if (horizon.IsNight())
            {
                statsController.ChangeStats(StatsController.Stats.Thirst, 20);
                TextPopup(plus20ThirstPrefab, position);
                StartCoroutine(ShowNightBonus(position));
            }
            else
            {
                statsController.ChangeStats(StatsController.Stats.Thirst, 10);
                TextPopup(plus10ThirstPrefab, position);
            }
        }

        private void LoseThirst()
        {
            statsController.ChangeStats(StatsController.Stats.Thirst, -10);
            TextPopup(minus10ThirstPrefab, transform.position);
        }

        private IEnumerator ShowNightBonus(Vector3 position)
        {
            yield return new WaitForSeconds(0.75f);
            TextPopup(nightBonusPrefab, position);
        }

        private IEnumerator DestroyGhost()
        {
            ghostTrail.Stop();
            ghostCollider.enabled = false;
            yield return new WaitForSeconds(1f);

            GhostCounter--;

            if (GhostCounter == 0)
            {
                thirsMiniGame.StopThirsMiniGame();
            }
            Destroy(gameObject);
        }

        private void TextPopup(GameObject textPrefab, Vector3 position)
        {
            GameObject points = Instantiate(textPrefab, position, Quaternion.identity);
            Destroy(points, 3f);
        }
    }
}

