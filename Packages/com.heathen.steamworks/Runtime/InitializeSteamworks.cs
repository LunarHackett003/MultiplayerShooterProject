#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
    [HelpURL("https://kb.heathen.group/steamworks/initialization/unity-initialization#component")]
    [DisallowMultipleComponent]
    public class InitializeSteamworks : MonoBehaviour
    {
        [SerializeField]
        [HideInInspector]
        internal SteamSettings targetSettings;
        [SerializeField]
        [HideInInspector]
        internal SteamSettings mainSettings;
        [SerializeField]
        [HideInInspector]
        internal SteamSettings demoSettings;
        [SerializeField]
        [HideInInspector]
        internal List<SteamSettings> playtestSettings = new();

        private void Start()
        {
            if (targetSettings != null && !API.App.Initialized)
                targetSettings.Initialize();
            else if (!API.App.Initialized)
            {
#if !UNITY_EDITOR
                Debug.LogError("No settings found");
#else
                Debug.LogError($"No settings have been selected, either you have removed the previously selected settings or you have never set the desired settings on the Initialize Steamworks object.");
#endif
            }
            else
            {
                Debug.LogWarning("Steamworks API is already initialized, duplicate Steam initialization process detected.");
            }
        }

#if UNITY_EDITOR
        internal void RefreshSettings()
        {
            mainSettings = null;
            demoSettings = null;
            playtestSettings.Clear();

            mainSettings = SteamSettings.GetOrCreateSettings();
            
            if(SteamSettings.HasDemoSettings())
                demoSettings = SteamSettings.GetOrCreateDemoSettings();
            else
                demoSettings = null;

            playtestSettings = SteamSettings.GetPlayTestSettings();

            if(mainSettings != targetSettings
                && demoSettings != targetSettings)
            {
                bool foundTarget = false;
                foreach(var set in playtestSettings)
                {
                    if(set == targetSettings) 
                        foundTarget = true;
                }

                if(!foundTarget)
                    targetSettings = mainSettings;
            }
            else
            {
                // We must have found the target settings in one of the valid settings files so leave it alone.
            }

            EditorUtility.SetDirty(this);
        }

        [UnityEditor.CustomEditor(typeof(InitializeSteamworks))]
        public class InitializeSteamworksEditor : UnityEditor.Editor
        {
            private InitializeSteamworks parent;

            public override void OnInspectorGUI()
            {
                UnityEditor.EditorGUILayout.BeginHorizontal();
                if(UnityEditor.EditorGUILayout.LinkButton("Settings"))
                    UnityEditor.SettingsService.OpenProjectSettings("Project/Player/Steamworks");
                if (UnityEditor.EditorGUILayout.LinkButton("Inspector"))
                    UnityEditor.EditorApplication.ExecuteMenuItem("Window/Steamworks/Inspector");
                UnityEditor.EditorGUILayout.EndHorizontal();


                parent = target as InitializeSteamworks;

                if (parent.mainSettings != parent.targetSettings
                && parent.demoSettings != parent.targetSettings)
                {
                    bool foundTarget = false;
                    foreach (var set in parent.playtestSettings)
                    {
                        if (set == parent.targetSettings)
                            foundTarget = true;
                    }

                    if (!foundTarget)
                    {
                        parent.targetSettings = parent.mainSettings;
                        UnityEditor.EditorUtility.SetDirty(target);
                    }
                }

                if(parent.targetSettings == null)
                {
                    parent.mainSettings = SteamSettings.GetOrCreateSettings();
                    parent.targetSettings = parent.mainSettings;
                    UnityEditor.EditorUtility.SetDirty(target);
                }

                List<string> names = new List<string>();
                int index = 0;

                names.Add("Main");
                if (parent.demoSettings != null)
                {
                    if(parent.targetSettings == parent.demoSettings)
                        index = 1;
                    names.Add("Demo");
                }
                var currentLength = names.Count;
                if(parent.playtestSettings != null)
                {
                    parent.playtestSettings.RemoveAll(s => s == null);

                    foreach (var setting in  parent.playtestSettings)
                    {
                        if (parent.targetSettings == setting)
                            index = 1 + (parent.demoSettings != null ? 1 : 0) + names.Count - currentLength;

                        names.Add(setting.name);
                    }
                }

                var newIndex = index;

                UnityEditor.EditorGUILayout.BeginHorizontal();
                newIndex = UnityEditor.EditorGUILayout.Popup(index, names.ToArray());
                if (GUILayout.Button("Refresh", GUILayout.Width(60)))
                {
                    parent.RefreshSettings();
                }
                UnityEditor.EditorGUILayout.EndHorizontal();

                if(newIndex != index)
                {
                    UnityEditor.Undo.RecordObject(target, "Active Settings");
                    if (newIndex == 0)
                        parent.targetSettings = parent.mainSettings;
                    else if (parent.demoSettings != null && newIndex == 1)
                        parent.targetSettings = parent.demoSettings;
                    else
                    {
                        if(newIndex - currentLength < parent.playtestSettings.Count)
                            parent.targetSettings = parent.playtestSettings[newIndex - currentLength];
                    }
                    UnityEditor.EditorUtility.SetDirty(target);
                }

                //base.OnInspectorGUI();
            }
        }
#endif
    }
}
#endif