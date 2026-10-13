namespace RPG
{

    using System;
    using System.ComponentModel.Design;
    using System.Reflection.Emit;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Collections.Generic;
    public class Program
    {

        public static void Main(string[] args)
        {
            //Display a welcome screen
            WelcomeScreen();

            //Continue game
            WelcomeUser();

            // LoadingScreen
            LoadingScreen(1);

            // Level 1
            Level1();

            //Enemy stats
            string[] enemies = { "Bandit", "Gang member", "Corrupt cop", "Wolf" };
            int[] enemyHP = { 10, 30, 20, 15 };
            int[] enemyDMG = { 2, 5, 7, 4 };
        }

        public static void Level1()
        {
            // Player stats and inventory
            int playerHP = 15;
            int currentWeaponDamage = 2;
            List<string> inventory = new List<string> { "Bandage", "Bandage", "Revolver" };

            // Enemies setup
            string[] enemies = { "Bandit", "Gang member", "Corrupt cop", "Wolf", "Hiest Planner" };
            int[] enemyHP = { 10, 30, 20, 15, 45 };
            int[] enemyDMG = { 2, 5, 7, 4, 8 };

            Console.Clear();
            Console.WriteLine("Welcome to LEVEL 1");
            Console.WriteLine(" ");
            Console.WriteLine("You arrive at the town of Strawstal see a wanted poster on the bounty board outside\n");
            Thread.Sleep(1500);

            Console.WriteLine(" ");
            Console.WriteLine("Sherif Vector: 'So, Partner. Let me tell about the bounty you've accepted. O'Donald is a leader of a large gang who goes around terrorizing villages and taking anything and everything that has a value.'");
            Thread.Sleep(1500);

            Console.WriteLine(" ");
            Console.WriteLine(" 'Recently he ransacked everything we had. Your job is to find him and bring him back ALIVE... and don't forget to take everything he stole, or at least as much as you can.' ");
            Thread.Sleep(1500);

            Console.WriteLine(" ");
            Console.WriteLine("You exit Sheriff Vector's office and begin to walk around thinking on where to search. Two locations on your map stand out to you: the Forest far off and a Cave nearby.");
            Thread.Sleep(1500);

            string userPath = "";

            while (userPath != "F" && userPath != "C")
            {
                Console.WriteLine("Type F for Forest or C for Cave:");
                userPath = Console.ReadLine()?.ToUpper();

                if (userPath == "F")
                {
                    Console.Clear();
                    Console.WriteLine("You arrive at the forest and run into a wild animal.");
                    Thread.Sleep(2000);

                    
                    bool CombatResult = StartCombat(ref playerHP, ref inventory, ref currentWeaponDamage, enemies[3], enemyHP[3], enemyDMG[3]);

                    if (!CombatResult)
                    {
                        Console.WriteLine("GAME OVER");
                        return;
                    }

                    Console.WriteLine("After the battle, you conclude there is nothing here and head to the Cave.");
                }
                else if (userPath == "C")
                {
                    Console.Clear();
                    Console.WriteLine("On your way to the cave you run into so someone who seems friendly. But as they say, never judge a book by it's cover.");
                    Thread.Sleep(3000);

                    
                    bool CombatResult = StartCombat(ref playerHP, ref inventory, ref currentWeaponDamage, enemies[1], enemyHP[1], enemyDMG[1]);

                    if (!CombatResult)
                    {
                        Console.WriteLine("GAME OVER");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Please enter 'F' for Forest or 'C' for Cave.");
                }
            }

            
            

            Console.WriteLine(" ");
            Console.WriteLine("You arrive at the Cave after roughly a While walk and only find a few boxes and a fire that has burntout. As your about to leave the dark and damp cave you notice something off to the corner of your eye... some tunnels?");
            Console.WriteLine(" ");
            Console.WriteLine("There are three tunnels. One to the left (type L to go in this tunnel), one to the right (Type R to go into this tunnel), one straight ahead (Type F to go into this tunnel).");
            string TunnelChoice = "";

            while (TunnelChoice != "L" && TunnelChoice != "R" && TunnelChoice != "F") //a Loop until the player picks L or R
            {
                TunnelChoice = Console.ReadLine();

                if (TunnelChoice == "L")
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("You walk through the left Tunnel and find a lone Rifle in a crate.");
                    inventory.Add("Rilfe");
                    currentWeaponDamage = 5; //Weapon upgrade increasing damage
                    Console.WriteLine(" ");
                    Console.WriteLine("You equip the Rifle and your attack has rissen by 3");
                }
                else if (TunnelChoice == "R")
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("You walk down the right tunnel and find a padded vest.");
                    playerHP = 25; // health upgrade increasing health
                    Console.WriteLine("HP has incraesed to " + playerHP + ".");

                }
                else if (TunnelChoice == "F")
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("You make your way down the tunnel in front of you, the next thing you hear is a loud bang and then...nothing. No light, no noise, no sense of touch...just nothingness. You've died and somehow pick the only wrong option in this section, here, try it again");
                }
                else
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("This is an invalid input. Please enter L or R or F only.");
                }
            }

            bool victory = StartCombat(ref playerHP, ref inventory, ref currentWeaponDamage, enemies[4], enemyHP[4], enemyDMG[4]);

            if (!victory)
            {
                Console.WriteLine("GAME OVER");
                return;
            }
        }

        public static bool StartCombat(ref int playerHP, ref List<string> inventory, ref int currentWeaponDamage, string enemyName, int enemyHP, int enemyDMG)
        {
            Console.Clear();
            Console.WriteLine($"--- COMBAT STARTED: {enemyName.ToUpper()} ---");
            Console.WriteLine($"A hostile {enemyName} appears with {enemyHP} HP!\n");

            Random rand = new Random();

            while (playerHP > 0 && enemyHP > 0) //a while loop that will keep the fight going until the player or the enemy reach 0 HP 
            {
                Console.WriteLine($"Your HP: {playerHP} | {enemyName} HP: {enemyHP}");
                Console.WriteLine("What will you do?");
                Console.WriteLine("1. Attack (Deal 4-8 DMG)");
                Console.WriteLine("2. Heal (Restore 5 HP)");
                Console.Write("Choice: ");

                string choice = Console.ReadLine();
                Console.WriteLine(" ");

                if (choice == "1")
                {
                    int playerDamage = rand.Next(4, 9);
                    enemyHP -= playerDamage;
                    Console.WriteLine($"You attack the {enemyName} for {playerDamage} damage!");
                }
                else if (choice == "2")
                {
                    playerHP += 5;
                    Console.WriteLine("You quickly use a bandage and recover 5 HP!");
                }
                else
                {
                    Console.WriteLine("Invalid choice! You hesitate and lose your turn.");
                }

                if (enemyHP <= 0)
                {
                    Console.WriteLine($"You defeated the {enemyName}!");
                    Thread.Sleep(2000);
                    return true;
                }

                int enemyAttack = rand.Next(1, enemyDMG + 1);
                playerHP -= enemyAttack;
                Console.WriteLine($"The {enemyName} strikes back for {enemyAttack} damage!");
                Thread.Sleep(1500);
            }

            if (playerHP <= 0)
            {
                Console.WriteLine("You were defeated in battle...");
                Thread.Sleep(2000);
                return false;
            }

            return true;
        }

        public static void LoadingScreen(int level)
        {
            Console.Clear();
            Console.WriteLine("LEVEL " + level + " LOADING");
            Console.WriteLine(" ");

            switch (level) //will display the set loading screen depending on which level it is
            {
                case 1:
                    Console.WriteLine(@"      &&
           &&&&&
         &&&\/& &&&
        &&|,/  |/& &&
         &&/   /  /_&  &&
           \  {  |_____/_&
           {  / /          &&&
           `, \{___________/_&&
            } }{       \
            }{{         \____&
           {}{           `&\&&
           {{}             &&
     , -=-~{ .-^- _
           `}
            {");
                    break;
                case 2:
                    Console.WriteLine(@" (                                 _
   )                               /=>
  (  +____________________/\/\___ / /|
   .''._____________'._____      / /|/\
  : () :              :\ ----\|    \ )
   '..'______________.'0|----|      \
                    0_0/____/        \
                        |----    /----\
                       || -\\ --|      \
                       ||   || ||\      \
                        \\____// '|      \
                                .'/       |
                               .:/        |
                               :/_________|");
                    break;
                case 3:
                    Console.WriteLine(@"   .-=====----:::....        
           .-+=+=++########*+.        
           :+###%%%%%%%%%%##*:        
   ..:=***++*##%%###%%%%%###*=.       
  .-*###****###%%############+.       
..+%%###**#####%%%%##**###%##*:...    
.=%%%#***#####%%%%%%%%%%%%%%#*++===++:
:#%%#****####%%%%%%%%%%%%%%####****=:.
+%##*****##%%%%%%%%%%%%%%%%%####*+=-:.
*##********##%%%%%%%%%%%#####**+==-:..
-*********************####**++==-::...
.:+*****************###**++==--::.... 
   .:=***********###**++==-::....     
        ..:-=+++++==--::......        ");
                    break;
                case 4:
                    Console.WriteLine(@"                                                          
                        ................                      
                      ...::::::::::::::...                    
                   ...::::::::::::::::::::..                  
                ....::::::::::::::::::::::::...               
               ....:::::::::::::::::::::::::::...             
               ...:::::::::::::::::.....:::::::....           
              ...:::::::::::-::::::........::::......         
             ...:::::-::--:::::::::.......::.::::.....        
            ..:--:::::::------:::::...::--:.::.::::....       
           .:+#%%*-:--:-==+++==-::....:--::::..:::::...       
           .-#%%%%*---=++*##%%%#*=:::::::::::::::::::..       
           .:*%%##*-:-+++*%%%@@@%%#=-:-----:::::--:::..       
          ..-*%#*-:::--=**#%@@@@@@%*=--====:::::::-::..       
         ..:+##*+=-:::--=+#%@@@@@@%*=--=++-::::::::::..       
         ..:::::+*+-:.:--=+*#%@@%%*=-::++==-::::::::..        
        ..:::=-=####+-------=+#%%+-:::-====--:::::::..        
        ..::::-*%%@@%*---==----::::..:-++=-----::::...        
        .....:-+**#%%*----------::::.:::--:::::--::..         
           ..:---=**=-::-----:--::::::::::::::::::..          
          ..:::::-=--:.::---:--====-:-*%#+===-:::..           
          .:--::--:::::::::::=*%%%%=:-==-:+++-::..            
          .:---:-+-=:--=--::-+%%@@*:::::-=+*=::..             
           ..:----=---=+-=+--=#%%*-:::::=++=-:..              
           ..--=----=-==--=-==*#+-::::::::::...               
          ...::---------:-=-----::::::..                      
         ...:::::-::::::::...::::::::..                       
         ..::::::----:---::::...:::::..                       
          ...::::::::::::::::........                         
              ..............                                  
                                                              ");
                    break;


            }

            Thread.Sleep(3000); // Sleep for 3 seconds
        }


        public static void WelcomeUser()
        {

            Console.Clear();
            Console.WriteLine("Welcome, Partner! Mind telling me your name?");

            Console.WriteLine(" ");
            string userName = Console.ReadLine();
            Console.WriteLine(" ");
            Console.WriteLine("Well it's nice to meet you, " + userName + "! And welcome to the lovely town of Strawstal!");
            Console.WriteLine(" ");
            Thread.Sleep(3000); // Sleep for 3 seconds



        }

        public static void WelcomeScreen()
        {
            // Output your image
            // clear the screen 
            Console.Clear();
            Console.WriteLine("Welcome, hunter!");
            Console.WriteLine(" ");
            Console.WriteLine(@" (                                 _
   )                               /=>
  (  +____________________/\/\___ / /|
   .''._____________'._____      / /|/\
  : () :              :\ ----\|    \ )
   '..'______________.'0|----|      \
                    0_0/____/        \
                        |----    /----\
                       || -\\ --|      \
                       ||   || ||\      \
                        \\____// '|      \
                                .'/       |
                               .:/        |
                               :/_________|");


            Thread.Sleep(3000); // Sleep for 3 seconds
            Console.WriteLine(" ");
            Console.WriteLine("...Program continues after 3 seconds");
            Thread.Sleep(3000); // Sleep for 3 seconds
        }

    }
}



