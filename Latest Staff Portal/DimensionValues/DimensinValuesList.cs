using System.Collections.Generic;
using System.IO;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

public static class DimensinValuesList
{
    public static List<DropdownList> GetDimensionValues(int dimNo)
    {
        #region dim

        var dimensionValues = new List<DropdownList>();
        var pageDim = $"DimensionValues?$filter=Global_Dimension_No eq {dimNo} and Blocked eq false&$format=json";

        var httpResponsedim = Credentials.GetOdataData(pageDim);
        using var streamReader = new StreamReader(httpResponsedim.GetResponseStream());
        var result = streamReader.ReadToEnd();

        var details = JObject.Parse(result);


        foreach (JObject config in details["value"])
        {
            var dropdownList = new DropdownList();
            dropdownList.Text = (string)config["Code"] + "-" + (string)config["Name"];
            dropdownList.Value = (string)config["Code"];
            dimensionValues.Add(dropdownList);
        }

        #endregion

        return dimensionValues;
    }

    public static string GetDimensionValueName(string dimensionCode)
    {
        #region dim

        var dimensionName = "";
        var pageDim = $"DimensionValues?$filter=Code eq '{dimensionCode}' and Blocked eq false&$format=json";

        var httpResponsedim = Credentials.GetOdataData(pageDim);
        using var streamReader = new StreamReader(httpResponsedim.GetResponseStream());
        var result = streamReader.ReadToEnd();

        var details = JObject.Parse(result);


        foreach (JObject config in details["value"]) dimensionName = (string)config["Name"];

        #endregion

        return dimensionName;
    }
}