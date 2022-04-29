using System.Collections.Concurrent;
using Contents.UI.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace Contents.Mechanic
{
    public class AMecha : MonoBehaviour, 
        IDamagable, IAmmo, IFire, ICollision
    {
        [SerializeField]
        private long maxHP;
        [SerializeField]
        private long hp;
        [SerializeField]
        private float speed;
        [SerializeField]
        private long ammo;
        [SerializeField]
        private long maxAmmo;
        [SerializeField]
        private long totalAmmo;

        public bool isInvincible;


        //TempStatus
        public bool isHold = false;
        public bool isStun = false;
        public bool isDown = false;


        public long LookUpLimit;
        public long LookDownLimit;

        public GameObject mechaHead;
        public GameObject cameraTarget;
        public GameObject zoomCameraTarget;

        public AudioClip fireSoundClip;
        public AudioSource audioSource;
        public GameObject destroyEffect;
        public AudioClip collisionSoundClip;

        public CharacterController characterController;
        private IPossessor possessor;

        private long nearbyMechaInfoSize;
        private Collider[] nearbyMechaInfo;
        public ConcurrentBag<AItem> itemNearbyMechaInfo;
    
        public ASkill skillReload;
        public ASkill skillFire;
        public ASkill skillAdvanced;
        public ASkill skillSpecial;
        public ASkill skillUltimate;
        public ASkill skillSpecialMove;


        //Inventory
        public long inventorySize;
        public AItem[] inventory;
        public UnityEvent<InventoryEventArgs> inventoryEvent;
    
        //Temp
        private Vector3 velocity = new Vector3(0, 0, 0);

    

        protected virtual void Awake()
        {
            nearbyMechaInfoSize = 50;
            nearbyMechaInfo = new Collider[nearbyMechaInfoSize];
            itemNearbyMechaInfo = new ConcurrentBag<AItem>();

            inventory = new AItem[inventorySize];
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
        
        }

        protected virtual void Update()
        {
        
        }

        protected virtual void FixedUpdate()
        {
            #region ItemScan

            //Get Playables with item
            var size = Physics.OverlapSphereNonAlloc(
                transform.position, 
                5.0f,
                nearbyMechaInfo,
                1 << LayerMask.NameToLayer("Playables")
            );
        
            itemNearbyMechaInfo.Clear();

            for (int i = 0; i < size; i++)
            {
                var item = nearbyMechaInfo[i];

                if (item.gameObject.CompareTag("Item"))
                {
                    var aItem = item.gameObject.GetComponent<AItem>();
                
                    itemNearbyMechaInfo.Add(aItem);
                }
            }
        
            #endregion
        }

        public void Possess(IPossessor possessor)
        {
            this.possessor = possessor;
        }

        public void DisPossess()
        {
            possessor?.Dispossess();
        }

        /// <summary>
        /// Put Item. return result.
        /// </summary>
        /// <param name="item">Item to put</param>
        /// <returns>
        /// true : Success
        /// false : Failed(Inventory Max)
        /// </returns>
        public bool PutItem(AItem item)
        {
            for (var i = 0; i < inventorySize; i++)
            {
                if (inventory[i]) continue;
                item.gameObject.SetActive(false);
                inventory[i] = item;
                inventoryEvent.Invoke(new InventoryEventArgs());
                return true;
            }
        
            //Inventory NOT Available
            return false;
        }

        public AItem GetItem(long index)
        {
            var item = inventory[index];
            item.gameObject.SetActive(true);
            return item;
        }
    
        public void Damage(long damage)
        {
            if (isInvincible) return;
        
            HP -= damage;
        
            if (HP > 0) return;
            possessor?.Dispossess();
            Destroy(gameObject);
        }

        public void Repair(long amount)
        {
            HP += amount;
        }

        public void AddAmmo(long amount)
        {
            MaxAmmo += amount;
        }
    
        public void RotateHead(Vector2 lookValue)
        {
            #region Head Rotate

            var rotateVector = mechaHead.transform.eulerAngles + (new Vector3(-lookValue.y, lookValue.x, 0));

            if (rotateVector.x > LookDownLimit && rotateVector.x < 180)
            {
                rotateVector.x = LookDownLimit;
            }
            else if (rotateVector.x > 180 && rotateVector.x < LookUpLimit)
            {
                rotateVector.x = LookUpLimit;
            }

            mechaHead.transform.eulerAngles = rotateVector;

            #endregion
        }

        public void Move(Vector3 moveDirection)
        {
            velocity += Physics.gravity * (Time.deltaTime);
            if (characterController.isGrounded && velocity.y < 0) velocity.y = 0;

            var result = ((moveDirection * Speed) + velocity) * Time.deltaTime;

            characterController.Move(result);
        }

        public void MoveUpward(float amount)
        {
            velocity.y = amount;
        }

        public void Reload()
        {
            skillReload.Activate();
        }

        public void Fire()
        {
            skillFire.Activate();
        }

        public void Advance()
        {
            skillAdvanced.Activate();
        }

        public void Ultimate()
        {
            skillUltimate.Activate();
        }

        public void Special()
        {
            skillSpecial.Activate();
        }

        public void SpecialMove()
        {
            skillSpecialMove.Activate();
        }

        public void PlayFireSound()
        {
            audioSource.clip = fireSoundClip;
            audioSource.Play();
        }

        public void PlayCollisionSound()
        {
            audioSource.clip = collisionSoundClip;
            audioSource.Play();
        }

    
    
        #region Encapsulate
        public long MaxHP { get => maxHP;
            set
            {
                if(value < 0)
                {
                    maxHP = 0;
                }
                else
                {
                    maxHP = value;
                }
            }
        }
        public long HP { get => hp; set 
            { 
                if(value > MaxHP)
                {
                    hp = MaxHP;
                }
                else if(value < 0)
                {
                    hp = 0;
                }
                else
                {
                    hp = value;
                }
            }
        }
        public float Speed { get => speed; 
            set
            {
                if (value < 0)
                {
                    speed = 0;
                }
                else
                {
                    speed = value;
                }
            }
        }

        public long Ammo
        {
            get => ammo;
            set
            {
                if (value > MaxAmmo)
                {
                    ammo = MaxAmmo;
                }
                else if (value < 0)
                {
                    ammo = 0;
                }
                else
                {
                    ammo = value;
                }
            }
        }
        public long MaxAmmo
        {
            get => maxAmmo;
            set
            {
                if (value < 0)
                {
                    maxAmmo = 0;
                }
                else
                {
                    maxAmmo = value;
                }
            }
        }
        public long TotalAmmo
        {
            get => totalAmmo;
            set
            {
                if (value < 0)
                {
                    totalAmmo = 0;
                }
                else
                {
                    totalAmmo = value;
                }
            }
        }
        #endregion
    }
}
