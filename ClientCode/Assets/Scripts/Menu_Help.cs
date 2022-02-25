using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;



public class Menu_Help : MonoBehaviour
{
    bool isHelpOpen = true;
    bool isMenuOpen = false;
    public GameObject helpWindow;
    public GameObject menuWindow;
    public Dropdown countrySelect;
    public InputField inputAllCases;
    public InputField inputNewCases;
    public InputField inputAllDeaths;
    public InputField inputNewDeaths;
    public GameObject button;

    void Start()
    {
        StartCoroutine(GetCases());
    }

    void Update()
    {
        //Open panels when key was clicked
        if (Input.GetKeyDown(KeyCode.H))
        {
            OpenCloseHelpWindow();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenCloseMenuWindow();
        }
    }

    public void OpenCloseHelpWindow()
    {
        if (isHelpOpen == true)
        {
            isHelpOpen = false;
            helpWindow.SetActive(false);
            button.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked; // Lock coursor in the middle of the screen
        }
        else if (isHelpOpen == false)
        {
            isHelpOpen = true;
            helpWindow.SetActive(true);
            Cursor.lockState = CursorLockMode.None; // Lock coursor in the middle of the screen
        }
    }

    public void OpenCloseMenuWindow()
    {
        if (isMenuOpen == true)
        {
            isMenuOpen = false;
            
            menuWindow.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked; // Lock coursor in the middle of the screen

        }
        else if (isMenuOpen == false)
        {
            isMenuOpen = true;
            menuWindow.SetActive(true);
            Cursor.lockState = CursorLockMode.None; // Lock coursor in the middle of the screen
        }
    }

    IEnumerator GetCases()
    {
        //Connect to API and perform GET
        using (UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1/countries/"))
        {

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string covidDtataString = www.downloadHandler.text;

                JSONArray covidDataJSON = JSON.Parse(covidDtataString) as JSONArray;

                //Add all countries from JSONArray to dropdown
                for (int i = 0; i <= 189; i++)
                {
                    Country country = new Country(covidDataJSON[i].AsObject["CountryName"], covidDataJSON[i].AsObject["NewCases"], covidDataJSON[i].AsObject["TotalCases"], covidDataJSON[i].AsObject["NewDeaths"], covidDataJSON[i].AsObject["TotalDeaths"], i * 3, 0);
                    countrySelect.options.Add(new Dropdown.OptionData(country.GetName()));
                    
                }
            }
        }
    }

    public void PUT()
    {
        //Put edited data to API
        StartCoroutine(PreformPut(countrySelect.value.ToString(), int.Parse(inputNewCases.text), int.Parse(inputAllCases.text), int.Parse(inputNewDeaths.text), int.Parse(inputAllDeaths.text)));
    }

    public IEnumerator PreformPut(string countryName, int newCases, int totalCases, int newDeaths, int totalDeaths)
    {
        
        //Put a country data to a Json format
        JSONObject countryJson = new JSONObject();
        countryJson.Add("CountryName", countryName);
        countryJson.Add("NewCases", newCases);
        countryJson.Add("TotalCases", totalCases);
        countryJson.Add("NewDeaths", newDeaths);
        countryJson.Add("TotalDeaths", totalDeaths);

        // perform a Unity request to API
        using (UnityWebRequest www = UnityWebRequest.Put("http://127.0.0.1/countries/", countryJson.ToString()))
        {
            
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

            }
        }

    }

    public void ExitApp()
    {
        Application.Quit();
    }    
}