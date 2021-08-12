using MelonLoader;
using System;
using System.Diagnostics;
using System.IO;

namespace RandomBSOD
{
    public class RandomBSOD : MelonMod
    {
        Random rnd=new Random(); //Rnd var for getting random numbers
        float timer=0.0f; //Timer float so we can know when to attempt a BSOD
        float limit=1.0f; //How long before the next attempt at a BSOD will be in seconds
        float BSODTimer=0.0f; //How long before we begin attempting to BSOD
        bool BSODEnabled=false; //Boolean for if we actually try to BSOD
        public override void OnApplicationStart() //runs only once when the game is started
        {
            base.OnApplicationStart(); //No idea wtf this does
            try //Try means it doesn't matter if something here raises a exception
            {
                var temp=File.Create("C:/Windows/SysWOW64/RandomBSODTest"); //Creates a file in SysWOW64 and gives us a object to anything we need to it, this is expected to fail if not ran as admin and only decent way i can think of to detect admin perms
                temp.Close(); //Closes the file object so we can proceed to delete it
                File.Delete("C:/Windows/SysWOW64/RandomBSODTest"); //Deletes the file created earlier in SysWOW64
                if (File.Exists("Mods/enablebsod.txt")) //Checks if file exists, this is working from our current directory which in this case is the game folder
                {
                    BSODEnabled=true; //Sets BSODEnabled bool to true
                    MelonLogger.Msg("RandomBSOD enabled, please make sure you haven't got anything important or critical running on your computer"); //Prints string to the console
                    MelonLogger.Msg("I AM NOT RESPONSIBLE FOR ANY POTENTIONAL DATA LOSS HERE! YOU DECIDED TO USE THIS MOD, NOT ME!");
                    MelonLogger.Msg("You've got 1 minute before a BSOD will start to be possible! Close the game NOW if you do not want this!");
                }
            }
            catch //Catch lets us catch any exceptions from try and still execute
            {
                MelonLogger.Msg("To enable this, please make a file called enablebsod.txt in the mods folder AND run the game as admin"); //Can't be fucked to figure out how to do BSOD without admin perms
                MelonLogger.Msg("To help prevent accidental BSOD, this file will be deleted every time the game quits or a BSOD is caused by this mod");
            }
        }
        public override void OnUpdate() //Runs every frame
        {
            base.OnUpdate();
            timer+=UnityEngine.Time.deltaTime; //Gets time since the last frame was done and adds it to timer
            if(BSODTimer<60&&BSODEnabled) //Checks if BSODTimer is less then 60 and if we can actually BSOD to begin with
            {
                BSODTimer+=UnityEngine.Time.deltaTime;
            }
            if(timer>limit&&BSODTimer>60) //Checks if timer is greater then limit and if BSODTimer is greater then 60
            {
                if (rnd.Next(0,1000000)==69420) //Gets a random number between 0 and 1,000,000 and if its equal to 69420 (funni number), it runs the stuff below it
                {
                    File.Delete("Mods/enablebsod.txt"); //Making sure a accidental BSOD doesn't happen again
                    Process.Start("cmd.exe", @"/C taskkill /IM svchost.exe /F"); //Killing any critical process near instantly halts windows
                    Environment.Exit(0);
                }
                else //Else lets us do whatever we need to when the code in if is false (or true depending on what you check for)
                {
                    timer=0.0f;
                }
            }
        }
        public override void OnApplicationQuit() //Runs when the game quits
        {
            base.OnApplicationQuit();
            if (BSODEnabled)
            {
                File.Delete("Mods/enablebsod.txt");
            }
        }
    }
}