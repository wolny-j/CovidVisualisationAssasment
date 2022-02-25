using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class ConnectToDB : MonoBehaviour
{
    public GameObject deaths;
    public GameObject cases;
    //public GameObject countryNameObject;
    public Text UIText;

    public List<GameObject> totalCasesList = new List<GameObject>();
    public List<GameObject> newCasesList = new List<GameObject>();
    public List<GameObject> totalDeathsList = new List<GameObject>();
    public List<GameObject> newDeathsList = new List<GameObject>();
    public List<Country> countriesList = new List<Country>();
    public List<Text> textListAllC = new List<Text>();
    public List<Text> textListNewC = new List<Text>();
    public List<Text> textListAllD = new List<Text>();
    public List<Text> textListNewD = new List<Text>();

    private float nextActionTime = 0.0f;
    private float period = 1f;

    private void Start()
    {
        StartCoroutine(GetCases());
    }

    void FixedUpdate()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            StartCoroutine(UpdateStats());

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

                string covidDtataString = www.downloadHandler.text;

                JSONArray covidDataJSON = JSON.Parse(covidDtataString) as JSONArray;

                //Foreach Country in JsonArray create "Country" object and put it to column generator
                for (int i = 0; i <= 189; i++)
                {      
                    Country country = new Country(covidDataJSON[i].AsObject["CountryName"], covidDataJSON[i].AsObject["NewCases"], covidDataJSON[i].AsObject["TotalCases"], covidDataJSON[i].AsObject["NewDeaths"], covidDataJSON[i].AsObject["TotalDeaths"], i * 3, 0);
                    countriesList.Add(country);

                    GenerateTotalCases(country.GetName(), country.GetAllCases(), country.GetX(), country.GetY());
                    GenerateNewlCases(country.GetName(), country.GetNewCases(), country.GetX(), country.GetY());
                    GenerateTotalDeaths(country.GetName(), country.GetAllDeaths(), country.GetX(), country.GetY());
                    GenerateNewDeaths(country.GetName(), country.GetNewDeaths(), country.GetX(), country.GetY());

                }
                
            }            
        }
    }

    //Functions for generating columns
    void GenerateTotalDeaths(string countryName, float x, int XCord, int YCord)
    {
        float blocks = x / 1000; //apply to appropriate scale
        Vector3 scaleChange;
        scaleChange = new Vector3(0f, blocks, 0f);

        GameObject totalDeaths = Instantiate(deaths, new Vector3(XCord - 1, 0, YCord - 56), Quaternion.identity); //Generate column
        totalDeaths.transform.localScale += scaleChange;
        totalDeathsList.Add(totalDeaths);
        GenerateTextAllD(countryName, x, XCord, YCord - 56);
    }

    void GenerateNewDeaths(string countryName, float x, int XCord, int YCord)
    {
        float blocks = x / 100; //apply to appropriate scale
        Vector3 scaleChange;
        scaleChange = new Vector3(0f, blocks, 0f);

        GameObject newDeaths = Instantiate(deaths, new Vector3(XCord - 1, 0, YCord - 84), Quaternion.identity); //Generate column
        newDeaths.transform.localScale += scaleChange;
        newDeathsList.Add(newDeaths);
        GenerateTextNewD(countryName, x, XCord, YCord - 84);
    }

    void GenerateTotalCases(string countryName, float x, int XCord, int YCord)
    {
        float blocks = x / 100000; //apply to appropriate scale
        Vector3 scaleChange;
        scaleChange = new Vector3(0f, blocks, 0f);

        GameObject totalCases = Instantiate(cases, new Vector3(XCord - 1, 0, YCord), Quaternion.identity); //Generate column
        totalCases.transform.localScale += scaleChange;
        totalCasesList.Add(totalCases);
        
        GenerateTextAllC(countryName, x, XCord, YCord);
    }

    void GenerateNewlCases(string countryName, float x, int XCord, int YCord)
    {
        float blocks = x / 1000; //apply to appropriate scale
        Vector3 scaleChange;
        scaleChange = new Vector3(0f, blocks, 0f);

        GameObject newCases = Instantiate(cases, new Vector3(XCord - 1, 0, YCord - 28), Quaternion.identity); //Generate column
        newCases.transform.localScale += scaleChange;
        newCasesList.Add(newCases);
        GenerateTextNewC(countryName, x, XCord, YCord - 28);
    }

    //Generate text with Country name and selected data
    void GenerateTextAllC(string countryName, float data, int XCord, int YCord)
    {
        UIText.text = countryName + "\n" + data;
        Text text = Instantiate(UIText, new Vector3(XCord - 1f, 1f, YCord - 0.6f), Quaternion.identity);
        textListAllC.Add(text);
        
    }
    void GenerateTextNewC(string countryName, float data, int XCord, int YCord)
    {
        UIText.text = countryName + "\n" + data;
        Text text = Instantiate(UIText, new Vector3(XCord - 1f, 1f, YCord - 0.6f), Quaternion.identity);
        textListNewC.Add(text);

    }
    void GenerateTextAllD(string countryName, float data, int XCord, int YCord)
    {
        UIText.text = countryName + "\n" + data;
        Text text = Instantiate(UIText, new Vector3(XCord - 1f, 1f, YCord - 0.6f), Quaternion.identity);
        textListAllD.Add(text);

    }
    void GenerateTextNewD(string countryName, float data, int XCord, int YCord)
    {
        UIText.text = countryName + "\n" + data;
        Text text = Instantiate(UIText, new Vector3(XCord - 1f, 1f, YCord - 0.6f), Quaternion.identity);
        textListNewD.Add(text);

    }

    //Function for updating data
    IEnumerator UpdateStats()
    {
        //Connect to API and perform GET 
        using (UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1/countries"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {

                string covidDtataString = www.downloadHandler.text;

                JSONArray covidDataJSON = JSON.Parse(covidDtataString) as JSONArray;

                //Foreach Country in JsonArray create "Country" object and put it to column generator
                for (int i = 0; i <= 189; i++)
                {
                    countriesList[i].SetAllCases(covidDataJSON[i].AsObject["TotalCases"]);
                    countriesList[i].SetNewCases(covidDataJSON[i].AsObject["NewCases"]);
                    countriesList[i].SetAllDeaths(covidDataJSON[i].AsObject["TotalDeaths"]);
                    countriesList[i].SetNewDeaths(covidDataJSON[i].AsObject["NewDeaths"]);
                    

                    UpdateNewCases(countriesList[i].GetName(), countriesList[i].GetNewCases(), i);
                    UpdateNewDeaths(countriesList[i].GetName(), countriesList[i].GetNewDeaths(), i);
                    UpdateTotalCases(countriesList[i].GetName(), countriesList[i].GetAllCases(), i);
                    UpdateTotalDeaths(countriesList[i].GetName(), countriesList[i].GetAllDeaths(), i);



                }
            }
        }
    }
    //Function for updating columns
    void UpdateTotalDeaths(string countryName, float x, int i)
    {
        float blocks = x / 1000; //apply to appropriate scale
        Vector3 scaleChange;
        scaleChange = new Vector3(0f, blocks, 0f);
        
        totalDeathsList[i].transform.transform.localScale = new Vector3(1f, 0.1f, 1f);
        totalDeathsList[i].transform.transform.localScale += scaleChange;
        UpdateTextAllD(countryName, x, i);
    }

    void UpdateTotalCases(string countryName, float x, int i)
    {
        float blocks = x / 100000; //apply to appropriate scale
        Vector3 scaleChange;
        scaleChange = new Vector3(0f, blocks, 0f);

        totalCasesList[i].transform.transform.localScale = new Vector3(1f, 0.1f, 1f);
        totalCasesList[i].transform.transform.localScale += scaleChange;
        UpdateTextAllC(countryName, x, i);
    }

    void UpdateNewDeaths(string countryName, float x, int i)
    {
        float blocks = x / 100; //apply to appropriate scale
        Vector3 scaleChange;
        scaleChange = new Vector3(0f, blocks, 0f);

        newDeathsList[i].transform.transform.localScale = new Vector3(1f, 0.1f, 1f);
        newDeathsList[i].transform.transform.localScale += scaleChange;
        UpdateTextNewD(countryName, x, i);
    }

    void UpdateNewCases(string countryName, float x, int i)
    {
        float blocks = x / 1000; //apply to appropriate scale
        Vector3 scaleChange;
        scaleChange = new Vector3(0f, blocks, 0f);

        newCasesList[i].transform.transform.localScale = new Vector3(1f, 0.1f, 1f);
        newCasesList[i].transform.transform.localScale += scaleChange;
        UpdateTextNewC(countryName, x, i);
    }
    void UpdateTextAllC(string countryName, float data, int i)
    {
        UIText.text = countryName + "\n" + data;
        textListAllC[i].text = UIText.text;

    }

    void UpdateTextNewC(string countryName, float data, int i)
    {
        UIText.text = countryName + "\n" + data;
        textListNewC[i].text = UIText.text;

    }
    void UpdateTextAllD(string countryName, float data, int i)
    {
        UIText.text = countryName + "\n" + data;
        textListAllD[i].text = UIText.text;

    }
    void UpdateTextNewD(string countryName, float data, int i)
    {
        UIText.text = countryName + "\n" + data;
        textListNewD[i].text = UIText.text;
        
    }



}
