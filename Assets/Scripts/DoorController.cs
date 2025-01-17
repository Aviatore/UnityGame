using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class DoorController : MonoBehaviour
    {
        public string compatibleChipName;
        public int requiredCompatibleChipNameCount;
        public bool isGameOverChip;
        public bool isOpened;
        public Material greenLight;
        public Material redLight;
        public GameObject door;
        public Transform doorPlatform;
        

        private Animation _doorAnimation;

        private void Start()
        {
            _doorAnimation = door.GetComponent<Animation>();
            Player.OnPlatformEnter += OpenDoor;
        }

        public void OpenDoor(object sender, OnPlatformEnterArgs args)
        {
            if (args.ChipName.Equals(compatibleChipName) && args.ChipNumber >= requiredCompatibleChipNameCount)
            {
                Debug.Log($"Chip name: {compatibleChipName} count: {DataStore.GetItemQuantityFromInventory(compatibleChipName).ToString()}");
                var sphere = doorPlatform.GetChild(0);
                var spotLight = doorPlatform.GetChild(1);

                sphere.GetComponent<Renderer>().material = greenLight;

                spotLight.GetComponent<Light>().color = Color.green;

                _doorAnimation.Play("open");
                FindObjectOfType<AudioManager>().PlaySound("DoorOpen");
                isOpened = true;

                if (args.ChipName.Equals("Chip2"))
                {
                    if (SceneManager.sceneCountInBuildSettings - 1 == SceneManager.GetActiveScene().buildIndex)
                    {
                        DataStore.IsWonGameOver = true;
                    }
                    else
                    {
                        DataStore.IsLevelOver = true;

                        // if (SceneManager.GetActiveScene().buildIndex == 1)
                        // {
                        //     DataStore.RemoveItemsFromInventory("Chip2", requiredCompatibleChipNameCount);
                        // }
                    }
                }
            }
        }
    }
}