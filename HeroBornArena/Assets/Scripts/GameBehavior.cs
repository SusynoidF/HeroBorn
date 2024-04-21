using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;
public class GameBehavior : MonoBehaviour, Imanager
{  public delegate void DebugDelegate(string newText);

   
    public bool paused = false;
    public GameObject pause;
   public GameObject winscreen;
   public GameObject lossscreen;
   public DebugDelegate debug = Print;
   public Stack<string> lootStack = new Stack<string>();
   private string _state;

   public string State
   {
      get {return _state;}
      set {State = value;}
   }
     public string labelText = "Collect All Items and Win";
     public int maxItems = 5;
     public bool showWinScreen = false;
     public bool showLossScreen = false;
     private int _itemsCollected = 0;
     public int Items
     {
         get { return _itemsCollected; }
         set {
             _itemsCollected = value;
             // 2
             if(_itemsCollected >= maxItems)
             {
                 labelText = "You've found all the items!";
                 showWinScreen = true;
                 Time.timeScale = 0f;
                 winscreen.SetActive(true);
             }
             else
             {
                 labelText = "Item found, only " + (maxItems - _itemsCollected) +" more to go!";

            }
         }
     }
      private int _playerHP = 3;
     public int HP
     {
         get { return _playerHP; }
         set {
             _playerHP = value;
             if(_playerHP <= 0)
             {
                labelText = "Oof, thats rough buddy";
                showLossScreen = true;
                Time.timeScale = 0;
                lossscreen.SetActive(true);
             }
             else
             {
                labelText = "HealthAffected";
             }
             Debug.LogFormat("Lives: {0}", _playerHP);
         }
     }

     void RestartLevel()
     {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
        lossscreen.SetActive(false);
        winscreen.SetActive(false);
       
     }
      void OnGUI()
     { GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" +
             _playerHP);
        
         GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " +
            _itemsCollected);
         
         GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height /
            50, 300, 50), labelText);
             if (showWinScreen)
           {
             // 4
             if (GUI.Button(new Rect(Screen.width/2 - 100,
                Screen.height/2 - 50, 200, 100), "Restart?"))
             {
               Utilities.RestartLevel(0);
             }
            }

             if (showLossScreen)
             {
                if (GUI.Button(new Rect(Screen.width / 2 - 100,
                Screen.height / 2 - 50, 200, 100), "Unfortunate..."))
                { try
                 {
                     Utilities.RestartLevel(-1);
                     debug("Level restarted successfully...");
                 }
                  // 2
                 catch (System.ArgumentException e)
                 {
                                                 
                   Utilities.RestartLevel(0);
                     debug("Reverting to scene 0: " +
                        e.ToString());
                 }
                                        
                 finally
                      {
                        debug("Restart handled...");
                      }
                }
             }
        }
        void Start()
        {
         Initialize();
         InventoryList<string> inventoryList = new InventoryList<string>();

         inventoryList.SetItem("Potion");
         Debug.Log(inventoryList.item);
        }
        public void Initialize()
        {
         _state = "Manager initailized..";
         _state.FancyDebug();
         debug(_state);
         LogWithDelegate(debug);
          GameObject player = GameObject.Find("Player");
        
         PlayerCapsule playerCapsule =
             player.GetComponent<PlayerCapsule>();
         
         playerCapsule.playerJump += HandlePlayerJump;
         
          
         lootStack.Push("Sword of Doom");
         lootStack.Push("HP+");
         lootStack.Push("Golden Key");
         lootStack.Push("Winged Boot");
         lootStack.Push("Mythril Bracers");
        }
        public static void Print(string newText)
        {
            Debug.Log(newText);
        }
        public void LogWithDelegate(DebugDelegate del)
        {
            del("Delegating the debug task...");
        }
        public void PrintLootReport()
         {  var currentItem = lootStack.Pop();
           
            var nextItem = lootStack.Peek();
           
            Debug.LogFormat("You got a {0}! You have a good chance of finding a {1} next!", currentItem, nextItem);
            Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
         } 
         public void HandlePlayerJump()
         {
            debug("Player has jumped...");
         }
        void Update()
        {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (paused == true)
                    {
                        Time.timeScale = 1f;
                        pause.SetActive(false);
                        paused = false;
                    }
                    else
                    {

                        Debug.Log("Paused");
                        Time.timeScale = 0f;
                        pause.SetActive(true);
                        paused = true;
                    }
                }
                
         }
         
}

