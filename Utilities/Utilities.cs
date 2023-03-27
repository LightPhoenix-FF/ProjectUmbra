using System.Reflection;
using HarmonyLib;
using TMPro;
using Trainworks.Managers;
using UnityEngine;
using UnityEngine.TextCore;
using System.IO;

namespace ProjectUmbra
{
    public class Utilities
    {
        public static void Log(string logString)
        {
            Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.Info, logString);
        }

        public static void ModifyCharacterField(string charID, string field, int value)
        {
            Traverse.Create(
                CustomCharacterManager.GetCharacterDataByID(charID)).Field(field).SetValue(value);
        }

        public static void AddLocalizationKey(string keyName, string keyGroup, string description)
        {
            string key = keyName;
            CustomLocalizationManager.ImportSingleLocalization(key, "Text", "", "", keyGroup, "", description);
        }

        public static void OverwriteTextAtlas(string path)
        {
            Texture2D texture = LoadTextureFromPath(path);
            if (texture == null)
            {
                Log("No texture loaded.");
                return;
            }

            TMP_SpriteAsset[] atlas_TextIcons = UnityEngine.Resources.FindObjectsOfTypeAll<TMP_SpriteAsset>();
            atlas_TextIcons[0].material.SetTexture(ShaderUtilities.ID_MainTex, texture);
            atlas_TextIcons[0].UpdateLookupTables();
        }

        public static Texture2D LoadTextureFromPath(string path)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            string assetPath = PluginManager.PluginGUIDToPath[PluginManager.AssemblyNameToPluginGUID[assembly.FullName]];
            string fullPath = assetPath + "/" + path;
            if (File.Exists(fullPath))
            {
                byte[] fileData = File.ReadAllBytes(fullPath);
                Texture2D texture = new Texture2D(1, 1);
                ImageConversion.LoadImage(texture, fileData);
                texture.name = Path.GetFileNameWithoutExtension(fullPath);
                return texture;
            }
            return null;
        }

        public static void SaveTextureToPath(Texture2D texture, string path)
        {
            byte[] fileData = texture.EncodeToPNG();
            File.WriteAllBytes(path + "_export.png", fileData);
        }

        public static void AddTextureToTextAtlas(string path)
        {
            TMP_SpriteAsset[] atlas_TextIcons = UnityEngine.Resources.FindObjectsOfTypeAll<TMP_SpriteAsset>();

            // Get the texture to add - this should be 24x24.
            Texture2D texture = LoadTextureFromPath(path);
            if (texture == null)
            {
                Log("AddTextureToTextAtlas - No texture at " + path + " loaded.");
                return;
            }
            if(texture.width != 24 || texture.height != 24)
            {
                Log("AddTextureToTextAtlas - Texture " + texture.name + " is not 24 by 24.  Texture not loaded.");
                return;
            }

            // We need to do this to get around the original atlas texture having isReadable = false.
            Texture2D currentAtlas = (Texture2D)atlas_TextIcons[0].material.mainTexture;
            RenderTexture tmpRender = RenderTexture.GetTemporary(currentAtlas.width, currentAtlas.height);
            Graphics.Blit(currentAtlas, tmpRender);
            RenderTexture.active = tmpRender;
            Texture2D tmpAtlas = new Texture2D(currentAtlas.width, currentAtlas.height, currentAtlas.format, false);
            tmpAtlas.ReadPixels(new Rect(0, 0, tmpRender.width, tmpRender.height), 0, 0);
            tmpAtlas.Apply();
            RenderTexture.ReleaseTemporary(tmpRender);

            // Copy the icon to the next available location on the grid
            // Note: tiles go from left to right, and bottom to top, original glyph count is 67.
            // New tiles start at tileX, tileY = 8,0
            int tileN = atlas_TextIcons[0].spriteGlyphTable.Count - 67;
            int tileX = (tileN / 9) + 8;
            int tileY = tileN % 9;
            tmpAtlas.SetPixels(26 * tileX + 2, 26 * tileY + 2, texture.width, texture.height, texture.GetPixels());
            tmpAtlas.Apply();

            // Update the material
            atlas_TextIcons[0].material.SetTexture(ShaderUtilities.ID_MainTex, tmpAtlas);

            // Update the glyph table and character table
            RegisterIconToTextAtlas(texture.name, tileX, tileY);
        }

        public static void RegisterIconToTextAtlas(string iconName, int tileX, int tileY)
        {
            TMP_SpriteAsset[] atlas_TextIcons = UnityEngine.Resources.FindObjectsOfTypeAll<TMP_SpriteAsset>();

            // Update the glyph table
            uint index = (uint)atlas_TextIcons[0].spriteGlyphTable.Count + 1;
            GlyphMetrics metrics = new GlyphMetrics(24.0f, 24.0f, 0.0f, 20.0f, 24.0f);
            GlyphRect rect = new GlyphRect(26 * tileX + 2, 26 * tileY + 2, 24, 24);
            TMP_SpriteGlyph sg = new TMP_SpriteGlyph(index, metrics, rect, 1.0f, 0);
            atlas_TextIcons[0].spriteGlyphTable.Add(sg);

            // Update the character table
            TMP_SpriteCharacter sc = new TMP_SpriteCharacter(0, sg);
            sc.name = iconName;
            sc.glyphIndex = index;
            sc.scale = 1.0f;
            atlas_TextIcons[0].spriteCharacterTable.Add(sc);

            // Update the lookup table dictionaries
            atlas_TextIcons[0].UpdateLookupTables();
        }
    }
}
