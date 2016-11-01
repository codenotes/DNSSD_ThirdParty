// Fill out your copyright notice in the Description page of Project Se ttings.
//NOTE: You need androidlibs cloned and ANDROIDLIBS_ROOT and ANDROID_ROS_BOOST_LOCATION to whever that is
using UnrealBuildTool;
using System.IO;
using System;

public class DNSSD : ModuleRules
{
    private string ModulePath
    {
        get { return ModuleDirectory; }
    }

    public void addPreproc(string ros_preproc)
    {
        foreach (string s in ros_preproc.Split(';'))
        {
            Console.WriteLine("DEFINE:" + s);
            Definitions.Add(s);

        }
    }

    private string ThirdPartyPath
    {
        get { return Path.GetFullPath(Path.Combine(ModulePath, "../../ThirdParty/")); }
    }

    public void includeAdd(string env)
    {
        var items = Environment.GetEnvironmentVariable(env);
        foreach (string s in items.Split(';'))
        {
            Console.WriteLine("INCLUDE:" + s);
            PublicIncludePaths.Add(s);

        }

        

    }

    public void includeLib(string env, string prefix = null)
    {
        var items = Environment.GetEnvironmentVariable(env);
        string slib;

        if (prefix != null)
        {//TODO:make for non windows case
            if (prefix.PadRight(1) != "\\")
                prefix += '\\';
        }

        foreach (string s in items.Split(';'))
        {


            slib = prefix + s;
            Console.WriteLine("LIB INCLUDE:" + slib);
            PublicAdditionalLibraries.Add(slib);

        }
    }


    public DNSSD(TargetInfo Target)
    {
      
		Type = ModuleType.External;

        Console.WriteLine("^...iterating through Definitions...");

        foreach(string s in Definitions)
        {
            Console.WriteLine("-->"+s);
        }

		
		if (Target.Platform == UnrealTargetPlatform.Android)
		{
			// Uncomment if you are using Slate UI  ANDROIDLIBS_ROOT
				// PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });

				// Uncomment if you are using online features
				// PrivateDependencyModuleNames.Add("OnlineSubsystem");
				// if ((Target.Platform == UnrealTargetPlatform.Win32) || (Target.Platform == UnrealTargetPlatform.Win64))
				// {
				//		if (UEBuildConfiguration.bCompileSteamOSS == true)
				//		{
				//			DynamicallyLoadedModuleNames.Add("OnlineSubsystemSteam");
				//		}
				// }
				PrivateIncludePaths.AddRange(
					new string[] {

								"../../../../Source/Runtime/Renderer/Private",
								"../../../../Source/Runtime/Launch/Private"
						}
					);

	
		}

        bUseRTTI = true; //oh so very important...lets boost dynamic cast return horror to the client. /GR
        UEBuildConfiguration.bForceEnableExceptions = true;

        //BOOST_REGEX_NO_EXTERNAL_TEMPLATES
      //  var ros_preproc = "OPENCV;_NO_FTDI;GREG1;BOOST_LIB_DIAGNOSTIC";// ;BOOST_LIB_DIAGNOSTIC";BOOST_TYPE_INDEX_FORCE_NO_RTTI_COMPATIBILITY;BOOST_NO_RTTI;BOOST_NO_TYPEID
       // addPreproc(ros_preproc);

		
        if (Target.Platform == UnrealTargetPlatform.Android || Target.Platform == UnrealTargetPlatform.Win64)
        {

           
			

		    

        }

		
		if (Target.Platform == UnrealTargetPlatform.Win64)
        {
			
			Console.WriteLine("^In Win64 Build/Single DLL dns-sd.dll");


			//ModuleDirectory + \x64\Rel-64-15\rosjadecpp-r-2015.lib"
			string lib=ModuleDirectory + @"\Lib\Windows\x64\Rel-64-15\dns-sd.lib";
            PublicAdditionalLibraries.Add(ModuleDirectory + @"\Lib\Windows\x64\Rel-64-15\dns-sd.lib");
			Console.WriteLine(lib);
            
            string fname = Path.Combine(ModuleDirectory + @"\bin\x64\dns-sd.dll");
            PublicDelayLoadDLLs.Add(fname);
            RuntimeDependencies.Add(new RuntimeDependency(fname));
			RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(ModuleDirectory + @"\bin\Windows\x64\dns-sd.dll")));
			


        }
		
		if (Target.Platform == UnrealTargetPlatform.Android)
        {
			Console.WriteLine("^^^^DNSD for android isn't applicable.  Android uses a java DNS.");

		
			
	
		}
		
       

    }
}
