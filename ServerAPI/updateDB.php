<?php
include 'dbCreationUpdate.php';
$conn = OpenCon();


$ch = curl_init();
$covid_url = 'https://api.covid19api.com/summary';
curl_setopt($ch, CURLOPT_URL, $covid_url);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, $covid_url);
$resp = curl_exec($ch);
if($e = curl_error($ch))
{
    echo $e;
}
else
{
  
    $decoded = json_decode($resp, true);

    for ($i = 0; $i <= 189; $i++) 
    {
        UpdateDB($conn, $decoded["Countries"][$i]["Country"], $decoded["Countries"][$i]["NewConfirmed"], $decoded["Countries"][$i]["TotalConfirmed"], $decoded["Countries"][$i]["NewDeaths"], $decoded["Countries"][$i]["TotalDeaths"]);
    }
    
    
    CloseCon($conn);
    
    echo "Database updated with the latest covid data";
   
    
}
curl_close($ch);
?>
