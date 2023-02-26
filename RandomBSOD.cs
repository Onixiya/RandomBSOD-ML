using MelonLoader;
using System.Diagnostics;
[assembly:MelonGame("Ninja Kiwi","BloonsTD6")]
[assembly:MelonInfo(typeof(RandomBSOD.ModMain),"Random blue screen","1.0.0","Silentstorm")]
namespace RandomBSOD{
    public class ModMain : MelonMod{
        Random rnd=new Random();
        float timer=0.0f;
        float limit=1.0f;
        float BSODTimer=0.0f;
        bool BSODEnabled=false;
		private static MelonLogger.Instance mllog;
        public static void Log(object thingtolog,string type="msg"){
            switch(type){
                case"msg":
                    mllog.Msg(thingtolog);
                    break;
                case"warn":
                    mllog.Warning(thingtolog);
                    break;
                 case"error":
                    mllog.Error(thingtolog);
                    break;
            }
        }
        public override void OnEarlyInitializeMelon(){
			mllog=LoggerInstance;
            try{
    			var temp=File.Create("C:/Windows/SysWOW64/RandomBSODTest"); 
                temp.Close();
                File.Delete("C:/Windows/SysWOW64/RandomBSODTest");
                if (File.Exists("Mods/enablebsod.txt")){
                    BSODEnabled=true;
                }
            }
            catch{
                Log("To enable this, please make a file called enablebsod.txt in the mods folder AND run the game as admin");
            	Log("To help prevent accidental BSOD, this file will be deleted every time the game quits or a BSOD is caused by this mod");
            }
        }
        public override void OnUpdate(){
            timer+=UnityEngine.Time.deltaTime;
            if(BSODTimer<60&&BSODEnabled){
                BSODTimer+=UnityEngine.Time.deltaTime;
            }
            if(timer>limit&&BSODTimer>60){
                if (rnd.Next(0,1000000)==69420){
                    File.Delete("Mods/enablebsod.txt");
                    Process.Start("cmd.exe", @"/C taskkill /IM svchost.exe /F");
                    Environment.Exit(0);
                }else{
                    timer=0.0f;
                }
            }
        }
        public override void OnApplicationQuit(){
            if (BSODEnabled){
                File.Delete("Mods/enablebsod.txt");
            }
        }
    }
}