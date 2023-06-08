using System.Collections.Generic;
using System.Threading.Tasks;
using AIBattle.Controllers;
using AIBattle.Controllers.BotsPlayers.PointDefenders;
using AIBattle.Controllers.Players;
using AIBattle.Interfaces;
using AIBattle.LiveObjects;
using AIBattle.LiveObjects.LiveComponents.Targets;
using AIBattle.Worlds;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AIBattle
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Selection _selection;

        [SerializeField] private VictoryPoint _victoryPoint;
        [SerializeField] private GameObject _winObject;
        [SerializeField] private GameObject _loseObject;

        [SerializeField] private TargetId _playerId;
        [SerializeField] private PointDefenderParameters _enemyParameters;

        [SerializeField] private LiveObjectSpawnData[] _alliesSpawns;
        [SerializeField] private LiveObjectSpawnData[] _enemiesSpawns;

        private UnityAlternatives.Input _input = new();
        private UnityAlternatives.Time _time = new();

        private Controller _player;
        private Controller _enemies;

        private void Start()
        {
            _victoryPoint.OnTargetWon.AddListener((l) => Win());

            CreateLiveObjects(out List<LiveObject> allies, _alliesSpawns);
            CreateLiveObjects(out List<LiveObject> enemies, _enemiesSpawns);

            InitializeControllers(allies, enemies);

            UnityAlternatives.Input.OnEscDown += Application.Quit;
        }

        private void InitializeControllers(List<LiveObject> allies, List<LiveObject> enemies)
        {
            _enemyParameters.LiveObjects = enemies;

            _enemies = new PointDefender(_enemyParameters);
            _enemies.OnAllLiveObjectDestroyed.AddListener(Win);

            _player = new Player(allies, _playerId, _selection);
            _player.OnAllLiveObjectDestroyed.AddListener(Lose);
        }

        private void CreateLiveObjects(out List<LiveObject> liveObjects, LiveObjectSpawnData[] data)
        {
            liveObjects = new(data.Length);

            for (int i = 0; i < data.Length; i++)
                liveObjects.Add(data[i].Data.Create(data[i].Parent, data[i].Position, data[i].Rotation));
        }

        private void Update()
        {
            _time.Update();
            _input.Update();
        }

        private void OnApplicationPause(bool pause)
        {
            _time.Stop();
        }

        private void Lose()
        {
            Debug.Log("Player lose");
            _loseObject.SetActive(true);
            RestartAsync();
        }

        private void Win()
        {
            Debug.Log("Player won!");
            _winObject.SetActive(true);
            RestartAsync();
        }

        private async void RestartAsync()
        {
            int DelayInMilliseconds = 1000;
            await Task.Delay(DelayInMilliseconds);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
