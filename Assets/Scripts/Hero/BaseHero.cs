using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public abstract class BaseHero : DragHandler, IMerge, IMoney, IRecources, IDamage, IDeath
    {
        [field: SerializeField] public ModelHero Model { get; private set; }
        [field: SerializeField] public SpriteRenderer Render { get; private set; }
        [field: SerializeField] public Transform _health { get; private set; }
        [field: SerializeField] public TextMeshProUGUI _textHelth {  get; private set; }
        
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerManager layerManager;

        private int countclick = 0;

        public int MaxIndex;
        public int CurrentIndex;
        public bool IsBattle = false;
        public event IMerge.CallBackMerge OnMerge;
        public event IMoney.CallbackMoney OnMoneyChange;
        public event IMoney.CallbackUpMoney OnMoneyUp;
        public event IRecources.CallBackRes OnAddRes;
        public event IDeath.CallBackDeath OnDeath;
        public Animator Animator => _animator;

        public void Init(ModelHero model, LayerManager manager = null)
        {
            layerManager = manager;
            Model = model;
        }
        public override void OnMouseDrag()
        {
            if (IsBattle)
            {
                return;
            }
            base.OnMouseDrag();
            Vector3 vector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            vector.z = layerManager.SetLayer(transform.position.y) + Random.Range(0.0001f, 0.1111f);
            transform.position = vector;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != _collider2D && colliders[i].TryGetComponent<BaseHero>(out BaseHero baseHero))
                {
                    if (transform.position.y > baseHero.transform.position.y)
                    {
                        baseHero.Render.sortingOrder = 2;
                        Render.sortingOrder = 1;
                    }
                    else
                    {
                        baseHero.Render.sortingOrder = 1;
                        Render.sortingOrder = 2;
                    }
                }
            }
        }
        public override void OnStartDrag()
        {
            if (IsBattle)
            {
                return;
            }
            base.OnStartDrag();
            int randomRes = Random.Range(0, 6);
            Debug.Log($"{randomRes}");
            if (randomRes == 5)
            {
                OnAddRes.Invoke(Model.GetRes, transform);              
            }
            OnMoneyChange.Invoke(Model.GetMoney);
            OnMoneyUp.Invoke();
            _animator.SetTrigger("IsAttack");
            countclick++;
            if (countclick == 5)
            {
                AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.ClickHero[Random.Range(0, AudioManager.Instance.ClickHero.Length - 1 + 1)]);
                countclick = 0;
            }
        }

        public override void OnEndDrag()
        {
            if (IsBattle)
            {
                return;
            }
            base.OnEndDrag();
            if (CurrentIndex >= MaxIndex)
            {
                return;
            }
            BaseHero baseHero = SearchUnit();
            if (baseHero != null && baseHero.GetType() == GetType())
            {
                OnMerge.Invoke(baseHero, this);
            }
        }

        private BaseHero SearchUnit()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 4);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != _collider2D && colliders[i].TryGetComponent<BaseHero>(out BaseHero baseHero))
                {
                    return baseHero;
                }
            }
            return null;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2);
        }

        public void Damage(float damage)
        {
            ModelHero modelHero = Model;
            modelHero.Heath -= damage;
            modelHero.Heath = Mathf.Clamp(modelHero.Heath, 0f, float.MaxValue);
            Model = modelHero;
            _textHelth.SetText($"{Model.Heath}");
            if (Model.Heath <= 0f)
            {
                OnDeath?.Invoke(this);
                _animator.SetTrigger("IsDie");
            }
        }
    }
}
