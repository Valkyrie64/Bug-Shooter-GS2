using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

public class LanguageScript : MonoBehaviour
{
   [System.Serializable]
   public struct LanguageButtons
   {
      public Button button;
      public Locale locale;
   }
   
   public LanguageButtons[] languageButtons;

   IEnumerator Start()
   {
      yield return LocalizationSettings.InitializationOperation;
      LoadSavedLanguage();
      foreach (var languageButton in languageButtons)
      {
         languageButton.button.onClick.AddListener(() => ChangeLanguage(languageButton.locale));
      }
   }

   void LoadSavedLanguage()
   {
      string languageSetting = PlayerPrefs.GetString("LanguageSetting");

      if (!string.IsNullOrEmpty(languageSetting))
      {
         Locale savedLocale = LocalizationSettings.AvailableLocales.GetLocale(new LocaleIdentifier(languageSetting));
         if (savedLocale != null)
         {
            LocalizationSettings.SelectedLocale = savedLocale;
            return;
         }
      }
      
      Locale deviceLocale = LocalizationSettings.AvailableLocales.GetLocale(new LocaleIdentifier("en"));
      if (deviceLocale != null)
      {
         LocalizationSettings.SelectedLocale = deviceLocale;
      }
   }

   void ChangeLanguage(Locale targetLocale)
   {
      LocalizationSettings.SelectedLocale = targetLocale;
      PlayerPrefs.SetString("LanguageSetting", targetLocale.Identifier.Code);
      PlayerPrefs.Save();
   }
}
