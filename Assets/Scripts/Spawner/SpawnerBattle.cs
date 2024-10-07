using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using System.Linq;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Game
{
    public class SpawnerBattle : MonoBehaviour
    {
        [SerializeField] private Transform[] SpawnPosHero;
        [SerializeField] private Transform[] SpawnPosEnemy;

        private WindowSelectHero _windowSelectHero;
        private WindowAncoument _windowAncoument;
        private FillingEnemies _fillingEnemies;
        private List<Target> _targets = new List<Target>();
        public List<BaseHero> _hero = new List<BaseHero>();
        public List<BaseHero> _enemy = new List<BaseHero>();
        private Coroutine _coroutine;

        [Inject]
        public void Construct(WindowSelectHero selectHero, FillingEnemies filling, WindowAncoument ancoument)
        {
            _windowSelectHero = selectHero;
            _fillingEnemies = filling;
            _windowAncoument = ancoument;
        }
        public void StartBattle()
        {
            SpawmHero();
            SpawnEnemy();
            SetTargetEnemy();
            SetTargetHero();
            _coroutine = StartCoroutine(Battle());
        }
        public void SpawmHero()
        {
            for (int i = 0; i < _windowSelectHero._selectedHero.Count; i++)
            {
                BaseHero baseHero = Instantiate(_windowSelectHero._selectedHero[i].Prefab, SpawnPosHero[i]);
                baseHero.Init(_windowSelectHero._selectedHero[i]);
                baseHero.IsBattle = true;
                baseHero._textHelth.SetText($"{baseHero.Model.Heath}");
                baseHero._health.gameObject.SetActive(true);
                baseHero.Render.sortingOrder = i;
                baseHero.OnDeath += OnDeath;
                _hero.Add(baseHero);
            }
        }
        public void SpawnEnemy()
        {
            for (int i = 0; i < _fillingEnemies._baseEnemy.Count; i++)
            {
                BaseHero baseEnemy = Instantiate(_fillingEnemies._baseEnemy[i].Prefab, SpawnPosEnemy[i]);
                baseEnemy.Init(_fillingEnemies._baseEnemy[i]);
                baseEnemy.IsBattle = true;
                baseEnemy._textHelth.SetText($"{baseEnemy.Model.Heath}");
                baseEnemy._health.gameObject.SetActive(true);
                baseEnemy._health.transform.localRotation = Quaternion.Euler(baseEnemy._textHelth.transform.localRotation.x, -180, baseEnemy._textHelth.transform.localRotation.z);
                baseEnemy._textHelth.transform.localScale = new Vector3(-1,1,1);
                baseEnemy.Render.flipX = true;
                baseEnemy.Render.sortingOrder = i;
                baseEnemy.OnDeath += OnDeath;
                _enemy.Add(baseEnemy);
            }
        }

        public void SetTargetHero()
        {
            List<BaseHero> temp = new List<BaseHero>(_enemy);
            for (int i = _hero.Count - 1; i >= 0; i--)
            {
                Target _target = new Target();
                _target.OffsetX = -1;
                _target.DefaultPos = SpawnPosHero[i];
                _target.Unit1 = _hero[i];
                if (temp.Count > 0)
                {
                    _target.Unit2 = temp.FindLast(hero => true);
                    temp.Remove(_target.Unit2);
                }
                else
                {
                    BaseHero tempHero = _enemy[0];
                    for (int j = 1; j < _enemy.Count; j++)
                    {
                        if (_enemy[j].Model.Heath >= tempHero.Model.Heath)
                        {
                            _target.Unit2 = _enemy[j];
                        }
                    }
                    _target.Unit2 = _target.Unit2 == null ? tempHero : _target.Unit2;
                }
                _targets.Add(_target);
            }
        }
        public void SetTargetEnemy()
        {
            List<BaseHero> temp = new List<BaseHero>(_hero);
            for (int i = _enemy.Count - 1; i >= 0; i--)
            {
                Target _target = new Target();
                _target.OffsetX = 1;
                _target.DefaultPos = SpawnPosEnemy[i];
                _target.Unit1 = _enemy[i];
                if (temp.Count > 0)
                {
                    _target.Unit2 = temp.FindLast(hero => true);
                    temp.Remove(_target.Unit2);
                }
                else
                {
                    BaseHero tempHero = _hero[0];
                    for (int j = 1; j < _hero.Count; j++)
                    {
                        if (_hero[j].Model.Heath >= tempHero.Model.Heath)
                        {
                            _target.Unit2 = _hero[j];
                        }
                    }
                    _target.Unit2 = _target.Unit2 == null ? tempHero : _target.Unit2;
                }
                _targets.Add(_target);
            }
        }

        public IEnumerator Battle()
        {
            for (int i = _targets.Count - 1; i >= 0; i--)
            {
                yield return new WaitForSeconds(0.5f);
                Vector3 pos = _targets[i].Unit2.transform.position;
                pos.x += _targets[i].OffsetX;
                _targets[i].Unit1.transform.DOMove(pos, 0.6f);
                yield return new WaitForSeconds(1f);
                _targets[i].Unit1.Animator.SetTrigger("IsAttack");
                AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Fight[Random.Range(0, AudioManager.Instance.Fight.Length - 1 + 1)]);
                yield return new WaitForSeconds(0.5f);
                _targets[i].Unit2.Damage(_targets[i].Unit1.Model.Damage);
                yield return new WaitForSeconds(0.5f);
                if (i < _targets.Count && _targets[i].Unit1 != null)
                {
                    _targets[i].Unit1.transform.DOMove(_targets[i].DefaultPos.position, 0.6f);
                }
            }
            if (_hero.Count > 0 && _enemy.Count > 0)
            {
                RecalculateTarget();
                StopCoroutine(_coroutine);
                _coroutine = StartCoroutine(Battle());
            }
        }
        private void OnDeath(BaseHero hero)
        {
            if (_hero.Contains(hero))
            {
                _hero.Remove(hero);
                hero.OnDeath -= OnDeath;
                Destroy(hero.gameObject, 1f);
                RecalculateTarget();
                return;
            }
            if (_enemy.Contains(hero))
            {
                _enemy.Remove(hero);
                hero.OnDeath -= OnDeath;
                Destroy(hero.gameObject, 1f);
                RecalculateTarget();
            }
        }
        private void RecalculateTarget()
        {
            if (_hero.Count <= 0)
            {
                _windowAncoument.ShowLose();
                StopCoroutine(_coroutine);
                return;
            }
            if (_enemy.Count <= 0)
            {
                _windowAncoument.ShowWin();
                StopCoroutine(_coroutine);
                return;
            }
            _targets.Clear();
            SetTargetEnemy();
            SetTargetHero();
        }
    }
    [Serializable]
    public struct Target
    {
        public Transform DefaultPos;
        public BaseHero Unit1;
        public BaseHero Unit2;
        public float OffsetX;
    }
}
