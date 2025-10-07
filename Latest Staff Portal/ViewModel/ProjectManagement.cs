using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
  

    public class ProjectProposals
    {
       
        public string No { get; set; }
        public string RequesterName { get; set; }
        public string Name { get; set; }
        public string AdminUnitCode { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Administration_Unit_Name { get; set; }
        public string County { get; set; }
        public string County_Name { get; set; }
        public string Sub_County { get; set; }
        public string Sub_County_Name { get; set; }
        public string Request_Description { get; set; }
        public string Justification { get; set; }
        public string Request_Source { get; set; }
        public string Created_By { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }

        public string Request_Status { get; set; }
        public string Status { get; set; }
        public string LoggedInUserID { get; set; }
        public bool Design_Created { get; set; }
        public string pagetitle { get; set; }
        public bool IsManager { get; set; }
        public List<SelectListItem> ListOfAdminUnits { get; set; }
        public List<SelectListItem> ListOfCounties { get; set; }
        
        public List<SelectListItem> ListOfRequestSource{ get; set; }
    }

    public class TeamLeadSelections
    {
      
        public string No { get; set; }
        public string User_Request_No { get; set; }
        public string Administrative_Unit { get; set; }
        public string Committee_Chair { get; set; }
        public string Chair_Name { get; set; }
        public string Date_of_Meeting { get; set; }
        public string Caseload { get; set; }
        public string Caseload_Category { get; set; }
        public string Project_Code { get; set; }
        public string Project_Name { get; set; }
        public string Objective_of_engagement { get; set; }
        public string Team_Lead { get; set; }
        public string Section_Lead_Area { get; set; }
        public bool Sent_to_team_Leads { get; set; }
        public bool isManager { get; set; }
        public bool Design_Created { get; set; }
        public List<SelectListItem> ListOfEmployees { get; set; }
    }


    public class ProjectTeamSelections
    {   
        public string No { get; set; }
        public string Project_Code { get; set; }
        public string Project_Name { get; set; }
        public string Geographical_Unit_Name { get; set; }
        public string Team_Lead { get; set; }
        public string Section_Lead_Area { get; set; }
        public string User_Request_No { get; set; }
        public bool Appointed { get; set; }
        public bool isManager { get; set; }
        public bool Design_Created { get; set; }
    }



    //Team leads Lines
    public class TeamLeads
    {
       
        public string Project_No { get; set; }
        public string Resource_No { get; set; }
        public string Resource_Name { get; set; }
        public string Specialty { get; set; }

        public bool isManager { get; set; }
        public List<SelectListItem> ListOfEmployees { get; set; }
        
    }

    //Project Team Lines
    public class ProjectTeam
    {
        public string Design_No { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Expertise { get; set; }
        public int Approval_Sequence { get; set; }
        public string Involvement_Stage { get; set; }

        public bool isManager { get; set; }

        public List<SelectListItem> ListOfEmployees { get; set; }
        
    }

    public class StakeholderMeeting
    {

        public string No { get; set; }
        public string Project_Code { get; set; }
        public string Project_Name { get; set; }
        public string Date_of_Meeting { get; set; }
        public bool isManager { get; set; }
    }

    public class StakeholderFeedback
    {  
        public string No { get; set; }
        public int Line_No { get; set; }
        public string Suggestion { get; set; }
        public string Location { get; set; }
        public string Proponent { get; set; }
        public string Status_Of_Suggestion { get; set; }
        public bool isManager { get; set; }
        public  List<SelectListItem> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfStatus { get; set; }
    }


    //Preliminary Design Control
    public class PreliminaryDesignControl
    {
        public string No { get; set; }
        public string Project_Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string Committee { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Geographic_Location_Name { get; set; }
        public int Total_Estimated_Cost { get; set; }
        public string Project_Classifications { get; set; }
        public string Created_By { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Rejection_Comments { get; set; }
        public string Deferal_Comments { get; set; }
        public string Design_Stage { get; set; }
        public string Status { get; set; }
        public string Proposed_Project { get; set; }
        public string Proposed_Project_Name { get; set; }
        public string Approved_Proposal { get; set; }
        public string Proposal_Name { get; set; }
        public string Team_Leader { get; set; }
        public string Design_Status { get; set; }
        public string Document_Type { get; set; }
        public string Design_Control_Type { get; set; }
        public string Current_Actioning_Member { get; set; }
        public string Design_Approval_Stage { get; set; }
        public string Situation_Analysis { get; set; }
        public string Relevance_of_proj_Idea { get; set; }
        public string Scope_of_the_project { get; set; }
        public string Logical_Framework { get; set; }
        public string Goal { get; set; }
        public string Project_Objectives_Outcome { get; set; }
        public string Proposed_Project_Outputs { get; set; }
        public string Project_Activities_and_Inputs { get; set; }
        public string institutional_Mandate { get; set; }
        public string Management_of_the_Project { get; set; }
        public string Monitoring_and_Evaluation { get; set; }
        public string Risk_and_Mitigation_Measures { get; set; }
        public string Project_Sustainability { get; set; }
        public string Project_Stakeholders_and_Collaborators { get; set; }
        public string Project_Readiness { get; set; }
        public bool Final_Design_Created { get; set; }
    }


    public class DesignControlsLines
    {  
        public string Header_No { get; set; }
        public int Entry_No { get; set; }
        public string Attachment_Code { get; set; }
        public int Design_Control_No { get; set; }
        public string Design_Control_Type { get; set; }
        public string Name { get; set; }
        public int No_of_Items { get; set; }
    }

    public class GrandSummaryBOQLines
    {  
        public string Header_No { get; set; }
        public int Line_No { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Unit_of_Measure { get; set; }
        public int Unit_Cost { get; set; }
        public int Total_Cost { get; set; }
    }

    public class TeamMembersBOQLines
    {
      
        public string Design_Code { get; set; }
        public string Member_No { get; set; }
        public int Entry_No { get; set; }
        public string Design_Stage { get; set; }
        public string Description { get; set; }
        public int quantity { get; set; }
        public int Unit_Price { get; set; }
        public int Total_Amount { get; set; }
        public List<SelectListItem> ListOfEmployees { get; set; }
    }


    public class ConceiptAnalysis
    {
        public string Project_No { get; set; }
        public string situationAnalysis { get; set; }
        public string relevance { get; set; }
        public string projectScope { get; set; }
        public string logicalFramework { get; set; }
        public string goal { get; set; }
        public string objectives { get; set; }
        public string projectOutput { get; set; }
        public string activitiesInput { get; set; }
        public string InstitutionalMandate { get; set; }
        public string ProjectManagement { get; set; }
        public string Monitoring { get; set; }
        public string Risk { get; set; }
        public string ProjectSustainability { get; set; }
        public string ProjectStakeholders { get; set; }
        public string ProjectReadiness { get; set; }
    }

}